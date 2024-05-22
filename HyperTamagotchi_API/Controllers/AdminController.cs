using AutoMapper;
using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Filters;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_API.Models.TamagotchiProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Controllers;

//// api/Admin
[Route("api/[controller]")]
[ApiController]
[AuthorizeByRole("Admin")]
public class AdminController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    //// POST: api/Admin/ShoppingItems/AddDiscountToShoppingItems/{discountUpdateModel}
    [HttpPost]
    [Route("AddDiscountToShoppingItems")]
    public async Task<IActionResult> AddDiscountToShoppingItems([FromBody] DiscountUpdateModel discountUpdateModel)
    {

        if (discountUpdateModel.SelectedShoppingItems == null || discountUpdateModel.SelectedShoppingItems.Count <= 0 ||
            (discountUpdateModel.DiscountPercentage == null && discountUpdateModel.DiscountPercentage >= 0 && discountUpdateModel.DiscountPercentage <= 100))
        {
            return BadRequest();
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

    //// POST: api/Admin/ShoppingItems/CreateShoppingItem/{shoppingItemDto}
    [HttpPost]
    [Route("CreateShoppingItem")]
    public async Task<IActionResult> Create([FromBody] ShoppingItemDto shoppingItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Update to a real Image Path based on our project folders and that the user only enters the image name in the input
        var shoppingItem = _mapper.Map<ShoppingItem>(shoppingItemDto);
        shoppingItem.ImagePath = Path.Combine("/Assets/ShoppingItem/", shoppingItem.ImagePath);

        _context.Add(shoppingItem);
        await _context.SaveChangesAsync();

        return Ok("Shopping Item Created Successfully.");
    }

    //// POST: api/Admin/ShoppingItems/CreateTamagotchi/{tamagotchiDto}
    [HttpPost]
    [Route("CreateTamagotchi")]
    public async Task<IActionResult> CreateTamagotchi([FromBody] TamagotchiDto tamagotchiDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Update to a real Image Path based on our project folders and that the user only enters the image name in the input
        var tamagotchi = _mapper.Map<Tamagotchi>(tamagotchiDto);
        tamagotchi.ImagePath = Path.Combine("/Assets/Tamagotchi/", tamagotchi.ImagePath);

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

        return Ok("Tamagotchi Created Successfully.");
    }

    //// POST: api/Admin/ShoppingItems/EditShoppingItem/{shoppingItem}
    [HttpPost]
    [Route("EditShoppingItem")]
    public async Task<IActionResult> Edit([FromBody] ShoppingItem shoppingItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        _context.Update(shoppingItem);
        await _context.SaveChangesAsync();

        return Ok("Shopping Item Updated Successfully.");
    }

    //// POST: api/Admin/ShoppingItems/EditTamagotchi/{tamagotchi}
    [HttpPost]
    [Route("EditTamagotchi")]
    public async Task<IActionResult> EditTamagotchi([FromBody] Tamagotchi tamagotchi)
    {
        if (tamagotchi == null)
        {
            return BadRequest();
        }

        _context.Update(tamagotchi);

        await _context.SaveChangesAsync();

        return Ok("Tamagotchi Updated Successfully.");
    }

    //// POST: api/Admin/ShoppingItems/Delete/{id}
    [HttpDelete]
    [Route("Delete{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var shoppingItem = await _context.ShoppingItems.FindAsync(id);
        if (shoppingItem != null)
        {
            _context.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();
            return Ok("Item Deleted Successfully.");
        }

        return NotFound("Item not found.");
    }


    // DELETE: api/Orders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}