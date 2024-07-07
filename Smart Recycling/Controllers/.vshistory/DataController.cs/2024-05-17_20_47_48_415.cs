using Microsoft.AspNetCore.Mvc;

namespace SmartRecycling.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
