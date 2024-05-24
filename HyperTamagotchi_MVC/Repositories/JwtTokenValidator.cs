using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HyperTamagotchi_MVC.Repositories;

public class JwtTokenValidator(IConfiguration configuration) : IJwtTokenValidator
{
    private readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? configuration["Jwt:Issuer"];
    private readonly string _audience = Environment.GetEnvironmentVariable("JwtAudience") ?? configuration["Jwt:Audience"];
    private readonly string _jwtkey = Environment.GetEnvironmentVariable("JwtKey") ?? configuration["Jwt:Key"];
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtkey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
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
}
