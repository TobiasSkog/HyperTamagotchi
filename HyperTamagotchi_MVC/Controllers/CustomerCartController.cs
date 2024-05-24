using HyperTamagotchi_MVC.Models;
using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace HyperTamagotchi_MVC.Controllers
{
    public class CustomerCartController(ApiServices api) : Controller
    {
        private readonly ApiServices _api = api;

        private ShoppingCart GetShoppingCartFromCookie()
        {
            var cookie = Request.Cookies["ShoppingCart"];
            if (string.IsNullOrEmpty(cookie))
            {
                return new ShoppingCart { ShoppingItems = [] };
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(cookie);
        }
        private void UpdateShoppingCartCookieAfterCheckout()
        {
            var shoppingCart = GetShoppingCartFromCookie();
            shoppingCart.ShoppingItems.Clear();
            UpdateShoppingCartCookie(shoppingCart);
            SetShoppingCartInViewBag(shoppingCart);
        }
        private void UpdateShoppingCartCookie(ShoppingCart shoppingCart)
        {
            var shoppingCartJson = JsonConvert.SerializeObject(shoppingCart);
            Response.Cookies.Append("ShoppingCart", shoppingCartJson, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(7)
            });
        }

        private void SetShoppingCartInViewBag(ShoppingCart shoppingCart)
        {
            ViewBag.ShoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            //var email =  User.FindFirst(ClaimTypes.Email)?.Value;
            var address = await _api.GetAddressByIdAsync(User.Claims.FirstOrDefault(c => c.Type == "AddressId").Value);

            ViewData["Customer"] = new
            {
                FullName = User.Claims.FirstOrDefault(c => c.Type == "FullName").Value,
                Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                Address = address
            };

            var shoppingCart = GetShoppingCartFromCookie();
            SetShoppingCartInViewBag(shoppingCart);
            return View(shoppingCart);

        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ShoppingCart model)
        {

            try
            {
                // Looking into removing this unecssary step
                var user = await _api.GetUserIdByEmailAsync();

                if (string.IsNullOrEmpty(user.Id))
                {
                    return Unauthorized();
                }

                var checkoutModel = new CheckoutModel
                {
                    CustomerId = user.Id,
                    Items = model.ShoppingItems.Select(item => new CheckoutItemDto
                    {
                        ShoppingItemId = item.ShoppingItemId,
                        Quantity = item.Quantity!
                    }).ToList()
                };

                var order = await _api.CheckoutAsync(checkoutModel);

                var viewModel = new OrderConfirmationViewModel
                {
                    OrderId = order.OrderId,
                    CustomerEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                    OrderDate = order.OrderDate,
                    Items = model.ShoppingItems
                };

                UpdateShoppingCartCookieAfterCheckout();

                return View("OrderConfirmation", viewModel);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, "Error communicating with the API");
            }
        }
        [HttpGet]
        public IActionResult OrderConfirmation(OrderConfirmationViewModel viewModel)
        {
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult OrderConfirmation()
        {
            // UpdateShoppingCartCookieAfterCheckout();

            return RedirectToAction("Index", "Home");
        }
    }
}