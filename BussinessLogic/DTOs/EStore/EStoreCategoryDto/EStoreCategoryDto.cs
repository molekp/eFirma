using System.Collections.Generic;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DTOs.EStore.EStoreCategoryDto
{
    public class EStoreCategoryDto
    {
        public int IdEStoreCategory { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public Database.Entities.EStore.EStore EStore { get; set; }

        public List<ProductItem> ProductItems { get; set; }

        public List<ServiceItem> ServiceItems { get; set; }  
    }
}