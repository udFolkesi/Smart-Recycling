using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TransportationService
    {
        private readonly SmartRecyclingDbContext dbContext;

        public TransportationService(SmartRecyclingDbContext smartRecyclingDbContext)
        {
            this.dbContext = smartRecyclingDbContext;
        }

        public void CreateTransportOperation(string location, string trashType)
        {

        }
    }
}
