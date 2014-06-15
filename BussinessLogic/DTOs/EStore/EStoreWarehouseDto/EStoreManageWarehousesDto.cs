using System.Collections.Generic;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DTOs.EStore.EStoreWarehouseDto
{
    public class EStoreManageWarehousesDto
    {
        public int IdEStore { get; set; }

        public IEnumerable<EStoreWarehouseDto> WarehousesWhichIsActive { get; set; }

        public IEnumerable<EStoreWarehouseDto> WarehousesWhichIsNotActive { get; set; }
    }
}