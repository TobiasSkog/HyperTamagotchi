namespace HyperTamagotchi_API.Middleware;

public class DebugRequestHeadersMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        // Log request headers to the console
        foreach (var header in context.Request.Headers)
        {
            Console.WriteLine($"{header.Key}: {header.Value}");
        }

        await _next(context);
    }
}

public static class DebugRequestHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseDebugRequestHeadersMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DebugRequestHeadersMiddleware>();
    }
}