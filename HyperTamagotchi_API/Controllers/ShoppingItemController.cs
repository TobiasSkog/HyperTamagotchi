using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class ShoppingItemController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItems()
    {
        return await _context.ShoppingItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingItem>> GetShoppingItem(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingItem = await _context.ShoppingItems
            .FirstOrDefaultAsync(m => m.ShoppingItemId == id);
        if (shoppingItem == null)
        {
            return NotFound();
        }

        return shoppingItem;
    }
}