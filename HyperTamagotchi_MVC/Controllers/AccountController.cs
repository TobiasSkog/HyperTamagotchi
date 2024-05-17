using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Services;
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
}
