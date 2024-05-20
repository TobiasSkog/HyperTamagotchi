using Microsoft.AspNetCore.Mvc;

namespace HyperTamagotchi_MVC.Controllers
{
    public class CustomerCart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
