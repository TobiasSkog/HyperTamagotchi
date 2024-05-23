using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using Newtonsoft.Json;


namespace HyperTamagotchi_MVC.Services;
public partial class ApiServices
{
    public async Task<CustomerDto> GetUserIdByEmailAsync()
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.GetAsync($"api/Customer/Email");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<CustomerDto>();
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