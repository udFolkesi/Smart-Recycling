using BLL.Services;
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
        private readonly AuthService authService;
        public AuthController(SmartRecyclingDbContext dbContext, AuthService authService)
        {
            this.dbContext = dbContext;
            this.authService = authService;
        }

        [HttpPost("/token")]
        public IActionResult Token(string email, string password)
        {
            var identity = authService.GetIdentity(email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            return Json(authService.GetResponse(email, password, identity));
        }
    }
}
