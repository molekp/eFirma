using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Database.Core.Interfaces;
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

namespace Database.Core
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public IDbSet<RoleEntity> Roles { get; set; }
        public IDbSet<UserEntity> Users { get; set; }
        public IDbSet<TypeOfSafetyPoint> TypesOfSafetyPoints { get; set; } //this is created in DatabaseInitializer only
        public IDbSet<SafetyPoint> SafetyPoints { get; set; }
        public IDbSet<SafetyPointGroup> SafetyPointGroups { get; set; }

        public IDbSet<Warehouse> Warehouses { get; set; }
        public IDbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public IDbSet<ServiceWarehouse> ServiceWarehouses { get; set; }

        public IDbSet<TaxEntity> Taxes { get; set; }
        public IDbSet<ProductType> ProductTypes { get; set; }
        public IDbSet<ServiceType> ServiceTypes { get; set; }
        public IDbSet<SaleType> SaleTypes { get; set; }
        public IDbSet<ProductItem> ProductItems { get; set; }
        public IDbSet<ServiceItem> ServiceItems { get; set; }
        public IDbSet<Distribution> Distributions { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<ValueAttributeType> Attributes { get; set; }
        public IDbSet<SimpleAttributeType> AttributeTypes { get; set; }
        public IDbSet<EmployeeEntity> Employees { get; set; }
        public IDbSet<Supply> Supplies { get; set; }
        public IDbSet<Facture> Factures { get; set; }
        public IDbSet<FactureItem> FactureItems { get; set; }
        public IDbSet<EStore> EStore { get; set; }
        public IDbSet<StoreEntity> Stores { get; set; }
        public IDbSet<EStoreCategory> EStoreCategory { get; set; }
        public IDbSet<Complaint> Complaints { get; set; }
        public IDbSet<Currency> Currencies { get; set; }
        public IDbSet<CurrencyExchange> CurrencyExchanges { get; set; }
        public IDbSet<EStoreShipmentType> EStoreShipmentTypes { get; set; }

        public DataBaseContext()
            : base("DatabaseConnection")
        {
        }

        public void SetModified(object a_entity)
        {
            Entry(a_entity).State = EntityState.Modified;
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder a_modelBuilder)
        {

            Configuration.LazyLoadingEnabled = false;// wylaczylem lazy loading entitow bo tylko problemy byly z tym
            a_modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            a_modelBuilder.Entity<SafetyPointGroup>()
                          .HasMany(a => a.SafetyPoints)
                          .WithMany()
                          .Map(x =>
                                   {
                                       x.MapLeftKey("IdSafetyPointGroup");
                                       x.MapRightKey("IdSafetyPoint");
                                       x.ToTable("SafetyPointInGroups");
                                   });
            a_modelBuilder.Entity<UserEntity>()
                          .HasMany(a => a.UserSafetyPointGroups)
                          .WithMany()
                          .Map(x =>
                          {
                              x.MapLeftKey("IdUser");
                              x.MapRightKey("IdSafetyPointGroup");
                              x.ToTable("UserInSafetyPointGroups");
                          });

            a_modelBuilder.Entity<Warehouse>()
                .HasMany(a => a.ProductWarehouses)
                .WithMany()
                .Map(x =>
                    {
                        x.MapLeftKey("IdWarehouse");
                        x.MapRightKey("IdProductWarehouse");
                        x.ToTable("ProductWarehousesInBaseWarehouse");
                    });

            a_modelBuilder.Entity<Warehouse>()
                .HasMany(a => a.ServiceWarehouses)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdWarehouse");
                    x.MapRightKey("IdServiceWarehouse");
                    x.ToTable("ServiceWarehousesInBaseWarehouse");
                });

            a_modelBuilder.Entity<ProductWarehouse>()
                .HasMany(a => a.ProductItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdProductWarehouse");
                    x.MapRightKey("IdProductItem");
                    x.ToTable("ProductsInProductWarehouse");
                });

            a_modelBuilder.Entity<ServiceWarehouse>()
                .HasMany(a => a.ServiceItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdServiceWarehouse");
                    x.MapRightKey("IdServiceItem");
                    x.ToTable("ServicesInServiceWarehouse");
                });

            a_modelBuilder.Entity<Supply>()
                .HasMany(a => a.ProductItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdSupply");
                    x.MapRightKey("IdProductItem");
                    x.ToTable("ProductItemsInSupplies");
                });

            a_modelBuilder.Entity<ProductItem>()
                .HasMany(a => a.Attributes)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdItem");
                    x.MapRightKey("IdAttribute");
                    x.ToTable("AttributesInProductItems");
                });

            a_modelBuilder.Entity<ServiceItem>()
               .HasMany(a => a.Attributes)
               .WithMany()
               .Map(x =>
               {
                   x.MapLeftKey("IdItem");
                   x.MapRightKey("IdAttribute");
                   x.ToTable("AttributesInServiceItems");
               });

            a_modelBuilder.Entity<ProductType>()
                .HasMany(a => a.AttributeTypes)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdItemType");
                    x.MapRightKey("IdAttributeType");
                    x.ToTable("AttributeTypesInProductType");
                });
            a_modelBuilder.Entity<ServiceType>()
                .HasMany(a => a.AttributeTypes)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdItemType");
                    x.MapRightKey("IdAttributeType");
                    x.ToTable("AttributeTypesInServiceType");
                });

            a_modelBuilder.Entity<Distribution>()
                .HasMany(a => a.ProductItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdDistribution");
                    x.MapRightKey("IdItem");
                    x.ToTable("ProductItemsInDistribution");
                });

            a_modelBuilder.Entity<Distribution>()
                .HasMany(a => a.ServiceItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdDistribution");
                    x.MapRightKey("IdItem");
                    x.ToTable("ServiceItemsInDistribution");
                });

            a_modelBuilder.Entity<Firm>().ToTable("Firm");
            a_modelBuilder.Entity<SimpleCustomer>().ToTable("SimpleCustomer");


            a_modelBuilder.Entity<EStore>()
                .HasMany(a => a.Warehouses)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdEStore");
                    x.MapRightKey("IdWarehouse");
                    x.ToTable("EStoreInWarehouses");
                });

            a_modelBuilder.Entity<EStoreCategory>()
                .HasMany(a => a.ProductItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdEStoreCategory");
                    x.MapRightKey("IdItem");
                    x.ToTable("EStoreCategoryInProductItem");
                });

            a_modelBuilder.Entity<EStoreCategory>()
                .HasMany(a => a.ServiceItems)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdEStoreCategory");
                    x.MapRightKey("IdItem");
                    x.ToTable("EStoreCategoryInServiceItem");
                });

            a_modelBuilder.Entity<StoreEntity>()
                .HasMany(a => a.Distributions)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdStore");
                    x.MapRightKey("IdDistribution");
                    x.ToTable("DistributionsInStore");
                });

            a_modelBuilder.Entity<EStore>()
                .HasMany(a => a.EStoreShipmentType)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdEStore");
                    x.MapRightKey("IdEStoreShipmentType");
                    x.ToTable("EStoreInEStoreShipmentType");
                });
        }

    }
}
