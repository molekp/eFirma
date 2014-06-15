using System.Data.Entity;
using Database.Entities;
using Database.Entities.Customers;
using Database.Entities.EStore;
using Database.Entities.EStore.Category;
using Database.Entities.Factures;
using Database.Entities.Safety;
using Database.Entities.Stores;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Currencies;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace Database.Core.Interfaces
{
    public interface IDataBaseContext // Czy ten interfejs jest do czegoœ potrzebny? Zwróæcie wagê ile tych propertisów jest nie u¿ywanych.
    {
        IDbSet<RoleEntity> Roles { get; set; }
        IDbSet<UserEntity> Users { get; set; }
        IDbSet<TypeOfSafetyPoint> TypesOfSafetyPoints { get; set; } //this is created in DatabaseInitializer only
        IDbSet<SafetyPoint> SafetyPoints { get; set; }
        IDbSet<SafetyPointGroup> SafetyPointGroups { get; set; }

        IDbSet<Warehouse> Warehouses { get; set; }
        IDbSet<ProductWarehouse> ProductWarehouses { get; set; }
        IDbSet<ServiceWarehouse> ServiceWarehouses { get; set; }
            
        IDbSet<TaxEntity> Taxes { get; set; }
        IDbSet<ProductType> ProductTypes { get; set; }
        IDbSet<ServiceType> ServiceTypes { get; set; }
        IDbSet<SaleType> SaleTypes { get; set; }

        IDbSet<ProductItem> ProductItems { get; set; }
        IDbSet<ServiceItem> ServiceItems { get; set; }

        IDbSet<Distribution> Distributions { get; set; }
        IDbSet<Customer> Customers { get; set; }

        IDbSet<ValueAttributeType> Attributes { get; set; }
        IDbSet<Supply> Supplies { get; set; }
        IDbSet<SimpleAttributeType> AttributeTypes { get; set; }

        IDbSet<Facture> Factures { get; set; }
        IDbSet<FactureItem> FactureItems { get; set; }


        IDbSet<EmployeeEntity> Employees { get; set; }

        IDbSet<EStore> EStore { get; set; }
        IDbSet<StoreEntity> Stores { get; set; }

        IDbSet<EStoreCategory> EStoreCategory { get; set; }

        IDbSet<Complaint> Complaints { get; set; }

        IDbSet<Currency> Currencies { get; set; }
        IDbSet<CurrencyExchange> CurrencyExchanges { get; set; }

        IDbSet<EStoreShipmentType> EStoreShipmentTypes { get; set; } 

        void SetModified(object a_entity);
        void SaveChanges();
    }
}
