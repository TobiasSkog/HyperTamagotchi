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

    public bool IsUserInRole(string role)
    {
        return _contextAccessor.HttpContext.User.IsInRole(role);

        // Used like this
        //if (!_api.IsUserInRole("Admin")) <- Input your desired roll
        //{
        //    return RedirectToAction("AccessDenied", "Account"); <- The View and Controller you want to direct the user to
        //}
    }
    //public (string Controller, string View) IsUserInRole(string role)
    //{
    //    DenyAccessDto userPermission = new() { IsUserAllowed = _contextAccessor.HttpContext.User.IsInRole(role) };

    //    if (!userPermission.IsUserAllowed)
    //    {
    //        return new("Account", userPermission.RedirectUrl);
    //    }

    //    return new("", "");
    //}
}


