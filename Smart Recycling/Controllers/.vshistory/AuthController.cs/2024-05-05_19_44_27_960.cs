using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Smart_Recycling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private SmartRecyclingDbContext dbContext;
        public AuthController(SmartRecyclingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("/token")]
        public IActionResult Token(string email, string password)
        {
            return Json(GetResponse(email, password));
        }
    }
}
