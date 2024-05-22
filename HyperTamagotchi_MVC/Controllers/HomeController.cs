using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HyperTamagotchi_MVC.Controllers;
public class HomeController(ApiServices api) : BaseController(api)
{
    public async Task<IActionResult> Index()
    {
        _api.EnsureJwtTokenIsAddedToRequest();

        SetShoppingCartInViewBagFromCookie();
        return View(await _api.GetAllShoppingItemsAsync());
    }

    [HttpPost]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<IActionResult> AddToCart(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var shoppingCart = GetShoppingCartFromCookie();
        var shoppingItem = await _api.GetShoppingItemByIdAsync(id);

        shoppingItem.Quantity = 1;

        shoppingCart.ShoppingItems.Add(shoppingItem);

        _api.UpdateShopingCartCookie(shoppingCart);

        SetShoppingCartInViewBag(shoppingCart);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<IActionResult> AdjustQuantity(int? shoppingItemId, int amount)
    {
        if (shoppingItemId == null)
        {
            return Json(new { success = false, message = "Invalid shopping item ID." });
        }

        var shoppingCart = GetShoppingCartFromCookie();
        if (shoppingCart == null)
        {
            return Json(new { success = false, message = "Shopping cart not found." });
        }

        var itemToUpdate = shoppingCart.ShoppingItems.FirstOrDefault(item => item.ShoppingItemId == shoppingItemId);
        itemToUpdate ??= await _api.GetShoppingItemByIdAsync(shoppingItemId);
        if (itemToUpdate == null)
        {
            return Json(new { success = false, message = "Item not found." });
        }

        if (amount <= 0)
        {
            RemoveItemFromCart(shoppingCart, itemToUpdate);
            return Json(new { success = true, quantity = 0, totalQuantity = shoppingCart.ShoppingItems.Sum(si => si.Quantity), shouldRemove = true });
        }

        byte stock = itemToUpdate.Stock > 0 ? itemToUpdate.Stock : (byte)0;
        byte adjustedAmount = (byte)Math.Min(amount, Math.Min(byte.MaxValue, stock));

        if (adjustedAmount == 0)
        {
            RemoveItemFromCart(shoppingCart, itemToUpdate);
            return Json(new { success = true, quantity = 0, totalQuantity = shoppingCart.ShoppingItems.Sum(si => si.Quantity), shouldRemove = true });
        }

        itemToUpdate.Quantity = adjustedAmount;

        _api.UpdateShopingCartCookie(shoppingCart);
        SetShoppingCartInViewBag(shoppingCart);

        //return Json(new { success = true, quantity = adjustedAmount });
        return Json(new { success = true, quantity = adjustedAmount, totalQuantity = shoppingCart.ShoppingItems.Sum(si => si.Quantity), totalPrice = shoppingCart.ShoppingItems.Sum(si => Math.Round((float)si.Quantity * si.Price * si.Discount, 2)) });
    }
    private void RemoveItemFromCart(ShoppingCart shoppingCart, ShoppingItem itemToRemove)
    {
        shoppingCart.ShoppingItems.RemoveAll(item => item.ShoppingItemId == itemToRemove.ShoppingItemId);
        _api.UpdateShopingCartCookie(shoppingCart);
        SetShoppingCartInViewBag(shoppingCart);
    }
    [HttpPost]
    [AuthorizeByRole("Customer")]
    public IActionResult EmptyCart()
    {
        var shoppingCart = GetShoppingCartFromCookie();
        if (shoppingCart == null)
        {
            return RedirectToAction("Index");
        }

        shoppingCart.ShoppingItems.Clear();

        _api.UpdateShopingCartCookie(shoppingCart);
        SetShoppingCartInViewBag(shoppingCart);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}


//[HttpPost]
//[AuthorizeByRole("Admin", "Customer")]
//public async Task<IActionResult> IncreaseQuantity(int? shoppingItemId)
//{
//    if (shoppingItemId == null)
//    {
//        return NotFound();
//    }
//    var shoppingCart = GetShoppingCartFromCookie();
//    if (shoppingCart == null)
//    {
//        return NotFound();
//    }
//    var itemToUpdate = shoppingCart.ShoppingItems.FirstOrDefault(item => item.ShoppingItemId == shoppingItemId);
//    itemToUpdate ??= await _api.GetShoppingItemByIdAsync(shoppingItemId);
//    itemToUpdate.Quantity ??= 1;
//    if (itemToUpdate.Quantity < 255 && itemToUpdate.Quantity < itemToUpdate.Stock)
//    {
//        itemToUpdate.Quantity++;
//    }
//    else if (itemToUpdate.Quantity >= 255 || itemToUpdate.Quantity >= itemToUpdate.Stock)
//    {
//        itemToUpdate.Quantity = Math.Min((byte)255, itemToUpdate.Stock);
//    }
//    _api.UpdateShopingCartCookie(shoppingCart);
//    SetShoppingCartInViewBag(shoppingCart);
//    return RedirectToAction("Index");
//}
//[HttpPost]
//[AuthorizeByRole("Admin", "Customer")]
//public async Task<IActionResult> DecreaseQuantity(int? shoppingItemId)
//{
//    if (shoppingItemId == null)
//    {
//        return NotFound();
//    }
//    var shoppingCart = GetShoppingCartFromCookie();
//    if (shoppingCart == null)
//    {
//        return NotFound();
//    }
//    var shoppingItemToUpdate = shoppingCart.ShoppingItems.FirstOrDefault(item => item.ShoppingItemId == shoppingItemId);
//    shoppingItemToUpdate ??= await _api.GetShoppingItemByIdAsync(shoppingItemId);
//    shoppingItemToUpdate.Quantity ??= 1;
//    if (shoppingItemToUpdate.Quantity > 1)
//    {
//        shoppingItemToUpdate.Quantity--;
//    }
//    else
//    {
//        shoppingCart.ShoppingItems.RemoveAll(item => item.ShoppingItemId == shoppingItemId);
//    }
//    _api.UpdateShopingCartCookie(shoppingCart);
//    SetShoppingCartInViewBag(shoppingCart);
//    return RedirectToAction("Index");
//}