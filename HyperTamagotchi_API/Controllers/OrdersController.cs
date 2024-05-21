using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Filters;
using HyperTamagotchi_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        // GET: api/Orders
        [HttpGet("{customerId}")]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersFromCustomer(string customerId)
        {
            return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{itemId}")]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<ActionResult<Order>> GetOrder(int itemId)
        {
            var order = await _context.Orders.FindAsync(itemId);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
