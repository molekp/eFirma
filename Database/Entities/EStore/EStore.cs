using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Database.Entities.WarehouseEntities;

namespace Database.Entities.EStore
{
    public class EStore
    {
        [Key]
        public int IdEStore { get; set; }

        public string Name { get; set; }

        public string UniqHash { get; set; }

        public virtual List<Warehouse> Warehouses { get; set; }

        public virtual List<EStoreShipmentType> EStoreShipmentType { get; set; }
    }
}
