using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyperTamagotchi_API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController(
    UserManager<IdentityUser> userManager,
    ApplicationDbContext context,
    IJwtService jwtService,
    SignInManager<IdentityUser> signInManager) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signinManager = signInManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtService _jwtService = jwtService;

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
                await _signinManager.SignInAsync(customer, false);
                return Ok("Customer account successfully created!");
            }
        }
        return BadRequest("Customer registration failed.");

    }

    //Post: /api/Auth/Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "No user found with that email address.");

            return BadRequest();
        }

        var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (!checkPasswordResult)
        {
            await _userManager.AccessFailedAsync(user);
            ModelState.AddModelError(string.Empty, "Incorrect Username or Password.");

            return BadRequest();
        }

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Any())
        {
            await _userManager.AddToRoleAsync(user, "Customer");
            roles = ["Customer"];
        }
        var loginResponse = await _signinManager.PasswordSignInAsync(loginRequestDto.Username, loginRequestDto.Password, loginRequestDto.RememberMe, false);

        if (!loginResponse.Succeeded)
        {
            return Unauthorized();
        }

        var token = _jwtService.CreateJWTToken(user, [.. roles], loginRequestDto.RememberMe);

        var response = new LoginResponseDto
        {
            AccessToken = token
        };

        return Ok(response);
    }

    //Post: /api/Auth/Refresh/{tokenRequest}
    [HttpPost]
    [Route("Refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
    {
        var principal = _jwtService.GetPrincipalFromExpiredToken(tokenRequest.Token);
        if (principal == null)
        {
            return BadRequest("Invalid token.");
        }

        Customer? user = await _userManager.FindByEmailAsync(principal.Identity.Name) as Customer;

        if (user == null)
        {
            return BadRequest("Invalid email.");
        }

        // Validate the refresh token
        var validRefreshToken = _jwtService.ValidateRefreshToken(user, tokenRequest.RefreshToken);
        if (!validRefreshToken)
        {
            return BadRequest("Invalid refresh token");
        }

        var newJwtToken = _jwtService.GenerateJwtToken(principal.Claims);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        // Save the new refresh token
        SaveRefreshToken(user.Id, newRefreshToken);

        return Ok(new
        {
            token = newJwtToken,
            refreshToken = newRefreshToken
        });
    }
    private async void SaveRefreshToken(string userId, string refreshToken)
    {
        var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == userId);
        if (customer == null)
        {
            return;
        }
        customer.RefreshToken = refreshToken;
        _context.Update(customer);
        await _context.SaveChangesAsync();
    }
}