using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities.Service
{
    public class ServiceType : IItemType
    {
        [Key]
        public int IdItemType { get; set; }
        public string Name { get; set; }
        public virtual TaxEntity ItemTax { get; set; }
        public virtual List<SimpleAttributeType> AttributeTypes { get; set; }
        public virtual List<ServiceType> ServiceTypeNodes { get; set; }

        public bool Equals(ServiceType a_serviceType)
        {
            return (this.Name.Equals(a_serviceType.Name));
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ IdItemType.GetHashCode();
        }
    }
}
