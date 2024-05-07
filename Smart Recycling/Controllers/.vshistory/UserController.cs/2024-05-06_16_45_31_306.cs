using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRecycling.Dto;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly SmartRecyclingDbContext dbContext;
        private readonly IMapper _mapper;

        public UserController(SmartRecyclingDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                IQueryable<User> query = dbContext.User
                    .Include(u => u.UserStatistics)
                    .Include(u => u.Operations);

                var user = await query.FirstOrDefaultAsync(c => c.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto user)
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
