using AutoMapper;
using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using HyperTamagotchi.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HyperTamagotchi_API.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HyperTamagotchi_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerCartController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerCartController(ApplicationDbContext context, ILogger<CustomerCartController> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

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
                    _logger.LogError($"Shopping cart with ID {shoppingCartId} not found.");
                    return NotFound("Shopping cart not found.");
                }

                var cartItem = shoppingCart.Items.FirstOrDefault(ci => ci.ShoppingItemId == itemId);
                if (cartItem == null)
                {
                    _logger.LogError($"Item with ID {itemId} not found in cart.");
                    return NotFound("Item not found in cart.");
                }

                _context.ShoppingItemShoppingCarts.Remove(cartItem);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the item from the cart.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user.Id, user.Email });
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutViewModel checkoutModel)
        {
            if (checkoutModel == null || !checkoutModel.Items.Any())
            {
                return BadRequest("Invalid order data.");
            }

            var customer = await _context.Customer
                .Include(c => c.ShoppingCart)
                .ThenInclude(sc => sc.Items)
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

            var shoppingItemOrders = checkoutModel.Items.Select(item => new ShoppingItemOrder
            {
                ShoppingItemId = item.ShoppingItemId,
                Order = order 
            }).ToList();

            order.Items = shoppingItemOrders;

            _context.Orders.Add(order);

            // Remove items from the cart
            var cartItems = customer.ShoppingCart.Items
                .Where(ci => checkoutModel.Items.Any(cm => cm.ShoppingItemId == ci.ShoppingItemId)).ToList();

            _context.ShoppingItemShoppingCarts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            // Create the OrderDto to return
            var orderDto = new OrderDto
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
    }
}



