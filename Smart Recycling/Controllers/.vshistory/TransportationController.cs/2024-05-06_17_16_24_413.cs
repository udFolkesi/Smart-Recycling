using Microsoft.AspNetCore.Mvc;

namespace SmartRecycling.Controllers
{
    public class TransportationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
