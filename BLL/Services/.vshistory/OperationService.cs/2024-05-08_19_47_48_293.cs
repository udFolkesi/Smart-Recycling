using CORE.Models;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OperationService
    {
        private readonly SmartRecyclingDbContext dbContext;

        public OperationService(SmartRecyclingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async void UpdateCollectionPointState(Operation operation)
        {
            //var point = await dbContext.CollectionPoint.FindAsync(operation.CollectionPointID);
            var composition = dbContext.CollectionPointComposition
                .FirstOrDefault(c => c.CollectionPointID == operation.CollectionPointID && c.TrashType == "metal");
            composition.Weight += 10;
            await dbContext.SaveChangesAsync();
/*            switch(operation.TrashType)
            {
                case "plastic":
                    operation.CollectionPoint.CollectionPointComposition
                    break;
                case "glass":

            }*/
        }
    }
}
