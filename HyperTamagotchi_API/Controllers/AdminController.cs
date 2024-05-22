using AutoMapper;
using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Filters;
using HyperTamagotchi_API.Helpers.ExchangeRate;
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

    //// POST: api/Admin/AddDiscountToShoppingItems/{discountUpdateModel}
    [HttpPost]
    [Route("AddDiscountToShoppingItems")]
    public async Task<IActionResult> AddDiscountToShoppingItems([FromBody] DiscountUpdateModel discountUpdateModel)
    {

        if (discountUpdateModel.SelectedShoppingItems == null || discountUpdateModel.SelectedShoppingItems.Count <= 0 ||
            (discountUpdateModel.DiscountValue == null && discountUpdateModel.DiscountValue >= 0 && discountUpdateModel.DiscountValue <= 100))
        {
            return BadRequest("Pasta");
        }
        var foundShoppingItems = await _context.ShoppingItems
            .Where(si => discountUpdateModel.SelectedShoppingItems.Contains(si.ShoppingItemId))
            .ToListAsync();

        foreach (var item in foundShoppingItems)
        {
            item.Discount = (float)discountUpdateModel.DiscountValue!;
            _context.Update(item);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    //// POST: api/Admin/CreateShoppingItem/{shoppingItemDto}
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

    //// POST: api/Admin/CreateTamagotchi/{tamagotchiDto}
    [HttpPost]
    [Route("CreateTamagotchi")]
    public async Task<IActionResult> CreateTamagotchi([FromBody] TamagotchiDto tamagotchiDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Update to a real Image Path based on our project folders and that the user only enters the image name in the input
        Tamagotchi tamagotchi = new()
        {
            Name = tamagotchiDto.Name,
            Description = tamagotchiDto.Description,
            Stock = tamagotchiDto.Stock,
            Price = tamagotchiDto.Price,
            CurrencyType = CurrencyType.SEK,
            Discount = 1.00f,
            ImagePath = $"Assets/Tamagotchi/{tamagotchiDto.TamagotchiType}/{tamagotchiDto.TamagotchiType}_{tamagotchiDto.TamagotchiStage}_{tamagotchiDto.TamagotchiColor}.png",
            TamagotchiColor = tamagotchiDto.TamagotchiColor,
            TamagotchiType = tamagotchiDto.TamagotchiType,
            Mood = tamagotchiDto.Mood,
            TamagotchiStage = tamagotchiDto.TamagotchiStage,
        };

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

    //// POST: api/Admin/EditShoppingItem/{shoppingItem}
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

    //// POST: api/Admin/EditTamagotchi/{tamagotchi}
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

    //// POST: api/Admin/DeleteItem/{id}
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var shoppingItem = await _context.ShoppingItems.FindAsync(id);
        if (shoppingItem == null)
        {
            return NotFound("Item not found.");
        }
        if (shoppingItem is Tamagotchi)
        {
            var tamagotchi = await _context.Tamagotchis.FindAsync(id);
            _context.Remove(tamagotchi!);
            await _context.SaveChangesAsync();
        }
        else
        {
            _context.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();
        }

        return Ok("Item Deleted Successfully.");
    }

    [HttpGet]
    [Route("GetAllOrders")]
    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {

        var orders = await _context.Orders.ToListAsync();
        var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return ordersDto;

    }

    [HttpGet]
    [Route("GetSpecificOrder/{id}")]
    public async Task<OrderDto> GetSpecificOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        var orderDto = _mapper.Map<OrderDto>(order);

        return orderDto;
    }
    [HttpGet]
    [Route("GetItemToEdit/{id}")]
    public async Task<IActionResult> GetItemToEdit(int id)
    {
        var item = await _context.ShoppingItems.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        if (item is Tamagotchi tamagotchi)
        {
            return Ok(new { Type = ItemType.Tamagotchi, Data = tamagotchi });
        }
        else
        {
            return Ok(new { Type = ItemType.ShoppingItem, Data = item });
        }
    }
    public enum ItemType
    {
        Tamagotchi,
        ShoppingItem
    }
    [HttpGet]
    [Route("GetOrder{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
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