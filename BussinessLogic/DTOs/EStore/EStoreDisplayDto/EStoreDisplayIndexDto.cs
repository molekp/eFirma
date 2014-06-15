using System.Collections.Generic;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DTOs.EStore.EStoreDisplayDto
{
    public class EStoreDisplayIndexDto
    {
        public int IdEStore { get; set; }

        public EStoreDisplayMenuDto EStoreDisplayMenu { get; set; }

        public EStoreDisplayItemsDto EStoreDisplayItemsDto { get; set; }

    }
}