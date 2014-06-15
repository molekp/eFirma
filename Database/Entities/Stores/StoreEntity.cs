using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities.WarehouseEntities;

namespace Database.Entities.Stores
{
    public class StoreEntity
    {
        [Key]
        public int IdStore { get; set; }

        public string Name { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual List<Distribution> Distributions { get; set; }
    }
}
