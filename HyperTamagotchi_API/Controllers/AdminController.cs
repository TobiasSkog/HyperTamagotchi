using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[AuthorizeByRole("Admin")]
[Authorize(Roles = "Admin")]
public class AdminController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    [Route("AddDiscountToShoppingItems")]

    public async Task<IActionResult> AddDiscountToShoppingItems([FromBody] DiscountUpdateModel discountUpdateModel)
    {

        if (discountUpdateModel.SelectedShoppingItems == null || discountUpdateModel.SelectedShoppingItems.Count <= 0 ||
            (discountUpdateModel.DiscountPercentage == null && discountUpdateModel.DiscountPercentage >= 0 && discountUpdateModel.DiscountPercentage <= 100))
        {
            return NotFound();
        }

        var foundShoppingItems = await _context.ShoppingItems
            .Where(si => discountUpdateModel.SelectedShoppingItems.Contains(si.ShoppingItemId))
            .ToListAsync();

        foreach (var item in foundShoppingItems)
        {
            item.Discount = (float)discountUpdateModel.DiscountPercentage;
            _context.Update(item);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

}
