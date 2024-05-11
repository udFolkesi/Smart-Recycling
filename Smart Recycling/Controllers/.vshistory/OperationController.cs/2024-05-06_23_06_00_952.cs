using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using SmartRecycling.Dto;

namespace SmartRecycling.Controllers
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
        public async Task<IActionResult> CreateOperation(OperationDto operation)
        {
            var operationrMap = _mapper.Map<Operation>(operation);

            await dbContext.Operation.AddAsync(operationrMap);
            await dbContext.SaveChangesAsync();

            return Ok(operation);
        }
    }
}
