using HyperTamagotchi_MVC.Repositories;

namespace HyperTamagotchi_MVC.Middleware;

public class JwtMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, IJwtTokenValidator tokenValidator)
    {
        var token = context.Request.Cookies["jwtToken"];

        if (!string.IsNullOrEmpty(token))
        {
            var principal = tokenValidator.ValidateToken(token);
            if (principal != null)
            {
                context.User = principal;
            }
        }

        await _next(context);
    }
}

// Extension method to add the middleware to the pipeline
public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}