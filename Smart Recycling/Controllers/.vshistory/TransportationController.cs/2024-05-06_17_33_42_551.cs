using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRecycling.Dto;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransportationController : BaseController
    {
        public TransportationController(SmartRecyclingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(TransportationDto transportation)
        {
            var transportationMap = _mapper.Map<Transportation>(transportation);

            await dbContext.Operation.AddAsync(transportationMap);
            await dbContext.SaveChangesAsync();

            return Ok(transportation);
        }
    }
}
