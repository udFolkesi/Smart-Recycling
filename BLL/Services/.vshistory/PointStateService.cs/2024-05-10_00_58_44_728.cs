using CORE.Models;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PointStateService
    {
        private readonly SmartRecyclingDbContext dbContext;
        private readonly TransportationService transportationService;
        public  PointStateService(SmartRecyclingDbContext dbContext, TransportationService transportationService)
        {
            this.dbContext = dbContext;
            this.transportationService = transportationService;
        }

        public async Task CreatePointComposition(int pointId)
        {
            string[] trashTypes = { "plastic", "glass", "paper", "metal" };
            for(int i = 0; i < 4; i++)
            {
                CollectionPointComposition composition = new()
                {
                    TrashType = trashTypes[i],
                    CollectionPointID = pointId,
                };

                await dbContext.CollectionPointComposition.AddAsync(composition);
            }

            await dbContext.SaveChangesAsync();
        }

        public async void UpdateCollectionPointState(Operation operation)
        {
            var point = await dbContext.CollectionPoint.FindAsync(operation.CollectionPointID);
            var composition = dbContext.CollectionPointComposition
                .FirstOrDefault(c => c.CollectionPointID == operation.CollectionPointID && c.TrashType == operation.TrashType);
            composition.Weight += operation.Weight;
            composition.Volume += operation.Volume;
            composition.Fullness = composition.Volume / composition.MaxVolume * 100;
            point.Fullness = dbContext.CollectionPointComposition
                .Where(c => c.CollectionPointID == operation.CollectionPointID)
                .Select(c => c.Fullness)
                .Sum() / 4;
            if(composition.Fullness > 95)
            {
                transportationService.CreateTransportOperation(point.Location, operation.TrashType); // атоматическая смена статуса контейнера или его изменение со стороны сотрудника через мобайл
            }

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
