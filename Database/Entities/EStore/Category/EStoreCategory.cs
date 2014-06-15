using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace Database.Entities.EStore.Category
{
    public class EStoreCategory
    {
        [Key]
        public int IdEStoreCategory { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }
        /*
        public int Parent { get; set; }

        public int Depth { get; set; }

        public string Path { get; set; }
        */
        public virtual EStore EStore { get; set; }

        public virtual List<ProductItem> ProductItems { get; set; }

        public virtual List<ServiceItem> ServiceItems { get; set; } 
    }
}