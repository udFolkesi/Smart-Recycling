using AutoMapper;
using BLL.Services;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecyclingPointController : BaseController
    {
        private readonly StatisticsService statisticsService;

        public RecyclingPointController(SmartRecyclingDbContext dbContext, IMapper mapper, StatisticsService statisticsService) : base(dbContext, mapper)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public IEnumerable<RecyclingPoint> GetRecyclingPoints()
        {
            return dbContext.RecyclingPoint.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecyclingPoint>> GetRecyclingPoint(int id)
        {
            var point = await dbContext.RecyclingPoint
                .Include(p => p.Transportation)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(point == null)
            {
                return NotFound();
            }

            return Ok(point);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(RecyclingPointStatistics pointStatistics)
        {
            await dbContext.RecyclingPointStatistics.AddRangeAsync(pointStatistics);
            await dbContext.SaveChangesAsync();

            return Ok(pointStatistics);
        }

        [HttpGet]
        public IActionResult GetReport(int point, string period)
        {
            RecyclingPointStatistics recyclingPointStatistics = new()
            {
                Id = 0,
                Collected = 0,
                Recycled = 0,
                Period = period,
                RecyclingPointID = point
            };

            byte[] pdfBytes = statisticsService.GeneratePdfContent(point, period);
            MemoryStream stream = new MemoryStream(pdfBytes);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"report-for:{period}.pdf"
            };
        }
    }
}
