using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HyperTamagotchi_MVC.Controllers;

public class HomeController(ApiServices api, ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly ApiServices _api = api;

    //[AuthorizeByRole("Admin")]
    public async Task<IActionResult> Index()
    {
        return View(await _api.GetAllShoppingItemsAsync());
    }
    public async Task<IActionResult> AddCart()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
