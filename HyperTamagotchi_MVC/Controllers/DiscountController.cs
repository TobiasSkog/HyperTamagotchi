using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Helpers;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;

[AuthorizeByRole("Admin")]
public class DiscountController(ApiServices api) : Controller
{
    private readonly ApiServices _api = api;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _api.EnsureJwtTokenIsAddedToRequest();

        return View(await _api.GetAllShoppingItemsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddDiscountToShoppingItems(List<int> selectedShoppingItems, float? discountValue)
    {
        _api.EnsureJwtTokenIsAddedToRequest();

        if (selectedShoppingItems == null || selectedShoppingItems.Count <= 0 || discountValue == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid input.");
            return RedirectToAction("Index");
        }

        if (selectedShoppingItems == null || selectedShoppingItems.Count <= 0)
        {
            ModelState.AddModelError(string.Empty, "No selected items.");
            return RedirectToAction("Index");
        }

        float discountPercentage = DiscountConversionHelper.ConvertFromUserInputToShoppingItem((float)discountValue);


        var success = await _api.UpdateDiscountToShoppingItems(selectedShoppingItems, discountPercentage);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, "Failed to update discount.");
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");

    }
}
