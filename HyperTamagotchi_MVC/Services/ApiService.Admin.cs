using HyperTamagotchi_MVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Services;


[AuthorizeByRole("Admin")]
//[Authorize(Roles = "Admin")]

public partial class ApiServices
{
    [HttpPost]
    public async Task<bool> UpdateDiscountToShoppingItems(List<int> selectedShoppingItems, float discountPercentage)
    {

        var dto = new
        {
            SelectedShoppingItems = selectedShoppingItems,
            DiscountPercentage = discountPercentage
        };
        var response = await _client.PostAsJsonAsync($"api/Admin/AddDiscountToShoppingItems", dto);

        return response.IsSuccessStatusCode;
    }
}

//public string Edit()
//{
//    var userPermission = IsUserInRole("Admin");

//    if (!string.IsNullOrEmpty(userPermission))
//    {
//        return userPermission;
//    }

//    return "";
//}