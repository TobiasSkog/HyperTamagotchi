﻿using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using HyperTamagotchi_API.Models.GoogleMaps;

namespace HyperTamagotchi_API.Models.GoogleMaps
{
    public class TimeDelivery
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<TimeDelivery> _logger;

        public TimeDelivery(HttpClient httpClient, string apiKey, ILogger<TimeDelivery> logger)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _logger = logger;
        }

        public async Task<(string, string)> CalculateDeliveryTime(string origin, string destination)
        {
            string requestUri = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={Uri.EscapeDataString(origin)}&destinations={Uri.EscapeDataString(destination)}&key={_apiKey}";
            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                var element = data["rows"][0]["elements"][0];
                string status = element["status"].ToString();
                if (status == "OK")
                {
                    string duration = element["duration"]["text"].ToString();
                    return (duration, "OK");
                }
                return ("", status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating delivery time.");
                return ("", response != null ? $"HTTP Error: {response.StatusCode}" : ex.Message);
            }
        }

        public async Task<(string, string)> CalculateRouteWithStops(string origin, string destination)
        {
            var (directResult, directStatus) = await CalculateDeliveryTime(origin, destination);
            if (directStatus == "OK")
            {
                TimeSpan directDuration = ParseDuration(directResult);
                string formattedDirectResult = FormatDurationToDays(directDuration);
                return (formattedDirectResult, "Direct route successful.");
            }

            string osloPort = "Oslo, Norway";
            string newYorkPort = "New York, USA";
            string australiaPort = "Sydney, Australia";

            var (osloResult, osloStatus) = await CalculateDeliveryTime(origin, osloPort);
            if (osloStatus != "OK") return ("", "Cannot calculate route to Oslo.");

            var (newYorkResult, newYorkStatus) = await CalculateDeliveryTime(newYorkPort, destination);
            if (newYorkStatus == "OK")
            {
                TimeSpan totalDuration = ParseDuration(osloResult) + TimeSpan.FromHours(GetFlightHours(newYorkPort)) + ParseDuration(newYorkResult);
                return (FormatDurationToDays(totalDuration), "Route via New York successful.");
            }

            var (australiaResult, australiaStatus) = await CalculateDeliveryTime(australiaPort, destination);
            if (australiaStatus == "OK")
            {
                TimeSpan totalDuration = ParseDuration(osloResult) + TimeSpan.FromHours(GetFlightHours(australiaPort)) + ParseDuration(australiaResult);
                return (FormatDurationToDays(totalDuration), "Route via Australia successful.");
            }

            return ("", "No valid routes found.");
        }

        private string FormatDurationToDays(TimeSpan duration)
        {
            int days = (int)Math.Ceiling(duration.TotalHours / 8.0);
            return $"{days} day(s)";
        }

        private int GetFlightHours(string port)
        {
            if (port.Contains("New York")) return 8;
            if (port.Contains("Sydney")) return 12;
            return 0;
        }

        private TimeSpan ParseDuration(string duration)
        {
            int hours = 0;
            int minutes = 0;

            var parts = duration.Split(' ');
            for (int i = 0; i < parts.Length; i += 2)
            {
                if (parts[i + 1].StartsWith("hour"))
                    hours = int.Parse(parts[i]);
                else if (parts[i + 1].StartsWith("min"))
                    minutes = int.Parse(parts[i]);
            }
            return new TimeSpan(hours, minutes, 0);
        }
    }
}
