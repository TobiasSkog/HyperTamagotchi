using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
