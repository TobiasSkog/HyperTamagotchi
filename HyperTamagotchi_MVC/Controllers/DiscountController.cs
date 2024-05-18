using HyperTamagotchi_MVC.Data;
using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;

[AuthorizeByRole("Admin")]
public class DiscountController(ApiServices api) : Controller
{
    private readonly ApiServices _api = api;

    public async Task<IActionResult> Index()
    {
        return View(await _api.GetAllShoppingItemsAsync());
    }

    public IActionResult AddDiscountToShoppingItems(List<int> selectedShoppingItems, float discountValue)
    {
        float discountPercentage = DiscountConversionHelper.ConvertFromUserInputToShoppingItem(discountValue);
        var hej = _api.Edit();

        return View();
    }
}
