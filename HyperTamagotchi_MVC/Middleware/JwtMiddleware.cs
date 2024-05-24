using HyperTamagotchi_MVC.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace HyperTamagotchi_MVC.Middleware;

public class JwtMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly string _apiuri = Environment.GetEnvironmentVariable("TamagotchiUri") ?? configuration["ApiUri:Azure"];
    public async Task Invoke(HttpContext context, IJwtTokenValidator tokenValidator)
    {
        var token = context.Request.Cookies["jwtToken"];

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var principal = tokenValidator.ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                // Token is expired, attempt to refresh it
                var refreshToken = context.Request.Cookies["refreshToken"];
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.PostAsJsonAsync($"{_apiuri}/refresh", new { Token = token, RefreshToken = refreshToken });
                    if (response.IsSuccessStatusCode)
                    {
                        var tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
                        context.Response.Cookies.Append("jwtToken", tokens.Token);
                        context.Response.Cookies.Append("refreshToken", tokens.RefreshToken);

                        var principal = tokenValidator.ValidateToken(tokens.Token);
                        if (principal != null)
                        {
                            context.User = principal;
                        }
                    }
                }
            }
        }

        await _next(context);
    }
    private class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}