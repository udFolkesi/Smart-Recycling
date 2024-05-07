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
                    .Include(c => c.Client)
                    .Include(u => u.Client.Blood)
                    .Include(u => u.Client.DonationsAsDonor)
                        .ThenInclude(d => d.BloodBank)
                    .Include(u => u.Client.DonationsAsPatient)
                        .ThenInclude(d => d.BloodBank);

                var user = await query.FirstOrDefaultAsync(c => c.UserID == id);

                if (user == null)
                {
                    return NotFound(); // 404 Not Found if client is not found
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
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
