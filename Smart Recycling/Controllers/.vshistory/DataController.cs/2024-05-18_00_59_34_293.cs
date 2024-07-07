﻿using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Net;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        private string backupDirectory = @"C:\Users\USER\source\repos\Smart Recycling\DAL\Backup\";

        [HttpGet]
        public async Task<IActionResult> ExportDatabaseAsync()
        {
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
        public ActionResult  UploadBackup()
        {
            return Ok("fef");
        }

        private void ApplyBackup(string filePath)
        {
            string connectionString = @"Server=DESKTOP-C6I6F3P\SQLEXPRESS;Database=SmartRecycling;Integrated Security=True;TrustServerCertificate=True";
            var databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = $@"
                    USE master;
                    ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    RESTORE DATABASE [{databaseName}] FROM DISK = @filePath WITH REPLACE;
                    ALTER DATABASE [{databaseName}] SET MULTI_USER;";

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@filePath", filePath);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
