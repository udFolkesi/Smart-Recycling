using CORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class SmartRecyclingDbContext: DbContext
    {
        public SmartRecyclingDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<Transportation> Transportation { get; set; }
        public DbSet<RecyclingPoint> RecyclingPoint { get; set; }
        public DbSet<RecyclingPointStatistics> RecyclingPointStatistics { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<CollectionPoint> CollectionPoint { get; set; }
        public DbSet<CollectionPointComposition> CollectionPointComposition { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
