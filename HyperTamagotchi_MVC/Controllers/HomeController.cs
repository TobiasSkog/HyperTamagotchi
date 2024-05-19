using HyperTamagotchi_MVC.Data;
using HyperTamagotchi_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HyperTamagotchi_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger; 
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: ShoppingItems
    public async Task<IActionResult> Index()
    {
        return View(await _context.ShoppingItems.ToListAsync());
    }


    //ARBETAR PÅ DETTA -KÄROLAJN
    public async Task<IActionResult> AddCart()
    {
        return View()
    }


    ////ADMIN
    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
