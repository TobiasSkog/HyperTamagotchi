using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Filters;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HyperTamagotchi_API.Controllers;
[ApiController]
[Route("api/[controller]")]
[AuthorizeByRole("Admin", "Customer")]
public class CustomerCartController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
    // private readonly ILogger<CustomerCartController> _logger = logger;

    [HttpDelete("{shoppingCartId}/{itemId}")]
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
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _context.Customer.FirstOrDefaultAsync(c => c.Email == email);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(new { user.Id, user.Email });
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutViewModel checkoutModel)
    {
        //  Vi måste få till att när vi checkar ut
        //  så vill vi också uppdatera stocken i
        //  databasen med de nuvarande itemet i
        // databasen stock - checkoutModel.Quantity

        // Vi skulle skickat in hela customer carten
        // och skapat en Order i APIet och sedan skicka
        // tillbaka den färdiga ordern.
        // På så sätt får vi in alla shoppingitems kunden
        // vill köpa, vi får in quantityn, vi skapar en Order
        // direkt i databasen med all data vi behöver och
        // undviker att skapa 59 olika DTOs som ska skickas
        // fram och tillbaka!

        if (checkoutModel == null || checkoutModel.Items.Count == 0)
        {
            return BadRequest("Invalid order data.");
        }

        var customer = await _context.Customer
            .Include(c => c.ShoppingCart)
            .ThenInclude(sc => sc.Items)
            .ThenInclude(i => i.ShoppingItem)
            .FirstOrDefaultAsync(c => c.Id == checkoutModel.CustomerId);

        if (customer == null)
        {
            return NotFound("Customer not found.");
        }

        var order = new Order
        {
            CustomerId = checkoutModel.CustomerId,
            OrderDate = DateTime.UtcNow
        };

        var shoppingItemOrders = checkoutModel.Items
            .Select(item => new ShoppingItemOrder
            {
                ShoppingItemId = item.ShoppingItemId,
                Order = order
            }).ToList();

        order.Items = shoppingItemOrders;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var cartItems = customer.ShoppingCart.Items
            .Where(ci => checkoutModel.Items.Any(cm => cm.ShoppingItemId == ci.ShoppingItemId))
            .ToList();

        // Create the OrderDto to return
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



