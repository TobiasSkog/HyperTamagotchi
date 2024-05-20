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
}
