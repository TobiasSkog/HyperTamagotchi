using Newtonsoft.Json;
using System.Net;

namespace HyperTamagotchi_MVC.Models;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occured");
            await HandleExceptionAsync(httpContext, ex);

        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new
        {
            error = "An unexpected error occured.",
            details = ex.Message
        });

        return httpContext.Response.WriteAsync(result);
    }
}
