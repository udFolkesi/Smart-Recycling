using AutoMapper;
using BLL.Services;
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
        private readonly PointStateService pointStateService;

        public OperationController(SmartRecyclingDbContext dbContext, IMapper mapper, PointStateService operationService)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this.pointStateService = operationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperation(OperationDto operation)
        {
            var operationrMap = _mapper.Map<Operation>(operation);

            await dbContext.Operation.AddAsync(operationrMap);
            await dbContext.SaveChangesAsync();

            pointStateService.UpdateCollectionPointState(operationrMap);

            return Ok(operation);
        }
    }
}
