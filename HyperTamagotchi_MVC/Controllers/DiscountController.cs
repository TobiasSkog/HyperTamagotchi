using HyperTamagotchi_MVC.Data;
using HyperTamagotchi_MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_MVC.Controllers;

[AuthorizeByRole("Admin")]
public class DiscountController : Controller
{
    private readonly ApplicationDbContext _context;

    public DiscountController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.ShoppingItems.ToListAsync());
    }

    public IActionResult AddDiscountToShoppingItems(List<int> selectedShoppingItems, float discountValue)
    {
        float discountPercentage = DiscountConversionHelper.ConvertFromUserInputToShoppingItem(discountValue);
        return View();
    }
}
