using HyperTamagotchi_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;
public partial class ApiServices
{
    public async Task<IdentityUser> GetUserByEmailAsync(string email)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.GetAsync($"api/CustomerCart/email/{email}");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<IdentityUser>();
        return user;
    }

    public async Task<Order> CheckoutAsync(CheckoutModel checkoutModel)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.PostAsJsonAsync("api/CustomerCart/checkout", checkoutModel);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var order = JsonConvert.DeserializeObject<Order>(jsonResponse);
        return order!;
    }

    public async Task<Address> GetAddressByIdAsync(string? id)
    {
        EnsureJwtTokenIsAddedToRequest();

        if (id == null)
        {
            return default;
        }
        _ = int.TryParse(id, out var addressId);
        //https://localhost:7043/api/CustomerCart/GetAddress/2
        //https://localhost:7043/api/CusomterCart/GetAddress/2

        var response = await _client.GetAsync($"api/CustomerCart/GetAddress/{addressId}");
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var address = JsonConvert.DeserializeObject<Address>(jsonResponse);

        return address;
    }
}