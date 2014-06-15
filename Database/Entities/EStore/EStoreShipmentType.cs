using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Database.Entities.EStore
{
    public class EStoreShipmentType
    {
        [Key]
        public int IdEStoreShipmentType { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public decimal PriceShipment { get; set; }

        public int SortOrder { get; set; }
    }
}
