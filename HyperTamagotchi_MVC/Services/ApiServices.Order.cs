using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Services;

{
    public partial class ApiServices
    {
        [HttpGet]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<IEnumerable<OrderDTO>> GetCustomerOrders(string customerId)
        {
            EnsureJwtTokenIsAddedToRequest();

            var response = await _client.GetAsync($"api/Orders/GetOrdersFromCustomer/{customerId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
            }
            else
            {
                return null;
            }
        }

      return order;
       
    }
}
