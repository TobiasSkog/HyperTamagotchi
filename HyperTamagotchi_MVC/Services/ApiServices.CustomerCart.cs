using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi.Common.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HyperTamagotchi_MVC.Services;
public partial class ApiServices
{
    public async Task<IdentityUser> GetUserByEmailAsync(string email)
    {
        var response = await _client.GetAsync($"api/CustomerCart/email/{email}");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<IdentityUser>();
        return user;
    }

    public async Task<Order> CheckoutAsync(CheckoutModel checkoutModel)
    {
        var response = await _client.PostAsJsonAsync("api/CustomerCart/checkout", checkoutModel);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var order = JsonConvert.DeserializeObject<Order>(jsonResponse);
        return order!;
    }
}