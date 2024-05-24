using HyperTamagotchi_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace HyperTamagotchi_API.Repositories;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? configuration["Jwt:Issuer"];
    private readonly string _audience = Environment.GetEnvironmentVariable("JwtAudience") ?? configuration["Jwt:Audience"];
    private readonly string _jwtkey = Environment.GetEnvironmentVariable("JwtKey") ?? configuration["Jwt:Key"];

    private TokenValidationParameters GetTokenValidation()
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtkey))
        };
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
            CustomClaimShoppingCart customClaimShoppingCart = new(customer.ShoppingCartId.ToString());
            CustomClaimFullName customClaimFullName = new(customer.FirstName, customer.LastName);
            CustomClaimAddressId customClaimAddressId = new(customer.AddressId.ToString());

            claims.Add(new Claim(customClaimShoppingCart.ClaimName, customClaimShoppingCart.ClaimValue));
            claims.Add(new(customClaimFullName.ClaimName, customClaimFullName.ClaimValue));
            claims.Add(new(customClaimAddressId.ClaimName, customClaimAddressId.ClaimValue));
        }

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtkey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public bool ValidateRefreshToken(Customer customer, string refreshToken)
    {
        if (customer == null)
        {
            return false;
        }

        return refreshToken == customer.RefreshToken;
    }


    public string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtkey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtkey)),
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
    private class CustomClaimFullName(string firstName, string lastName)
    {
        public string ClaimName { get; set; } = "FullName";
        public string ClaimValue { get; set; } = firstName + " " + lastName;
    }
    private class CustomClaimAddressId(string addressId)
    {
        public string ClaimName { get; set; } = "AddressId";
        public string ClaimValue { get; set; } = addressId;
    }
}

