using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
