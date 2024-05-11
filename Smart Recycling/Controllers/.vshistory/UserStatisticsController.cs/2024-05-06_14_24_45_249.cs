using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRecycling.Dto;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserStatisticsController : Controller
    {
        private readonly SmartRecyclingDbContext dbContext;

        public UserStatisticsController(SmartRecyclingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserStatistics(UserStatistics userStatistics)
        {
            await dbContext.UserStatistics.AddAsync(userStatistics);
            await dbContext.SaveChangesAsync();

            return Ok(userStatistics);
        }
    }
}
