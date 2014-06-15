namespace Database.Migrations
{
    using Database.Core;
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
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        
    }
}
