using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Repositories;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor, IJwtTokenValidator jwtValidator, ILogger<ApiServices> logger)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IJwtTokenValidator _jwtValidator = jwtValidator;
    private readonly HttpClient _client = httpClientFactory.CreateClient("API Tamagotchi");

    private void AddJwtTokenToRequest(string token)
    {
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
    public void EnsureJwtTokenIsAddedToRequest()
    {
        if (!_client.DefaultRequestHeaders.Contains("Authorization"))
        {
            var token = _contextAccessor.HttpContext.Request.Cookies["jwtToken"];
            token ??= _contextAccessor.HttpContext.Request.Cookies["Authorization"];

            if (!string.IsNullOrEmpty(token))
            {
                AddJwtTokenToRequest(token);
            }
        }
    }
    private void AppendCookie(string key, string value, bool rememberMe)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(1)
        };

        _contextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
    }
    public void UpdateShopingCartCookie(ShoppingCart shoppingCart)
    {
        var shoppingCartJson = JsonConvert.SerializeObject(shoppingCart);
        bool rememberMe = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "RememberMe").Value == "TRUE";

        AppendCookie("ShoppingCart", shoppingCartJson, rememberMe);
    }
    private class CustomClaimShoppingCart(string shoppingCartId)
    {
        public static string ClaimName { get; set; } = "ShoppingCartId";
        public string ClaimValue { get; set; } = shoppingCartId;
    }

    private class ApiToken
    {
        public string AccessToken { get; set; }
    }
}


