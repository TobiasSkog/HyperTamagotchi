using HyperTamagotchi_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices
{
    [HttpPost]
    public async Task<bool> UpdateDiscountToShoppingItems(List<int> selectedShoppingItems, float discountPercentage)
    {
        EnsureJwtTokenIsAddedToRequest();

        var dto = new
        {
            SelectedShoppingItems = selectedShoppingItems,
            DiscountPercentage = discountPercentage
        };

        var response = await _client.PostAsJsonAsync($"api/Admin/AddDiscountToShoppingItems", dto);
        return response.IsSuccessStatusCode;
    }

    [HttpPost]
    public async Task<bool> CreateShoppingItemAsync(ShoppingItemDto dto)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.PostAsJsonAsync($"api/Admin/CreateShoppingItem", dto);
        return response.IsSuccessStatusCode;
    }

    [HttpPost]
    public async Task<bool> CreateTamagotchiAsync(TamagotchiDto dto)
    {
        EnsureJwtTokenIsAddedToRequest();

        var response = await _client.PostAsJsonAsync($"api/Admin/CreateTamagotchi", dto);
        return response.IsSuccessStatusCode;
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