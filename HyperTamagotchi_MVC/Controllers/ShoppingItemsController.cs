using HyperTamagotchi_MVC.Data;
using HyperTamagotchi_SharedModels.Models;
using HyperTamagotchi_SharedModels.Models.TamagotchiProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_MVC.Controllers;

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
        ShoppingItem shoppingItem = new();
        return View(shoppingItem);
    }

    // POST: ShoppingItems/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ShoppingItemId,Name,Description,Stock,Price,CurrencyType,Discount,ImagePath,Quantity")] ShoppingItem shoppingItem)
    {
        if (ModelState.IsValid)
        {
            // Update to a real Image Path based on our project folders and that the user only enters the image name in the input
            string realImagePath = @"/Assets/Img/" + shoppingItem.ImagePath;
            shoppingItem.ImagePath = realImagePath;
            _context.Add(shoppingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(shoppingItem);
    }

    public IActionResult CreateTamagotchi()
    {
        Tamagotchi tamagotchi = new();

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

    // POST: ShoppingItems/CreateTamagotchi
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTamagotchi([Bind("TamagotchiColor,TamagotchiType,Mood,TamagotchiStage,Experience,ShoppingItemId,Name,Description,Stock,Price,CurrencyType,Discount,ImagePath,Quantity")] Tamagotchi tamagotchi)
    {
        if (ModelState.IsValid)
        {
            // Update to a real Image Path based on our project folders and that the user only enters the image name in the input
            string realImagePath = @"/Assets/Tamagotchi/" + tamagotchi.ImagePath;
            tamagotchi.ImagePath = realImagePath;

            // Update the experiences points on the Tamagotchi based on what stage it is when it's created
            switch (tamagotchi.TamagotchiStage)
            {
                case TamagotchiStage.Egg:
                    tamagotchi.Experience = 0;
                    break;
                case TamagotchiStage.Child:
                    tamagotchi.Experience = 50;
                    break;
                case TamagotchiStage.Adult:
                    tamagotchi.Experience = 100;
                    break;
            }

            _context.Add(tamagotchi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(tamagotchi);
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

        if (shoppingItem is Tamagotchi)
        {
            return RedirectToAction("EditTamagotchi", shoppingItem);
        }

        return View(shoppingItem);
    }

    // POST: ShoppingItems/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ShoppingItemId,Name,Description,Stock,Price,CurrencyType,Discount,ImagePath,Quantity")] ShoppingItem shoppingItem)
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

    public async Task<IActionResult> EditTamagotchi(ShoppingItem shoppingItem)
    {
        if (shoppingItem == null)
        {
            return NotFound();
        }
        Tamagotchi tamagotchi = await _context.Tamagotchis.FindAsync(shoppingItem.ShoppingItemId);

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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTamagotchi(int ShoppingItemId, [Bind("TamagotchiColor,TamagotchiType,Mood,TamagotchiStage,Experience,ShoppingItemId,Name,Description,Stock,Price,CurrencyType,Discount,ImagePath,Quantity")] Tamagotchi tamagotchi)
    {
        if (ShoppingItemId != tamagotchi.ShoppingItemId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tamagotchi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(tamagotchi.ShoppingItemId))
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
        return View(tamagotchi);
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