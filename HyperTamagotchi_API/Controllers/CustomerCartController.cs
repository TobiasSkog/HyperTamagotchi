using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_API.Models.GoogleMaps;
using HyperTamagotchi_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HyperTamagotchi_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomerCartController(ApplicationDbContext context, TimeDelivery timeDelivery) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
    private readonly TimeDelivery _timeDelivery = timeDelivery;

    /*[HttpDelete("{shoppingCartId}/{itemId}")]
    public async Task<IActionResult> RemoveItem(int shoppingCartId, int itemId)
    {
        try
        {
            var shoppingCart = await _context.ShoppingCarts
                                             .Include(sc => sc.Items)
                                             .ThenInclude(sic => sic.ShoppingItem)
                                             .FirstOrDefaultAsync(sc => sc.ShoppingCartId == shoppingCartId);

            if (shoppingCart == null)
            {
                // _logger.LogError($"Shopping cart with ID {shoppingCartId} not found.");
                return NotFound("Shopping cart not found.");
            }

            var cartItem = shoppingCart.Items.FirstOrDefault(ci => ci.ShoppingItemId == itemId);
            if (cartItem == null)
            {
                //_logger.LogError($"Item with ID {itemId} not found in cart.");
                return NotFound("Item not found in cart.");
            }

            _context.ShoppingItemShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An error occurred while removing the item from the cart.");
            return StatusCode(500, "Internal server error");
        }
    }*/



    [HttpGet("item/{shoppingItemId}/stock")]
    public async Task<IActionResult> GetItemStock(int shoppingItemId)
    {
        var shoppingItem = await _context.ShoppingItems.FindAsync(shoppingItemId);

        if (shoppingItem == null)
        {
            return NotFound($"Shopping item with ID {shoppingItemId} not found.");
        }

        return Ok(new { ShoppingItemId = shoppingItemId, Stock = shoppingItem.Stock });
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutViewModel checkoutModel)
    {
        if (checkoutModel == null || checkoutModel.Items.Count == 0)
        {
            return BadRequest("Invalid order data.");
        }

        var customer = await _context.Customer
            .Include(c => c.ShoppingCart)
            .ThenInclude(sc => sc.Items)
            .ThenInclude(i => i.ShoppingItem)
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Id == checkoutModel.CustomerId);

        if (customer == null)
        {
            return NotFound("Customer not found.");
        }

        foreach (var item in checkoutModel.Items)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(item.ShoppingItemId);
            if (shoppingItem == null)
            {
                return NotFound($"Shopping item with ID {item.ShoppingItemId} not found.");
            }
            if (shoppingItem.Stock < item.Quantity)
            {
                return BadRequest($"Insufficient stock for item ID {item.ShoppingItemId}. Current stock: {shoppingItem.Stock}, requested: {item.Quantity}.");
            }
        }
        var wareHouse = await _context.Address.FirstOrDefaultAsync(a => a.AddressId == 1);
        var origin = $"{wareHouse.StreetAddress}, {wareHouse.City}, {wareHouse.ZipCode}";
        var destination = $"{customer.Address.StreetAddress}, {customer.Address.City}, {customer.Address.ZipCode}";
        var (duration, status) = await _timeDelivery.CalculateDeliveryTime(origin, destination);

        if (status != "OK")
        {
            return BadRequest("Unable to calculate delivery time.");
        }

        var (deliveryDays, extraHours) = ParseDuration(duration);
        var shippingDate = DateTime.UtcNow.AddDays(2);
        var expectedDate = shippingDate.AddDays(deliveryDays).AddHours(extraHours);

        var order = new Order
        {
            CustomerId = checkoutModel.CustomerId,
            OrderDate = DateTime.UtcNow,
            ShippingDate = shippingDate,
            ExpectedDate = expectedDate
        };

        var shoppingItemOrders = checkoutModel.Items
            .Select(item => new ShoppingItemOrder
            {
                ShoppingItemId = item.ShoppingItemId,
                Order = order
            }).ToList();

        order.Items = shoppingItemOrders;

        foreach (var item in checkoutModel.Items)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(item.ShoppingItemId);
            shoppingItem.Stock -= (byte)item.Quantity;
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var orderDto = new OrderDtoCheckout
        {
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            ShippingDate = order.ShippingDate,
            ExpectedDate = order.ExpectedDate,
            Items = checkoutModel.Items.Select(i => new ShoppingItemOrderDTO
            {
                ShoppingItemId = i.ShoppingItemId,
                Quantity = i.Quantity
            }).ToList()
        };

        return Ok(orderDto);
    }

    private (int Days, int ExtraHours) ParseDuration(string duration)
    {
        const int HoursPerDay = 8;
        const int MinutesPerHour = 60;

        int hours = 0;
        int minutes = 0;

        var parts = duration.Split(' ');
        for (int i = 0; i < parts.Length; i += 2)
        {
            if (parts[i + 1].StartsWith("hour"))
                hours += int.Parse(parts[i]);
            else if (parts[i + 1].StartsWith("min"))
                minutes += int.Parse(parts[i]);
        }

        hours += minutes / MinutesPerHour;
        int extraMinutes = minutes % MinutesPerHour;

        int days = hours / HoursPerDay;
        int extraHours = hours % HoursPerDay;

        return (days, extraHours);
    }

    [HttpGet]
    [Route("GetAddress/{id}")]
    public async Task<ActionResult<Address>> GetAddress(int id)
    {
        var address = await _context.Address.FindAsync(id);
        if (address == null)
        {
            return NotFound("Address not found in DB");
        }
        return Ok(address);
    }
}