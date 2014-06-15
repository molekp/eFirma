using System;
using Database.Entities.Customers;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace Database.Entities.Stores
{
    public class Complaint
    {
        public int ComplaintId { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public virtual ServiceItem ServiceItem { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime ComplaintDate { get; set; }

        public string Description { get; set; }

        public bool IsResolved { get; set; }

        public virtual UserEntity ResolverUser { get; set; }
    }
}
