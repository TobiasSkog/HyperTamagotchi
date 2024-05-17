using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signinManager;
    private readonly ApplicationDbContext _context;
    private readonly ITokenRepository _tokenRepository;
    public AuthController(UserManager<IdentityUser> userManager, ApplicationDbContext context, ITokenRepository tokenRepository, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _context = context;
        _tokenRepository = tokenRepository;
        _signinManager = signInManager;
    }
    //Post: /api/Auth/Register
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var address = new Address
        {
            StreetAddress = registerRequestDto.StreetAddress,
            City = registerRequestDto.City,
            ZipCode = registerRequestDto.ZipCode
        };
        await _context.AddAsync(address);
        await _context.SaveChangesAsync();
        var customer = new Customer
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            FirstName = registerRequestDto.FirstName,
            LastName = registerRequestDto.LastName,
            AddressId = address.AddressId,
            Address = address,
            EmailConfirmed = true
        };

        var identityResult = await _userManager.CreateAsync(customer, registerRequestDto.Password);
        if (identityResult.Succeeded)
        {
            identityResult = await _userManager.AddToRoleAsync(customer, "Customer");
            if (identityResult.Succeeded)
            {
                return Ok("Customer account successfully registered!");
            }
        }
        return BadRequest("Customer registration failed.");

    }
    //Post: /api/Auth/Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);


        if (user != null)
        {
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (checkPasswordResult)
            {
                // get a role for the user
                var roles = await _userManager.GetRolesAsync(user);
                if (roles != null)
                {
                    var jwttoken = _tokenRepository.CreateJWTToken(user, [.. roles]);
                    var response = new LoginResponseDto
                    {
                        AccessToken = jwttoken
                    };

                    await _signinManager.SignInAsync(user, false);

                    return Ok(response);
                }
            }
        }
        return BadRequest("Username or Password was incorrect.");
    }
}