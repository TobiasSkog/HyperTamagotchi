using AutoMapper;
using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Filters;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        // GET: api/ShoppingCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(int id)
        {
            try
            {
                var shoppingCart = await _context.ShoppingCarts
                    .Include(sc => sc.Items)
                        .ThenInclude(si => si.ShoppingItem)
                    .FirstOrDefaultAsync(sc => sc.ShoppingCartId == id);

                if (shoppingCart == null)
                {
                    return NotFound();
                }

                var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);

                return shoppingCartDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/ShoppingCarts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCart(int id, ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.ShoppingCartId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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

        // POST: api/ShoppingCarts
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> PostShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.ShoppingCartId }, shoppingCart);
        }

        // DELETE: api/ShoppingCarts/5
        [HttpDelete("{id}")]
        [AuthorizeByRole("Admin")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCarts.Any(e => e.ShoppingCartId == id);
        }
    }
}
