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

        public void UpdateCollectionPointState(Operation operation)
        {

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
