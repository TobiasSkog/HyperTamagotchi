using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;
public partial class ApiServices
{
    [HttpGet]
    [AuthorizeByRole("Admin")]
    public async Task<object?> GetItemToEditAsync(int? id)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.GetAsync($"api/Admin/GetItemToEdit/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<EditItemResponse>(jsonResponse);
        if (result == null)
        {
            return default;
        }

        return result.Type switch
        {
            ItemType.Tamagotchi => JsonConvert.DeserializeObject<Tamagotchi>(result.Data),
            ItemType.ShoppingItem => JsonConvert.DeserializeObject<ShoppingItem>(result.Data),
            _ => null
        };
    }


    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<Tamagotchi> GetTamagotchiByIdAsync(int? id)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.GetAsync($"api/Admin/GetTamagotchi/{id}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var tamagotchi = JsonConvert.DeserializeObject<Tamagotchi>(jsonResponse);
        return tamagotchi!;
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<bool> UpdateDiscountToShoppingItems(List<int> selectedShoppingItems, float discountPercentage)
    {
        EnsureJwtTokenIsAddedToRequest();

        var dto = new
        {
            SelectedShoppingItems = selectedShoppingItems,
            DiscountValue = discountPercentage
        };

        var response = await _client.PostAsJsonAsync($"api/Admin/AddDiscountToShoppingItems", dto);
        return response.IsSuccessStatusCode;
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<bool> CreateShoppingItemAsync(ShoppingItemDto dto)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.PostAsJsonAsync($"api/Admin/CreateShoppingItem", dto);
        return response.IsSuccessStatusCode;
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<bool> CreateTamagotchiAsync(TamagotchiDto dto)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.PostAsJsonAsync($"api/Admin/CreateTamagotchi", dto);
        return response.IsSuccessStatusCode;
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<bool> EditShoppingItem(ShoppingItem item)
    {
        EnsureJwtTokenIsAddedToRequest();
        var response = await _client.PostAsJsonAsync($"api/Admin/EditShoppingItem", item);
        return response.IsSuccessStatusCode;

    }
    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<bool> EditTamagotchi(Tamagotchi item)
    {
        EnsureJwtTokenIsAddedToRequest();
        var response = await _client.PostAsJsonAsync($"api/Admin/EditTamagotchi", item);
        return response.IsSuccessStatusCode;
    }

    [HttpDelete]
    [AuthorizeByRole("Admin")]
    public async Task<bool> Delete(int id)
    {
        EnsureJwtTokenIsAddedToRequest();
        var response = await _client.DeleteAsync($"api/Admin/Delete/{id}");
        return response.IsSuccessStatusCode;
    }

    [HttpGet]
    [AuthorizeByRole("Admin")]
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        EnsureJwtTokenIsAddedToRequest();
        await Console.Out.WriteLineAsync("popo");
        var response = await _client.GetAsync("api/Admin/GetAllOrders");
        //if (!response.IsSuccessStatusCode)
        //{
        //    return [];
        //}
        var jsonResponse = await response.Content.ReadAsStringAsync();



        var orders = JsonConvert.DeserializeObject<List<Order>>(jsonResponse);
        return orders;
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<Order> GetOrderByIdAsync(int id)
    {
        EnsureJwtTokenIsAddedToRequest();

        return await _client.GetFromJsonAsync<Order>($"api/Admin/GetSpecificOrder/{id}");
    }
}


//// GET: ShoppingItems/Delete/5
//public async Task<IActionResult> Delete(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var shoppingItem = await _context.ShoppingItems
//        .FirstOrDefaultAsync(m => m.ShoppingItemId == id);
//    if (shoppingItem == null)
//    {
//        return NotFound();
//    }

//    return View(shoppingItem);
//}