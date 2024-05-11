using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackupManager
    {
        public static void CreateBackup()
        {
            string backupDirectory = @"C:\Users\USER\source\repos\Smart Recycling\DAL\Backup\";
            //string backupDirectory = @"..\DAL\Backup\";
            string connectionString = @"Server=DESKTOP-C6I6F3P\SQLEXPRESS;Database=SmartRecycling;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                // Ensure the backup directory exists, create it if not
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                // Generate backup file name with timestamp
                string backupFileName = "SmartRecycling_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                string backupFilePath = Path.Combine(backupDirectory, backupFileName);

                // Perform the database backup using SQL command
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string backupQuery = $"BACKUP DATABASE SmartRecycling TO DISK = '{backupFilePath}' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup of SmartRecycling';";
                    using (var command = new SqlCommand(backupQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Backup created successfully: " + backupFilePath);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine("Backup failed: " + ex.Message);
            }
        }
    }
}
