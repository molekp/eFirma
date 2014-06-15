using System.Collections.Generic;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DTOs.EStore.EStoreShipmentType
{
    public class EStoreShipmentTypeDto
    {
        public int IdEStoreShipmentType { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public decimal PriceShipment { get; set; }

        public int SortOrder { get; set; }
    }
}