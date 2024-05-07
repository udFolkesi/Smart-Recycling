using CORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Recycling.Dto;

namespace Smart_Recycling.Controllers
{
    public class UserStatisticsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserStatistics(UserDto user)
        {
            var existingUser = await dbContext.User.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                ModelState.AddModelError("", "User with such email already exists");
                return StatusCode(422, ModelState);
            }

            var userMap = _mapper.Map<User>(user);

            await dbContext.User.AddAsync(userMap);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
