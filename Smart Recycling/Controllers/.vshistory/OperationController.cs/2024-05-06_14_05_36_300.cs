using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Smart_Recycling.Dto;

namespace Smart_Recycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OperationController : Controller
    {
        private readonly SmartRecyclingDbContext dbContext;
        private readonly IMapper _mapper;

        public OperationController(SmartRecyclingDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Operation operation)
        {
            //var userMap = _mapper.Map<User>(operation);

            await dbContext.Operation.AddAsync(operation);
            await dbContext.SaveChangesAsync();

            return Ok(operation);
        }
    }
}
