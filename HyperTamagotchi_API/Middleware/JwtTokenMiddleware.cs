using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HyperTamagotchi_API.Middleware;

public class JwtTokenMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly string _issuer = configuration["Jwt:Issuer"];
    private readonly string _audience = configuration["Jwt:Audience"];
    private readonly string _key = configuration["Jwt:Key"];

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Cookies["Authorization"];
        token ??= context.Request.Cookies["jwtToken"];

        if (!string.IsNullOrEmpty(token))
        {
            var principal = ValidateToken(token);
            if (principal != null)
            {
                context.User = principal;
            }
        }

        await _next(context);
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);

        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        }, out SecurityToken validatedToken);

        return principal;
    }
}
public static class JwtTokenMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtTokenMiddleware>();
    }
}