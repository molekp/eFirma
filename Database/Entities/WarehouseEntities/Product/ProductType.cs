using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities.Product
{
    public class ProductType : IItemType, IEquatable<ProductType>
    {
        [Key]
        public int IdItemType { get; set; }
        public string Name { get; set; }
        public virtual TaxEntity ItemTax { get; set; }
        public virtual List<SimpleAttributeType> AttributeTypes { get; set; }
        public virtual List<ProductType> ProductTypeNodes { get; set; }

        public bool Equals(ProductType a_productType)
        {
            return (this.Name.Equals(a_productType.Name));
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ IdItemType.GetHashCode();
        }
    }
}
