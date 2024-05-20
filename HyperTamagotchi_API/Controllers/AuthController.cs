using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using HyperTamagotchi_API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HyperTamagotchi_API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController(
    UserManager<IdentityUser> userManager,
    ApplicationDbContext context,
    IJwtService jwtService,
    SignInManager<IdentityUser> signInManager,
    IConfiguration configuration) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signinManager = signInManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IConfiguration _configuration = configuration;

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
        var principal = GetPrincipalFromExpiredToken(tokenRequest.Token);
        if (principal == null)
        {
            return BadRequest("Invalid token.");
        }

        var user = await _userManager.FindByEmailAsync(principal.Identity.Name);

        if (user == null)
        {
            return BadRequest("Invalid email.");
        }

        // Validate the refresh token
        var validRefreshToken = await ValidateRefreshToken(user.Id, tokenRequest.RefreshToken);
        if (!validRefreshToken)
        {
            return BadRequest("Invalid refresh token");
        }

        var newJwtToken = GenerateJwtToken(principal.Claims);
        var newRefreshToken = GenerateRefreshToken();

        // Save the new refresh token
        SaveRefreshToken(user.Id, newRefreshToken);

        return Ok(new
        {
            token = newJwtToken,
            refreshToken = newRefreshToken
        });
    }
    private async Task<bool> ValidateRefreshToken(string userId, string refreshToken)
    {
        // Perform validation logic here (e.g., check expiration, match against stored token, etc.)
        // Return true if the refresh token is valid; otherwise, return false

        var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == userId);
        if (customer == null)
        {
            return false;
        }

        return refreshToken == customer.RefreshToken;
    }

    private void SaveRefreshToken(string userId, string refreshToken)
    {
        // Save the refresh token in a secure storage (e.g., database) associated with the user ID
    }
    private string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}