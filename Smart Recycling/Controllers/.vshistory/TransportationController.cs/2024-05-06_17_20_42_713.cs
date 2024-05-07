using CORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRecycling.Dto;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransportationController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(TransportationDto transportation)
        {
            var operationrMap = _mapper.Map<Transportation>(transportation);

            await dbContext.Operation.AddAsync(operationrMap);
            await dbContext.SaveChangesAsync();

            return Ok(transportation);
        }
    }
}
