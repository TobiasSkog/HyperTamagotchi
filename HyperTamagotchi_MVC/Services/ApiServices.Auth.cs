using HyperTamagotchi_MVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyperTamagotchi_MVC.Services;

public partial class ApiServices
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<bool> LoginAsync(string username, string password, bool rememberMe)
    {
        var response = await _client.PostAsJsonAsync("api/Auth/Login", new LoginRequestDto { Username = username, Password = password, RememberMe = rememberMe });

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<ApiToken>(content);

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.AccessToken))
            {

                var principal = _jwtValidator.ValidateToken(loginResponse.AccessToken);

                if (principal == null)
                {
                    return false;
                }

                _contextAccessor.HttpContext.User = principal;

                AppendCookie("jwtToken", loginResponse.AccessToken, rememberMe);
                AddJwtTokenToRequest(loginResponse.AccessToken);

                var shoppingCart = await GetShoppingCartById();
                var shoppingCartJson = JsonConvert.SerializeObject(shoppingCart);
                AppendCookie("ShoppingCart", shoppingCartJson, rememberMe);

                return true;
            }
        }

        return false;
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<bool> RegisterAsync(string password, string confirmPassword, string email, string firstName, string lastName, string streetAddress, string city, string zipCode)
    {
        var payload = new
        {
            email,
            password,
            confirmPassword,
            username = email,
            firstName,
            lastName,
            streetAddress,
            city,
            zipCode
        };

        var response = await _client.PostAsJsonAsync("api/Auth/Register", payload);
        return response.IsSuccessStatusCode;
    }

}
