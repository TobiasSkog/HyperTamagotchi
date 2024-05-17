using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;
public class AccountController : Controller
{
    private readonly ApiServices _api;
    public AccountController(ApiServices api)
    {
        _api = api;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestDto loginRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _api.LoginAsync(loginRequest.Username, loginRequest.Password, loginRequest.RememberMe);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(loginRequest);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto registerRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _api.RegisterAsync(registerRequest.Password, registerRequest.ConfirmPassword, registerRequest.Email, registerRequest.FirstName, registerRequest.LastName, registerRequest.StreetAddress, registerRequest.City, registerRequest.ZipCode);
            if (result)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError(string.Empty, "Registration failed.");
        }
        return View(registerRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Remove the JWT token from cookies
        Response.Cookies.Delete("jwtToken");

        // Sign out the user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Claims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Json(claims);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
