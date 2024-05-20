using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HyperTamagotchi_MVC.Controllers;
public class HomeController(ApiServices api, ILogger<HomeController> logger) : BaseController(api)
{
    private readonly ILogger<HomeController> _logger = logger;

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

        shoppingItem.Quantity ??= 1;

        shoppingCart.ShoppingItems.Add(shoppingItem);

        _api.UpdateShopingCartCookie(shoppingCart);

        SetShoppingCartInViewBag(shoppingCart);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<IActionResult> IncreaseQuantity(int? shoppingItemId)
    {
        if (shoppingItemId == null)
        {
            return NotFound();
        }

        var shoppingCart = GetShoppingCartFromCookie();
        if (shoppingCart == null)
        {
            return NotFound();
        }

        var shoppingItemToUpdate = shoppingCart.ShoppingItems.FirstOrDefault(item => item.ShoppingItemId == shoppingItemId);

        shoppingItemToUpdate ??= await _api.GetShoppingItemByIdAsync(shoppingItemId);

        shoppingItemToUpdate.Quantity ??= (byte)0;



        if (shoppingItemToUpdate.Quantity < 255)
        {
            shoppingItemToUpdate.Quantity++;
        }
        else
        {
            shoppingItemToUpdate.Quantity = 255;
        }


        _api.UpdateShopingCartCookie(shoppingCart);
        SetShoppingCartInViewBag(shoppingCart);



        return RedirectToAction("Index");
    }
    [HttpPost]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<IActionResult> DecreaseQuantity(int? shoppingItemId)
    {
        if (shoppingItemId == null)
        {
            return NotFound();
        }

        var shoppingCart = GetShoppingCartFromCookie();
        if (shoppingCart == null)
        {
            return NotFound();
        }

        var shoppingItemToUpdate = shoppingCart.ShoppingItems.FirstOrDefault(item => item.ShoppingItemId == shoppingItemId);

        shoppingItemToUpdate ??= await _api.GetShoppingItemByIdAsync(shoppingItemId);

        shoppingItemToUpdate.Quantity ??= (byte)1;

        if (shoppingItemToUpdate.Quantity > 1)
        {
            shoppingItemToUpdate.Quantity--;
        }
        else
        {
            shoppingCart.ShoppingItems.RemoveAll(item => item.ShoppingItemId == shoppingItemId);
        }

        _api.UpdateShopingCartCookie(shoppingCart);
        SetShoppingCartInViewBag(shoppingCart);

        return RedirectToAction("Index");
    }
    [HttpPost]
    [AuthorizeByRole("Admin", "Customer")]
    public async Task<IActionResult> AdjustQuantity(int? shoppingItemId, int? amount)
    {
        if (shoppingItemId == null || amount == null)
        {
            return NotFound();
        }
        if (amount > 255)
        {
            amount = (byte)255;
        }
        if (amount < 0)
        {
            amount = (byte)0;
        }
        var shoppingCart = GetShoppingCartFromCookie();
        if (shoppingCart == null)
        {
            return NotFound();
        }

        var shoppingItemToUpdate = shoppingCart.ShoppingItems.FirstOrDefault(item => item.ShoppingItemId == shoppingItemId);

        shoppingItemToUpdate ??= await _api.GetShoppingItemByIdAsync(shoppingItemId);

        shoppingItemToUpdate.Quantity ??= (byte)1;


        shoppingItemToUpdate.Quantity = (byte)amount;
        if (shoppingItemToUpdate.Quantity <= 0)
        {
            shoppingCart.ShoppingItems.RemoveAll(item => item.ShoppingItemId == shoppingItemId);
        }
        if (shoppingItemToUpdate.Quantity > 255)
        {
            shoppingItemToUpdate.Quantity = 255;
        }

        _api.UpdateShopingCartCookie(shoppingCart);
        SetShoppingCartInViewBag(shoppingCart);

        return RedirectToAction("Index");
    }
    [HttpPost]
    //[AuthorizeByRole("Admin", "Customer")]
    [Authorize(Roles = "Admin, Customer")]
    public IActionResult EmptyCart()
    {
        var shoppingCart = GetShoppingCartFromCookie();
        if (shoppingCart == null)
        {
            return NotFound();
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
