using HyperTamagotchi_MVC.Data;
using HyperTamagotchi_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShoppingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoppingItems.ToListAsync());
        }

        // GET: ShoppingItems/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(shoppingItem);
        }

        // GET: ShoppingItems/Create
        public IActionResult Create()
        {
            ShoppingItem newShoppingItem = new();
            return View(newShoppingItem);
        }

        // POST: ShoppingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingItemId,ShoppingItemName,Description,Stock,Price,CurrencyType,Discount,Quantity")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingItem);
        }

        // GET: ShoppingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }
            return View(shoppingItem);
        }

        // POST: ShoppingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingItemId,ShoppingItemName,Description,Stock,Price,CurrencyType,Discount,Quantity")] ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.ShoppingItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingItemExists(shoppingItem.ShoppingItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingItem);
        }

        // GET: ShoppingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(shoppingItem);
        }

        // POST: ShoppingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem != null)
            {
                _context.ShoppingItems.Remove(shoppingItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingItemExists(int id)
        {
            return _context.ShoppingItems.Any(e => e.ShoppingItemId == id);
        }
    }
}
