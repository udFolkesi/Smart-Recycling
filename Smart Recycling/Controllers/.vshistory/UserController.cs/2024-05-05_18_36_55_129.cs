using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Smart_Recycling.Controllers
{
    public class UserController : Controller
    {
        private readonly SmartRecyclingDbContext dbContext;
        private readonly IMapper _mapper;

        public UserController(SmartRecyclingDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var createdUser = await dbContext.User.Where(u => u.Email == createdUser.Email).FirstOrDefaultAsync();
            if (createdUser != null)
            {
                ModelState.AddModelError("", "User with such email already exists");
                return StatusCode(422, ModelState);
            }

            await dbContext.User.AddAsync(createdUser);
            await dbContext.SaveChangesAsync();

            return Ok(createdUser);
        }
    }
}
