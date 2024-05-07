using Microsoft.AspNetCore.Mvc;

namespace Smart_Recycling.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
