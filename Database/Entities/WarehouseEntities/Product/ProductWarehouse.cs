using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities.Product
{
    public class ProductWarehouse
    {
        [Key]
        public int IdProductWarehouse { get; set; }

        public string Name { get; set; }

        public virtual List<ProductItem> ProductItems { get; set; }
    }
}