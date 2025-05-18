using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
