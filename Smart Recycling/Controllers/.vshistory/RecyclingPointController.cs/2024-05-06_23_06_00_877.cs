using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace SmartRecycling.Controllers
{
    public class RecyclingPointController : BaseController
    {
        public RecyclingPointController(SmartRecyclingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IEnumerable<RecyclingPoint> GetRecyclingPoints()
        {
            return dbContext.RecyclingPoint.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(RecyclingPointStatistics pointStatistics)
        {
            await dbContext.RecyclingPointStatistics.AddRangeAsync(pointStatistics);
            await dbContext.SaveChangesAsync();

            return Ok(pointStatistics);
        }
    }
}
