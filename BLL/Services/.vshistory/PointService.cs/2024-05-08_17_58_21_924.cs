using CORE.Models;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PointService
    {
        private readonly SmartRecyclingDbContext dbContext;
        public  PointService(SmartRecyclingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreatePointComposition(int pointId)
        {
            string[] trashTypes = { "plastic", "glass", "paper", "metal" };
            for(int i = 0; i < 4; i++)
            {
                CollectionPointComposition composition = new()
                {
                    Id = pointId,
                    TrashType = trashTypes[i],
                    Weight = 0,
                    MaxVolume = 0,
                };

                dbContext.CollectionPointComposition.AddAsync(composition);
                dbContext.SaveChangesAsync();
            }
        }
    }
}
