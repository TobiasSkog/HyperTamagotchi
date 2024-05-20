using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;


public class ShoppingItemsController(ApiServices api, ILogger<ShoppingItemsController> logger) : Controller
{
    private readonly ApiServices _api = api;
    private readonly ILogger<ShoppingItemsController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        _api.EnsureJwtTokenIsAddedToRequest();

        return View(await _api.GetAllShoppingItemsAsync());
    }

    // GET: ShoppingItems/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return View(await _api.GetShoppingItemByIdAsync(id));
    }
}