using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities.Service
{
    public class ServiceWarehouse
    {
        [Key]
        public int IdServiceWarehouse { get; set; }

        public string Name { get; set; }

        public virtual List<ServiceItem> ServiceItems { get; set; }
    }
}