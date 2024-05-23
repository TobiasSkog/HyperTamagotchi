using HyperTamagotchi_MVC.Filters;
using HyperTamagotchi_MVC.Helpers;
using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Models.TamagotchiProperties;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HyperTamagotchi_MVC.Controllers;

[AuthorizeByRole("Admin")]
public class AdminController(ApiServices api) : Controller
{
    private readonly ApiServices _api = api;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _api.GetAllShoppingItemsAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _api.GetAllOrdersAsync();
        return View(orders);
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

    [HttpGet]
    public async Task<IActionResult> Discount()
    {
        return View(await _api.GetAllShoppingItemsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Discount(List<int> selectedShoppingItems, float? discountValue)
    {

        if (selectedShoppingItems == null || selectedShoppingItems.Count <= 0 || discountValue == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid input.");
            return RedirectToAction("Discount");
        }

        if (selectedShoppingItems == null || selectedShoppingItems.Count <= 0)
        {
            ModelState.AddModelError(string.Empty, "No selected items.");
            return RedirectToAction("Discount");
        }

        float discountPercentage = DiscountConversionHelper.ConvertFromUserInputToShoppingItem((float)discountValue);


        var success = await _api.UpdateDiscountToShoppingItems(selectedShoppingItems, discountPercentage);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, "Failed to update discount.");
            return RedirectToAction("Discount");
        }

        return RedirectToAction("Discount");

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

    public IActionResult AccessDenied()
    {
        return View();
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

        var item = await _api.GetItemToEditAsync(id);

        if (item is Tamagotchi)
        {
            return RedirectToAction("EditTamagotchi", item);
        }

        return View("Edit", item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ShoppingItem item)
    {
        var result = await _api.EditShoppingItem(item);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Something Went Wrong.");
        }

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult EditTamagotchi(Tamagotchi tamagotchi)
    {
        if (tamagotchi == null)
        {
            return NotFound();
        }

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

        return View(tamagotchi);
    }

    [HttpPost, ActionName("EditTamagotchi")]
    public async Task<IActionResult> EditTamagotchiPost(Tamagotchi item)
    {
        var result = await _api.EditTamagotchi(item);

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
