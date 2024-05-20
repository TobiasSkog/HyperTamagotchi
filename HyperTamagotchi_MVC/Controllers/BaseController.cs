using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Controllers;

public class BaseController(ApiServices api) : Controller
{
    protected readonly ApiServices _api = api;

    protected void SetShoppingCartInViewBagFromCookie()
    {
        var shoppingCart = GetShoppingCartFromCookie();

        if (shoppingCart != null)
        {
            ViewBag.ShoppingCart = shoppingCart;
        }
    }
    protected void SetShoppingCartInViewBag(ShoppingCart shoppingCart)
    {
        if (shoppingCart != null)
        {
            ViewBag.ShoppingCart = shoppingCart;
        }
    }
    protected ShoppingCart GetShoppingCartFromCookie()
    {
        var shoppingCartJson = HttpContext.Request.Cookies["ShoppingCart"];

        if (string.IsNullOrEmpty(shoppingCartJson))
        {
            return new ShoppingCart();
        }

        var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(shoppingCartJson);

        return shoppingCart;
    }
}
