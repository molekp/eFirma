using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.DTOs.Store
{
    public class SimpleSellItemDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public double Quantity { get; set; }

        public string SaleTypeName { get; set; }
    }
}
