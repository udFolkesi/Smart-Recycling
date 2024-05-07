using Microsoft.AspNetCore.Mvc;

namespace Smart_Recycling.Controllers
{
    public class CollectionPointController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
