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

        public void UpdateCollectionPointState(string trashType, int weight)
        {
            switch(trashType)
            {
                case "plastic":
                    
                    break;
                case "glass":

            }
        }
    }
}
