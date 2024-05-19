using HyperTamagotchi_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

}
