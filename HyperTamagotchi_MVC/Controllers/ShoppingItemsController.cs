using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Models.TamagotchiProperties;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HyperTamagotchi_MVC.Controllers;

[AuthorizeByRole("Admin")]

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
    public IActionResult Create()
    {
        return View(new ShoppingItemDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShoppingItemDto dto)
    {

        var result = await _api.CreateShoppingItemAsync(dto);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Something Went Wrong.");
        }

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult CreateTamagotchi()
    {
        ViewData["Colors"] = Enum.GetValues(typeof(TamagotchiColor))
        .Cast<TamagotchiColor>()
        .Select(tc => new SelectListItem
        {
            Value = ((byte)tc).ToString(),
            Text = tc.ToString()
        })
        .ToList();
        ViewData["Moods"] = Enum.GetValues(typeof(TamagotchiMood))
            .Cast<TamagotchiMood>()
            .Select(tc => new SelectListItem
            {
                Value = ((byte)tc).ToString(),
                Text = tc.ToString()
            })
            .ToList();
        ViewData["Stages"] = Enum.GetValues(typeof(TamagotchiStage))
            .Cast<TamagotchiStage>()
            .Select(tc => new SelectListItem
            {
                Value = ((byte)tc).ToString(),
                Text = tc.ToString()
            })
            .ToList();
        ViewData["Types"] = Enum.GetValues(typeof(TamagotchiType))
            .Cast<TamagotchiType>()
            .Select(tc => new SelectListItem
            {
                Value = ((byte)tc).ToString(),
                Text = tc.ToString()
            })
            .ToList();
        return View(new Tamagotchi());
    }

    [HttpPost]
    public async Task<IActionResult> CreateTamagotchi(TamagotchiDto dto)
    {
        var result = await _api.CreateTamagotchiAsync(dto);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Something Went Wrong.");
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var item = await _api.GetShoppingItemByIdAsync(id);

        if (item is Tamagotchi tamagotchi)
        {
            ViewData["Colors"] = Enum.GetValues(typeof(TamagotchiColor))
                .Cast<TamagotchiColor>()
                .Select(tc => new SelectListItem
                {
                    Value = ((byte)tc).ToString(),
                    Text = tc.ToString()
                })
                .ToList();
            ViewData["Moods"] = Enum.GetValues(typeof(TamagotchiMood))
                .Cast<TamagotchiMood>()
                .Select(tc => new SelectListItem
                {
                    Value = ((byte)tc).ToString(),
                    Text = tc.ToString()
                })
                .ToList();
            ViewData["Stages"] = Enum.GetValues(typeof(TamagotchiStage))
                .Cast<TamagotchiStage>()
                .Select(tc => new SelectListItem
                {
                    Value = ((byte)tc).ToString(),
                    Text = tc.ToString()
                })
                .ToList();
            ViewData["Types"] = Enum.GetValues(typeof(TamagotchiType))
                .Cast<TamagotchiType>()
                .Select(tc => new SelectListItem
                {
                    Value = ((byte)tc).ToString(),
                    Text = tc.ToString()
                })
                .ToList();
            return View("EditTamagotchi", tamagotchi);
        }

        return View("Edit", item);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(ShoppingItem item)
    {
        bool result;
        if (item is Tamagotchi tamagotchi)
        {
            result = await _api.EditTamagotchi(tamagotchi);
        }
        else
        {
            result = await _api.EditShoppingItem(item);
        }

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Something Went Wrong.");
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var item = await _api.GetShoppingItemByIdAsync(id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var result = await _api.Delete(id);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Something Went Wrong.");
        }

        return RedirectToAction("Index");
    }
}