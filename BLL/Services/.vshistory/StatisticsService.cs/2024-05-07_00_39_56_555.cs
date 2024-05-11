using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = iTextSharp.text.Document;

namespace BLL.Services
{
    public class StatisticsService
    {
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
    }
}
