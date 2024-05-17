using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Repositories;
using HyperTamagotchi_SharedModels.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HyperTamagotchi_MVC.Services;

public class ApiServices(IHttpClientFactory httpFactory, IHttpContextAccessor contextAccessor, IJwtTokenValidator jwtValidator)
{
    private readonly HttpClient _client = httpFactory.CreateClient("API Tamagotchi");
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IJwtTokenValidator _jwtValidator = jwtValidator;

    private void AddJwtTokenToRequest()
    {
        var token = _contextAccessor.HttpContext.Request.Cookies["jwtToken"];
        if (!string.IsNullOrEmpty(token))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
    [ValidateAntiForgeryToken]
    public async Task<bool> LoginAsync(string username, string password, bool rememberMe)
    {
        var response = await _client.PostAsJsonAsync("api/Auth/Login", new LoginRequestDto { Username = username, Password = password, RememberMe = rememberMe });

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<ApiToken>(content);

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.AccessToken))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(30)
                };

                _contextAccessor.HttpContext.Response.Cookies.Append("jwtToken", loginResponse.AccessToken, cookieOptions);

                var principal = _jwtValidator.ValidateToken(loginResponse.AccessToken);

                if (principal != null)
                {
                    _contextAccessor.HttpContext.User = principal;
                }

                AddJwtTokenToRequest();

                return true;
            }
        }

        return false;
    }
    [ValidateAntiForgeryToken]
    public async Task<bool> RegisterAsync(string password, string confirmPassword, string email, string firstName, string lastName, string streetAddress, string city, string zipCode)
    {
        var payload = new
        {
            email,
            password,
            confirmPassword,
            username = email,
            firstName,
            lastName,
            streetAddress,
            city,
            zipCode

        };

        var response = await _client.PostAsJsonAsync("api/Auth/Register", payload);
        return response.IsSuccessStatusCode;
    }
    private class ApiToken
    {
        public string AccessToken { get; set; }
    }




    public async Task<IEnumerable<ShoppingItem>> GetAllShoppingItemsAsync()
    {
        var response = await _client.GetAsync("api/ShoppingItem");
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var shoppingItems = JsonConvert.DeserializeObject<IEnumerable<ShoppingItem>>(jsonResponse);
        return shoppingItems!;
    }

    public async Task<ShoppingItem> GetShoppingItem(int? id)
    {
        var response = await _client.GetAsync($"api/ShoppingItem/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var shoppingItem = JsonConvert.DeserializeObject<ShoppingItem>(jsonResponse);
        return shoppingItem!;
    }

}


