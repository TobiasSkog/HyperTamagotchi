using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices
{
    [HttpPost]
    [AuthorizeByRole("Admin")]
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
        var response = await _client.DeleteAsync($"api/Admin/Delete{id}");
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