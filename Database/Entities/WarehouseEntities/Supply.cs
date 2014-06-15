using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities.WarehouseEntities.Product;

namespace Database.Entities.WarehouseEntities
{
    public class Supply
    {
        [Key]
        public int IdSupply { get; set; }

        public string Firm { get; set; }

        public decimal? Value { get; set; }

        public DateTime? CreationTime { get; set; } // when it was send to firm

        public DateTime RealizationTime { get; set; } // date of realization

        public DateTime? DeliveredTime { get; set; } // when it whas delivered to warehouse

        public int State { get; set; }

        public virtual List<Product.ProductItem> ProductItems { get; set; }
        
    }
}