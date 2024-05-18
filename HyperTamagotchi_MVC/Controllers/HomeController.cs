using HyperTamagotchi_MVC.Services;
using HyperTamagotchi_SharedModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HyperTamagotchi_MVC.Controllers;

public class HomeController(ApiServices api, ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly ApiServices _api = api;

    //[Authorize(Roles = "Customer")]
    public async Task<IActionResult> Index()
    {
        return View(await _api.GetAllShoppingItemsAsync());
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
        var hej = _api.Edit();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
