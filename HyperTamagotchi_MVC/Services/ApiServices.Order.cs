using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices
{
    [HttpGet]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<List<Order>> GetCustomerOrders()
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.GetAsync($"api/Orders/GetOrdersFromCustomer");
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var orders = JsonConvert.DeserializeObject<List<Order>>(jsonResponse);
        return orders;
    }

    [HttpGet]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<Order> GetOrderByIdAsync(int id)
    {
        EnsureJwtTokenIsAddedToRequest();
        var response = await _client.GetAsync($"api/Orders/GetOrderById/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var order = JsonConvert.DeserializeObject<Order>(jsonResponse);
        return order;
    }
}
