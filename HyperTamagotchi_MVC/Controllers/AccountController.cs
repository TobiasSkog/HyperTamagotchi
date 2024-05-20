﻿using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers;
public class AccountController(ApiServices api) : Controller
{
    private readonly ApiServices _api = api;

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
        _api.EnsureJwtTokenIsAddedToRequest();
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

    //[HttpGet]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Remove the JWT token from cookies
        Response.Cookies.Delete("jwtToken");
        Response.Cookies.Delete("ShoppingCart");
        Response.Cookies.Delete(".AspNetCore.Identity.Application");

        // Sign out the user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }


    //Can be removed, just a simple way to debug to see what Claims the current user have
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