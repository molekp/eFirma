using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.WarehouseEntities.Product
{
    public class ProductItem : IItem
    {
        [Key]
        public int IdItem { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string Vin { get; set; }
         [NotMapped]
        public ItemState ItemState
        {
            get { return (ItemState)DbState; }
            set { DbState = (int)value; }
        }
        public DateTime ExpirationTime { get; set; }
        public virtual ProductType ItemType { get; set; }
        public virtual SaleType SaleType { get; set; }
        public string PKWiU { get; set; }
        public double? Discount { get; set; }
        public virtual List<ValueAttributeType> Attributes { get; set; }
        public int DbState { get; set; }
    }
}
