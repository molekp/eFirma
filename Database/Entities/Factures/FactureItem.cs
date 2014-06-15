using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace Database.Entities.Factures
{
    public class FactureItem
    {
        [Key]
        public int IdItem { get; set; }

        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public double Quantity { get; set; }

        public string SaleTypeName { get; set; }

        public string PKWiU { get; set; }

        public double TaxValue { get; set; }
    }
}