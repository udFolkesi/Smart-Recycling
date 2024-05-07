using AutoMapper;
using BLL.Services;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace SmartRecycling.Controllers
{
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
