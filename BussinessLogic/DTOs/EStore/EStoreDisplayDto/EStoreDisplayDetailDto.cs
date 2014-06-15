using System.Collections.Generic;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DTOs.EStore.EStoreDisplayDto
{
    public class EStoreDisplayDetailDto
    {
        public int IdEStore { get; set; }

        public EStoreDisplayMenuDto EStoreDisplayMenu { get; set; }

        public EStoreDisplayDetailItemDto EStoreDisplayDetailItemDto { get; set; }

    }
}