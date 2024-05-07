using Microsoft.AspNetCore.Mvc;

namespace Smart_Recycling.Controllers
{
    public class OperationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
