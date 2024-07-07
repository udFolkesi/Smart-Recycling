using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = iTextSharp.text.Document;
using DAL.Contexts;
using CORE.Models;
using System.Web.Mvc;

namespace BLL.Services
{
    public class StatisticsService: BaseService
    {
        public StatisticsService(SmartRecyclingDbContext smartRecyclingDbContext) : base(smartRecyclingDbContext)
        {
        }

        public CollectionPointStatistics CreatePointStatistics(int point, DateOnly startDate, DateOnly endDate)
        {
            List<Operation> operations = dbContext.Operation
                .Where(o => o.CollectionPointID == point && DateOnly.FromDateTime(o.Time) >= startDate && DateOnly.FromDateTime(o.Time) <= endDate)
                .ToList();

            CollectionPointStatistics collectionPointStatistics = new()
            {
                Collected = operations.Sum(o => o.Weight),
                Recycled = 0,
                MostCollectedType = operations
                    .GroupBy(o => o.TrashType)
                    .OrderByDescending(g => g.Sum(o => o.Weight))
                    .Select(g => g.Key)
                    .FirstOrDefault(),
                Attendance = operations.Select(o => o.UserID).Count(),
                Period = startDate.ToString() + '-' + endDate.ToString(),
                CollectionPointID = point
            };

            return collectionPointStatistics;
        }

        public byte[] GeneratePdfContent(CollectionPointStatistics pointStatistics)
        {

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    PdfWriter.GetInstance(doc, ms);
                    doc.Open();
                    doc.Add(new Paragraph($"Report"));
                    doc.Close();
                }
                return ms.ToArray();
            }
        }

        public async void UpdateUserStatistics(Operation operation)
        {
            var statistics = await dbContext.UserStatistics.FindAsync(operation.UserID);
            statistics.Recycled += operation.Weight;
            Dictionary<string, double> bonusRate = new Dictionary<string, double>()
            {
                { "glass", 0.05 },
                { "plastic", 0.03 },
                { "paper", 0.02 },
                { "metal", 0.08 }
            };

            statistics.Bonuses += Convert.ToInt32(Math.Round(operation.Weight * bonusRate[operation.TrashType]));
            await dbContext.SaveChangesAsync();
        }

        public FileStreamResult GetFile(CollectionPointStatistics statistics)
        {
            byte[] pdfBytes = GeneratePdfContent(statistics);
            MemoryStream stream = new MemoryStream(pdfBytes);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"report-for:{statistics.Period}.pdf"
            };
        }
    }
}
