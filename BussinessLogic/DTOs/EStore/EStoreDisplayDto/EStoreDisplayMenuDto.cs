using System.Collections.Generic;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DTOs.EStore.EStoreDisplayDto
{
    public class EStoreDisplayMenuDto
    {
        public int IdEStore { get; set; }

        public int Type { get; set; }

        public List<EStoreDisplayItemTypeDto> ItemTypes { get; set; }
    }
}