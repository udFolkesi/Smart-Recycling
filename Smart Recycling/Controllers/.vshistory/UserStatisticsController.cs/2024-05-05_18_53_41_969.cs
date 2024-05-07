using Microsoft.AspNetCore.Mvc;

namespace Smart_Recycling.Controllers
{
    public class UserStatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
