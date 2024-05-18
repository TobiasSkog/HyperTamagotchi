using HyperTamagotchi_MVC.Repositories;
using System.Net.Http.Headers;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices(IHttpClientFactory httpFactory, IHttpContextAccessor contextAccessor, IJwtTokenValidator jwtValidator)
{
    private readonly HttpClient _client = httpFactory.CreateClient("API Tamagotchi");
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IJwtTokenValidator _jwtValidator = jwtValidator;

    private void AddJwtTokenToRequest()
    {
        var token = _contextAccessor.HttpContext.Request.Cookies["jwtToken"];
        if (!string.IsNullOrEmpty(token))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private bool IsUserInRole(string role)
    {
        return _contextAccessor.HttpContext.User.IsInRole(role);
    }







}


