using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.DTOs.WarehouseDtos.Items
{
    public class SimpleItemDto
    {
        public int IdItem { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public string Vin { get; set; }

        public ItemTypeEnum ItemType { get; set; }

        public string SaleType { get; set; }

        public string WarehouseName { get; set; }

        public string ItemWarehouseName { get; set; }

        public string WarehouseAddress { get; set; }
    }
}
