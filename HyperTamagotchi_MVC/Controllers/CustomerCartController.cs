using HyperTamagotchi_MVC.Models;
using HyperTamagotchi.Common.DTO;
using HyperTamagotchi_MVC.Models.DTO;
using HyperTamagotchi_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HyperTamagotchi_MVC.Controllers
{
    public class CustomerCartController : Controller
    {
        private readonly ApiServices _api;

        public CustomerCartController(ApiServices api)
        {
            _api = api;
        }

        private ShoppingCart GetShoppingCartFromCookie()
        {
            var cookie = Request.Cookies["ShoppingCart"];
            if (string.IsNullOrEmpty(cookie))
            {
                return new ShoppingCart { ShoppingItems = new List<ShoppingItem>() };
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(cookie);
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

        public IActionResult Index()
        {
            var shoppingCart = GetShoppingCartFromCookie();
            SetShoppingCartInViewBag(shoppingCart);
            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ShoppingCart model)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            try
            {
                var user = await _api.GetUserByEmailAsync(email);
                var userId = user?.Id;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var checkoutModel = new CheckoutModel
                {
                    CustomerId = userId,
                    Items = model.ShoppingItems.Select(item => new CheckoutItemDto
                    {
                        ShoppingItemId = item.ShoppingItemId,
                        Quantity = (int)item.Quantity
                    }).ToList()
                };

                var order = await _api.CheckoutAsync(checkoutModel);

                var viewModel = new OrderConfirmationViewModel
                {
                    OrderId = order.OrderId,
                    CustomerEmail = email,
                    OrderDate = order.OrderDate,
                    Items = model.ShoppingItems
                };

                UpdateShoppingCartCookie(model);


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
            Response.Cookies.Delete("ShoppingCart");
            return RedirectToAction("Index", "Home");
        }
    }
}