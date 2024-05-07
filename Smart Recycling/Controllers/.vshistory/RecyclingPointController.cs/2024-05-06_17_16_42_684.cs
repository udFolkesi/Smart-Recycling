using Microsoft.AspNetCore.Mvc;

namespace SmartRecycling.Controllers
{
    public class RecyclingControllerPoint : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
