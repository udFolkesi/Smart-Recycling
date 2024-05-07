using Microsoft.AspNetCore.Mvc;

namespace SmartRecycling.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
