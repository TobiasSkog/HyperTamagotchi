using HyperTamagotchi_SharedModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HyperTamagotchi_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //[Authorize(Roles = "Customer")]
    public async Task<IActionResult> Index()
    {
        User.Identity.IsAuthenticated.ToString();
        await Console.Out.WriteLineAsync("hej");
        return View();
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
