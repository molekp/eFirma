using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.DTOs.Store
{
    public class SoldItemDto
    {
        public int DistributionId { get; set; }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public string SaleTypeName { get; set; }

        public string Vin { get; set; }

        public string ItemType { get; set; }

        public DateTime DistributionTime { get; set; }

        public string CustomerName { get; set; }
    }
}
