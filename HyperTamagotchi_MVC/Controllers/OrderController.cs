using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;

public class OrderController(ApiServices api) : Controller
{
    private readonly ApiServices _api = api;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _api.GetCustomerOrders());
    }

    [HttpGet]
    public async Task<IActionResult> OrderDetails(int id)
    {
        var order = await _api.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}