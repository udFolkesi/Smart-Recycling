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

namespace BLL.Services
{
    public class StatisticsService: BaseService
    {
        public StatisticsService(SmartRecyclingDbContext smartRecyclingDbContext) : base(smartRecyclingDbContext)
        {
        }

        public byte[] GeneratePdfContent(int point, string period)
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
    }
}
