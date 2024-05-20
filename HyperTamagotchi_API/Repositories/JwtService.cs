using HyperTamagotchi_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace HyperTamagotchi_API.Repositories;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration _configuration = configuration;

    private TokenValidationParameters GetTokenValidation()
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!))
        };
    }

    public string CreateJWTToken(IdentityUser user, List<string> roles, bool rememberMe)
    {
        CustomClaimRememberMe customClaimRememberMe = new(rememberMe.ToString().ToUpper());

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(customClaimRememberMe.ClaimName, customClaimRememberMe.ClaimValue)
        };

        if (user is Customer customer)
        {
            string shoppingCartId = customer.ShoppingCartId.ToString();

            CustomClaimShoppingCart customClaimShoppingCart = new(shoppingCartId);

            claims.Add(new Claim(customClaimShoppingCart.ClaimName, customClaimShoppingCart.ClaimValue));
        }

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = GetTokenValidation();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        var roleClaims = jwtToken?.Claims.Where(claim => claim.Type == ClaimTypes.Role).ToList();

        if (roleClaims != null)
        {
            var claimsIdentity = new ClaimsIdentity(roleClaims, "jwtToken");
            principal.AddIdentity(claimsIdentity);
        }

        return principal;
    }
    private class CustomClaimShoppingCart(string shoppingCartId)
    {
        public string ClaimName { get; set; } = "ShoppingCartId";
        public string ClaimValue { get; set; } = shoppingCartId;
    }
    private class CustomClaimRememberMe(string RememberMe)
    {
        public string ClaimName { get; set; } = "RememberMe";
        public string ClaimValue { get; set; } = RememberMe;
    }
}

