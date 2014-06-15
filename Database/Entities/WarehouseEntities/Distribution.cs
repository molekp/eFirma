using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Entities.Customers;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace Database.Entities.WarehouseEntities
{
    public class Distribution
    {
        [Key]
        public int IdDistribution { get; set; }

        public virtual List<ProductItem> ProductItems { get; set; }

        public virtual List<ServiceItem> ServiceItems { get; set; }

        public DateTime DistributionTime { get; set; }

        public DateTime DistributionCreateTime { get; set; }

        public virtual UserEntity DistributionCreator { get; set; }

        public virtual Firm DistributionProvider { get; set; }

        public virtual Customer DistributionCustomer { get; set; }

        public virtual Facture Facture { get; set; }

        public int DbState { get; set; }

        [NotMapped]
        public DistributionState State {
            get { return (DistributionState) DbState; }
            set { DbState= (int) value;}
        }
    }


    public enum DistributionState
    {
        Prepared = 0,
        Performed = 1
    }
}
