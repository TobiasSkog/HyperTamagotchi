using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HyperTamagotchi_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet("Email")]
    public async Task<ActionResult<CustomerDto>> GetUserByEmail()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;


        var user = await _context.Customer
            .Where(c => c.Email == email)
            .Select(c => new CustomerDto
            {
                Id = c.Id.ToString(),
                FirstName = c.FirstName,
                LastName = c.LastName,
                FullName = (c.FirstName + " " + c.LastName),
                Email = c.Email!,
                AddressId = c.AddressId,
                ShoppingCartId = c.ShoppingCartId
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
