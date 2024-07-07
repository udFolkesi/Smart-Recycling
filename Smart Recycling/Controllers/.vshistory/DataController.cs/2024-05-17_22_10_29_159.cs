using DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ExportDatabaseAsync()
        {
            string backupDirectory = @"C:\Users\USER\source\repos\Smart Recycling\DAL\Backup\";
            string backupFileName = "SmartRecycling_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);
            BackupManager.CreateBackup(backupFilePath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(backupFilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, "application/octet-stream", Path.GetFileName(backupFilePath));
        }

        [HttpPost]
        public IActionResult ImportDatabase()
        {
            return View();
        }
    }
}
