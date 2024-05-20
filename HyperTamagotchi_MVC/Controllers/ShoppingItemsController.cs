using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;


public class ShoppingItemsController(ApiServices api) : Controller
{
    private readonly ApiServices _api = api;

    public async Task<IActionResult> Index()
    {
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

    [HttpGet]
    [AuthorizeByRole("Admin")]
    public IActionResult Create()
    {
        return View(new ShoppingItem());
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<IActionResult> Create(ShoppingItemDto dto)
    {

        var result = await _api.CreateShoppingItemAsync(dto);

        return result switch
        {
            true => Ok("Shopping Item Created Successfully."),
            false => NotFound("Something Went Wrong.")
        };
    }


    [HttpGet]
    [AuthorizeByRole("Admin")]
    public IActionResult CreateTamagotchi()
    {
        return View(new ShoppingItem());
    }

    [HttpPost]
    [AuthorizeByRole("Admin")]
    public async Task<IActionResult> CreateTamagotchi(TamagotchiDto dto)
    {
        var result = await _api.CreateTamagotchiAsync(dto);

        return result switch
        {
            true => Ok("Shopping Item Created Successfully."),
            false => NotFound("Something Went Wrong.")
        };
    }

    [HttpGet]
    [AuthorizeByRole("Admin")]
    public IActionResult Edit(ShoppingItem item)
    {
        if (item is Tamagotchi tamagotchi)
        {
            return View("EditTamagotchi", tamagotchi);
        }

        return View(new ShoppingItem());
    }
    [HttpGet]
    [AuthorizeByRole("Admin")]
    public IActionResult EditTamagotchi(Tamagotchi item)
    {

        return View(new ShoppingItem());
    }
}