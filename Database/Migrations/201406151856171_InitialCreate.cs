namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValueAttributeType",
                c => new
                    {
                        Value = c.String(nullable: false, maxLength: 128),
                        IdAttributeType = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Value);
            
            CreateTable(
                "dbo.SimpleAttributeType",
                c => new
                    {
                        IdAttributeType = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdAttributeType);
            
            CreateTable(
                "dbo.Complaint",
                c => new
                    {
                        ComplaintId = c.Int(nullable: false, identity: true),
                        ComplaintDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsResolved = c.Boolean(nullable: false),
                        Customer_IdCustomer = c.Int(),
                        ProductItem_IdItem = c.Int(),
                        ResolverUser_IdUser = c.Int(),
                        ServiceItem_IdItem = c.Int(),
                    })
                .PrimaryKey(t => t.ComplaintId)
                .ForeignKey("dbo.Customer", t => t.Customer_IdCustomer)
                .ForeignKey("dbo.ProductItem", t => t.ProductItem_IdItem)
                .ForeignKey("dbo.UserEntity", t => t.ResolverUser_IdUser)
                .ForeignKey("dbo.ServiceItem", t => t.ServiceItem_IdItem)
                .Index(t => t.Customer_IdCustomer)
                .Index(t => t.ProductItem_IdItem)
                .Index(t => t.ResolverUser_IdUser)
                .Index(t => t.ServiceItem_IdItem);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        IdCustomer = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdCustomer);
            
            CreateTable(
                "dbo.ProductItem",
                c => new
                    {
                        IdItem = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                        Vin = c.String(),
                        ExpirationTime = c.DateTime(nullable: false),
                        PKWiU = c.String(),
                        Discount = c.Double(),
                        DbState = c.Int(nullable: false),
                        ItemType_IdItemType = c.Int(),
                        SaleType_IdSaleType = c.Int(),
                    })
                .PrimaryKey(t => t.IdItem)
                .ForeignKey("dbo.ProductType", t => t.ItemType_IdItemType)
                .ForeignKey("dbo.SaleType", t => t.SaleType_IdSaleType)
                .Index(t => t.ItemType_IdItemType)
                .Index(t => t.SaleType_IdSaleType);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        IdItemType = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ItemTax_IdTax = c.Int(),
                        ProductType_IdItemType = c.Int(),
                    })
                .PrimaryKey(t => t.IdItemType)
                .ForeignKey("dbo.TaxEntity", t => t.ItemTax_IdTax)
                .ForeignKey("dbo.ProductType", t => t.ProductType_IdItemType)
                .Index(t => t.ItemTax_IdTax)
                .Index(t => t.ProductType_IdItemType);
            
            CreateTable(
                "dbo.TaxEntity",
                c => new
                    {
                        IdTax = c.Int(nullable: false, identity: true),
                        TaxName = c.String(),
                        TaxValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdTax);
            
            CreateTable(
                "dbo.SaleType",
                c => new
                    {
                        IdSaleType = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdSaleType);
            
            CreateTable(
                "dbo.UserEntity",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        EMail = c.String(),
                        Role_IdRole = c.Int(),
                    })
                .PrimaryKey(t => t.IdUser)
                .ForeignKey("dbo.RoleEntity", t => t.Role_IdRole)
                .Index(t => t.Role_IdRole);
            
            CreateTable(
                "dbo.RoleEntity",
                c => new
                    {
                        IdRole = c.Int(nullable: false, identity: true),
                        NameRole = c.String(),
                    })
                .PrimaryKey(t => t.IdRole);
            
            CreateTable(
                "dbo.SafetyPointGroup",
                c => new
                    {
                        IdSafetyPointGroup = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdSafetyPointGroup);
            
            CreateTable(
                "dbo.SafetyPoint",
                c => new
                    {
                        IdSafetyPoint = c.Int(nullable: false, identity: true),
                        NameOfsafetyPoint = c.String(nullable: false),
                        IdOfPointInTable = c.Int(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                        TypeOfSafetyPoint_IdTypeOfSafetyPoint = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSafetyPoint)
                .ForeignKey("dbo.TypeOfSafetyPoint", t => t.TypeOfSafetyPoint_IdTypeOfSafetyPoint, cascadeDelete: true)
                .Index(t => t.TypeOfSafetyPoint_IdTypeOfSafetyPoint);
            
            CreateTable(
                "dbo.TypeOfSafetyPoint",
                c => new
                    {
                        IdTypeOfSafetyPoint = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdTypeOfSafetyPoint);
            
            CreateTable(
                "dbo.ServiceItem",
                c => new
                    {
                        IdItem = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                        Vin = c.String(),
                        ExpirationTime = c.DateTime(nullable: false),
                        PKWiU = c.String(),
                        Discount = c.Double(),
                        DbState = c.Int(nullable: false),
                        ItemType_IdItemType = c.Int(),
                        SaleType_IdSaleType = c.Int(),
                    })
                .PrimaryKey(t => t.IdItem)
                .ForeignKey("dbo.ServiceType", t => t.ItemType_IdItemType)
                .ForeignKey("dbo.SaleType", t => t.SaleType_IdSaleType)
                .Index(t => t.ItemType_IdItemType)
                .Index(t => t.SaleType_IdSaleType);
            
            CreateTable(
                "dbo.ServiceType",
                c => new
                    {
                        IdItemType = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ItemTax_IdTax = c.Int(),
                        ServiceType_IdItemType = c.Int(),
                    })
                .PrimaryKey(t => t.IdItemType)
                .ForeignKey("dbo.TaxEntity", t => t.ItemTax_IdTax)
                .ForeignKey("dbo.ServiceType", t => t.ServiceType_IdItemType)
                .Index(t => t.ItemTax_IdTax)
                .Index(t => t.ServiceType_IdItemType);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.CurrencyExchange",
                c => new
                    {
                        CurrencyExchangeId = c.Int(nullable: false, identity: true),
                        ExchangeCourseDate = c.DateTime(nullable: false),
                        Exchange = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency_CurrencyId = c.Int(),
                        Facture_IdFacture = c.Int(),
                    })
                .PrimaryKey(t => t.CurrencyExchangeId)
                .ForeignKey("dbo.Currency", t => t.Currency_CurrencyId)
                .ForeignKey("dbo.Facture", t => t.Facture_IdFacture)
                .Index(t => t.Currency_CurrencyId)
                .Index(t => t.Facture_IdFacture);
            
            CreateTable(
                "dbo.Distribution",
                c => new
                    {
                        IdDistribution = c.Int(nullable: false, identity: true),
                        DistributionTime = c.DateTime(nullable: false),
                        DistributionCreateTime = c.DateTime(nullable: false),
                        DbState = c.Int(nullable: false),
                        DistributionCreator_IdUser = c.Int(),
                        DistributionCustomer_IdCustomer = c.Int(),
                        DistributionProvider_IdCustomer = c.Int(),
                        Facture_IdFacture = c.Int(),
                    })
                .PrimaryKey(t => t.IdDistribution)
                .ForeignKey("dbo.UserEntity", t => t.DistributionCreator_IdUser)
                .ForeignKey("dbo.Customer", t => t.DistributionCustomer_IdCustomer)
                .ForeignKey("dbo.Firm", t => t.DistributionProvider_IdCustomer)
                .ForeignKey("dbo.Facture", t => t.Facture_IdFacture)
                .Index(t => t.DistributionCreator_IdUser)
                .Index(t => t.DistributionCustomer_IdCustomer)
                .Index(t => t.DistributionProvider_IdCustomer)
                .Index(t => t.Facture_IdFacture);
            
            CreateTable(
                "dbo.Facture",
                c => new
                    {
                        IdFacture = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(),
                        ProviderAddress = c.String(),
                        ProviderNIP = c.String(),
                        ProviderInfo = c.String(),
                        ProviderBankAccountNumber = c.String(),
                        ProviderBankInfo = c.String(),
                        ClientName = c.String(),
                        ClientAddress = c.String(),
                        ClientNIP = c.String(),
                        ClientInfo = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreationPlace = c.String(),
                        RealizationTime = c.DateTime(nullable: false),
                        ExpirationTime = c.DateTime(nullable: false),
                        Sum = c.Decimal(precision: 18, scale: 2),
                        SumString = c.String(),
                        Paid = c.Decimal(precision: 18, scale: 2),
                        Issuer = c.String(),
                        Reciver = c.String(),
                        Payment = c.String(),
                    })
                .PrimaryKey(t => t.IdFacture);
            
            CreateTable(
                "dbo.FactureItem",
                c => new
                    {
                        IdItem = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                        SaleTypeName = c.String(),
                        PKWiU = c.String(),
                        TaxValue = c.Double(nullable: false),
                        Facture_IdFacture = c.Int(),
                    })
                .PrimaryKey(t => t.IdItem)
                .ForeignKey("dbo.Facture", t => t.Facture_IdFacture)
                .Index(t => t.Facture_IdFacture);
            
            CreateTable(
                "dbo.EmployeeEntity",
                c => new
                    {
                        IdEmployee = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankAccountNumber = c.String(),
                        UserEntity_IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IdEmployee)
                .ForeignKey("dbo.UserEntity", t => t.UserEntity_IdUser)
                .Index(t => t.UserEntity_IdUser);
            
            CreateTable(
                "dbo.EStore",
                c => new
                    {
                        IdEStore = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UniqHash = c.String(),
                    })
                .PrimaryKey(t => t.IdEStore);
            
            CreateTable(
                "dbo.EStoreShipmentType",
                c => new
                    {
                        IdEStoreShipmentType = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Info = c.String(),
                        PriceShipment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEStoreShipmentType);
            
            CreateTable(
                "dbo.Warehouse",
                c => new
                    {
                        IdWarehouse = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.IdWarehouse);
            
            CreateTable(
                "dbo.ProductWarehouse",
                c => new
                    {
                        IdProductWarehouse = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdProductWarehouse);
            
            CreateTable(
                "dbo.ServiceWarehouse",
                c => new
                    {
                        IdServiceWarehouse = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdServiceWarehouse);
            
            CreateTable(
                "dbo.EStoreCategory",
                c => new
                    {
                        IdEStoreCategory = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SortOrder = c.Int(nullable: false),
                        EStore_IdEStore = c.Int(),
                    })
                .PrimaryKey(t => t.IdEStoreCategory)
                .ForeignKey("dbo.EStore", t => t.EStore_IdEStore)
                .Index(t => t.EStore_IdEStore);
            
            CreateTable(
                "dbo.StoreEntity",
                c => new
                    {
                        IdStore = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Warehouse_IdWarehouse = c.Int(),
                    })
                .PrimaryKey(t => t.IdStore)
                .ForeignKey("dbo.Warehouse", t => t.Warehouse_IdWarehouse)
                .Index(t => t.Warehouse_IdWarehouse);
            
            CreateTable(
                "dbo.Supply",
                c => new
                    {
                        IdSupply = c.Int(nullable: false, identity: true),
                        Firm = c.String(),
                        Value = c.Decimal(precision: 18, scale: 2),
                        CreationTime = c.DateTime(),
                        RealizationTime = c.DateTime(nullable: false),
                        DeliveredTime = c.DateTime(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSupply);
            
            CreateTable(
                "dbo.AttributesInProductItems",
                c => new
                    {
                        IdItem = c.Int(nullable: false),
                        IdAttribute = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdItem, t.IdAttribute })
                .ForeignKey("dbo.ProductItem", t => t.IdItem, cascadeDelete: true)
                .ForeignKey("dbo.ValueAttributeType", t => t.IdAttribute, cascadeDelete: true)
                .Index(t => t.IdItem)
                .Index(t => t.IdAttribute);
            
            CreateTable(
                "dbo.AttributeTypesInProductType",
                c => new
                    {
                        IdItemType = c.Int(nullable: false),
                        IdAttributeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdItemType, t.IdAttributeType })
                .ForeignKey("dbo.ProductType", t => t.IdItemType, cascadeDelete: true)
                .ForeignKey("dbo.SimpleAttributeType", t => t.IdAttributeType, cascadeDelete: true)
                .Index(t => t.IdItemType)
                .Index(t => t.IdAttributeType);
            
            CreateTable(
                "dbo.SafetyPointInGroups",
                c => new
                    {
                        IdSafetyPointGroup = c.Int(nullable: false),
                        IdSafetyPoint = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdSafetyPointGroup, t.IdSafetyPoint })
                .ForeignKey("dbo.SafetyPointGroup", t => t.IdSafetyPointGroup, cascadeDelete: true)
                .ForeignKey("dbo.SafetyPoint", t => t.IdSafetyPoint, cascadeDelete: true)
                .Index(t => t.IdSafetyPointGroup)
                .Index(t => t.IdSafetyPoint);
            
            CreateTable(
                "dbo.UserInSafetyPointGroups",
                c => new
                    {
                        IdUser = c.Int(nullable: false),
                        IdSafetyPointGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUser, t.IdSafetyPointGroup })
                .ForeignKey("dbo.UserEntity", t => t.IdUser, cascadeDelete: true)
                .ForeignKey("dbo.SafetyPointGroup", t => t.IdSafetyPointGroup, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdSafetyPointGroup);
            
            CreateTable(
                "dbo.AttributesInServiceItems",
                c => new
                    {
                        IdItem = c.Int(nullable: false),
                        IdAttribute = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdItem, t.IdAttribute })
                .ForeignKey("dbo.ServiceItem", t => t.IdItem, cascadeDelete: true)
                .ForeignKey("dbo.ValueAttributeType", t => t.IdAttribute, cascadeDelete: true)
                .Index(t => t.IdItem)
                .Index(t => t.IdAttribute);
            
            CreateTable(
                "dbo.AttributeTypesInServiceType",
                c => new
                    {
                        IdItemType = c.Int(nullable: false),
                        IdAttributeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdItemType, t.IdAttributeType })
                .ForeignKey("dbo.ServiceType", t => t.IdItemType, cascadeDelete: true)
                .ForeignKey("dbo.SimpleAttributeType", t => t.IdAttributeType, cascadeDelete: true)
                .Index(t => t.IdItemType)
                .Index(t => t.IdAttributeType);
            
            CreateTable(
                "dbo.ProductItemsInDistribution",
                c => new
                    {
                        IdDistribution = c.Int(nullable: false),
                        IdItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDistribution, t.IdItem })
                .ForeignKey("dbo.Distribution", t => t.IdDistribution, cascadeDelete: true)
                .ForeignKey("dbo.ProductItem", t => t.IdItem, cascadeDelete: true)
                .Index(t => t.IdDistribution)
                .Index(t => t.IdItem);
            
            CreateTable(
                "dbo.ServiceItemsInDistribution",
                c => new
                    {
                        IdDistribution = c.Int(nullable: false),
                        IdItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDistribution, t.IdItem })
                .ForeignKey("dbo.Distribution", t => t.IdDistribution, cascadeDelete: true)
                .ForeignKey("dbo.ServiceItem", t => t.IdItem, cascadeDelete: true)
                .Index(t => t.IdDistribution)
                .Index(t => t.IdItem);
            
            CreateTable(
                "dbo.EStoreInEStoreShipmentType",
                c => new
                    {
                        IdEStore = c.Int(nullable: false),
                        IdEStoreShipmentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdEStore, t.IdEStoreShipmentType })
                .ForeignKey("dbo.EStore", t => t.IdEStore, cascadeDelete: true)
                .ForeignKey("dbo.EStoreShipmentType", t => t.IdEStoreShipmentType, cascadeDelete: true)
                .Index(t => t.IdEStore)
                .Index(t => t.IdEStoreShipmentType);
            
            CreateTable(
                "dbo.ProductsInProductWarehouse",
                c => new
                    {
                        IdProductWarehouse = c.Int(nullable: false),
                        IdProductItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdProductWarehouse, t.IdProductItem })
                .ForeignKey("dbo.ProductWarehouse", t => t.IdProductWarehouse, cascadeDelete: true)
                .ForeignKey("dbo.ProductItem", t => t.IdProductItem, cascadeDelete: true)
                .Index(t => t.IdProductWarehouse)
                .Index(t => t.IdProductItem);
            
            CreateTable(
                "dbo.ProductWarehousesInBaseWarehouse",
                c => new
                    {
                        IdWarehouse = c.Int(nullable: false),
                        IdProductWarehouse = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdWarehouse, t.IdProductWarehouse })
                .ForeignKey("dbo.Warehouse", t => t.IdWarehouse, cascadeDelete: true)
                .ForeignKey("dbo.ProductWarehouse", t => t.IdProductWarehouse, cascadeDelete: true)
                .Index(t => t.IdWarehouse)
                .Index(t => t.IdProductWarehouse);
            
            CreateTable(
                "dbo.ServicesInServiceWarehouse",
                c => new
                    {
                        IdServiceWarehouse = c.Int(nullable: false),
                        IdServiceItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdServiceWarehouse, t.IdServiceItem })
                .ForeignKey("dbo.ServiceWarehouse", t => t.IdServiceWarehouse, cascadeDelete: true)
                .ForeignKey("dbo.ServiceItem", t => t.IdServiceItem, cascadeDelete: true)
                .Index(t => t.IdServiceWarehouse)
                .Index(t => t.IdServiceItem);
            
            CreateTable(
                "dbo.ServiceWarehousesInBaseWarehouse",
                c => new
                    {
                        IdWarehouse = c.Int(nullable: false),
                        IdServiceWarehouse = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdWarehouse, t.IdServiceWarehouse })
                .ForeignKey("dbo.Warehouse", t => t.IdWarehouse, cascadeDelete: true)
                .ForeignKey("dbo.ServiceWarehouse", t => t.IdServiceWarehouse, cascadeDelete: true)
                .Index(t => t.IdWarehouse)
                .Index(t => t.IdServiceWarehouse);
            
            CreateTable(
                "dbo.EStoreInWarehouses",
                c => new
                    {
                        IdEStore = c.Int(nullable: false),
                        IdWarehouse = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdEStore, t.IdWarehouse })
                .ForeignKey("dbo.EStore", t => t.IdEStore, cascadeDelete: true)
                .ForeignKey("dbo.Warehouse", t => t.IdWarehouse, cascadeDelete: true)
                .Index(t => t.IdEStore)
                .Index(t => t.IdWarehouse);
            
            CreateTable(
                "dbo.EStoreCategoryInProductItem",
                c => new
                    {
                        IdEStoreCategory = c.Int(nullable: false),
                        IdItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdEStoreCategory, t.IdItem })
                .ForeignKey("dbo.EStoreCategory", t => t.IdEStoreCategory, cascadeDelete: true)
                .ForeignKey("dbo.ProductItem", t => t.IdItem, cascadeDelete: true)
                .Index(t => t.IdEStoreCategory)
                .Index(t => t.IdItem);
            
            CreateTable(
                "dbo.EStoreCategoryInServiceItem",
                c => new
                    {
                        IdEStoreCategory = c.Int(nullable: false),
                        IdItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdEStoreCategory, t.IdItem })
                .ForeignKey("dbo.EStoreCategory", t => t.IdEStoreCategory, cascadeDelete: true)
                .ForeignKey("dbo.ServiceItem", t => t.IdItem, cascadeDelete: true)
                .Index(t => t.IdEStoreCategory)
                .Index(t => t.IdItem);
            
            CreateTable(
                "dbo.DistributionsInStore",
                c => new
                    {
                        IdStore = c.Int(nullable: false),
                        IdDistribution = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdStore, t.IdDistribution })
                .ForeignKey("dbo.StoreEntity", t => t.IdStore, cascadeDelete: true)
                .ForeignKey("dbo.Distribution", t => t.IdDistribution, cascadeDelete: true)
                .Index(t => t.IdStore)
                .Index(t => t.IdDistribution);
            
            CreateTable(
                "dbo.ProductItemsInSupplies",
                c => new
                    {
                        IdSupply = c.Int(nullable: false),
                        IdProductItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdSupply, t.IdProductItem })
                .ForeignKey("dbo.Supply", t => t.IdSupply, cascadeDelete: true)
                .ForeignKey("dbo.ProductItem", t => t.IdProductItem, cascadeDelete: true)
                .Index(t => t.IdSupply)
                .Index(t => t.IdProductItem);
            
            CreateTable(
                "dbo.Firm",
                c => new
                    {
                        IdCustomer = c.Int(nullable: false),
                        Phone = c.String(),
                        BankAccountNumber = c.String(),
                        BankInfo = c.String(),
                        Info = c.String(),
                        NIP = c.String(),
                    })
                .PrimaryKey(t => t.IdCustomer)
                .ForeignKey("dbo.Customer", t => t.IdCustomer)
                .Index(t => t.IdCustomer);
            
            CreateTable(
                "dbo.SimpleCustomer",
                c => new
                    {
                        IdCustomer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCustomer)
                .ForeignKey("dbo.Customer", t => t.IdCustomer)
                .Index(t => t.IdCustomer);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SimpleCustomer", "IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.Firm", "IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.ProductItemsInSupplies", "IdProductItem", "dbo.ProductItem");
            DropForeignKey("dbo.ProductItemsInSupplies", "IdSupply", "dbo.Supply");
            DropForeignKey("dbo.StoreEntity", "Warehouse_IdWarehouse", "dbo.Warehouse");
            DropForeignKey("dbo.DistributionsInStore", "IdDistribution", "dbo.Distribution");
            DropForeignKey("dbo.DistributionsInStore", "IdStore", "dbo.StoreEntity");
            DropForeignKey("dbo.EStoreCategoryInServiceItem", "IdItem", "dbo.ServiceItem");
            DropForeignKey("dbo.EStoreCategoryInServiceItem", "IdEStoreCategory", "dbo.EStoreCategory");
            DropForeignKey("dbo.EStoreCategoryInProductItem", "IdItem", "dbo.ProductItem");
            DropForeignKey("dbo.EStoreCategoryInProductItem", "IdEStoreCategory", "dbo.EStoreCategory");
            DropForeignKey("dbo.EStoreCategory", "EStore_IdEStore", "dbo.EStore");
            DropForeignKey("dbo.EStoreInWarehouses", "IdWarehouse", "dbo.Warehouse");
            DropForeignKey("dbo.EStoreInWarehouses", "IdEStore", "dbo.EStore");
            DropForeignKey("dbo.ServiceWarehousesInBaseWarehouse", "IdServiceWarehouse", "dbo.ServiceWarehouse");
            DropForeignKey("dbo.ServiceWarehousesInBaseWarehouse", "IdWarehouse", "dbo.Warehouse");
            DropForeignKey("dbo.ServicesInServiceWarehouse", "IdServiceItem", "dbo.ServiceItem");
            DropForeignKey("dbo.ServicesInServiceWarehouse", "IdServiceWarehouse", "dbo.ServiceWarehouse");
            DropForeignKey("dbo.ProductWarehousesInBaseWarehouse", "IdProductWarehouse", "dbo.ProductWarehouse");
            DropForeignKey("dbo.ProductWarehousesInBaseWarehouse", "IdWarehouse", "dbo.Warehouse");
            DropForeignKey("dbo.ProductsInProductWarehouse", "IdProductItem", "dbo.ProductItem");
            DropForeignKey("dbo.ProductsInProductWarehouse", "IdProductWarehouse", "dbo.ProductWarehouse");
            DropForeignKey("dbo.EStoreInEStoreShipmentType", "IdEStoreShipmentType", "dbo.EStoreShipmentType");
            DropForeignKey("dbo.EStoreInEStoreShipmentType", "IdEStore", "dbo.EStore");
            DropForeignKey("dbo.EmployeeEntity", "UserEntity_IdUser", "dbo.UserEntity");
            DropForeignKey("dbo.ServiceItemsInDistribution", "IdItem", "dbo.ServiceItem");
            DropForeignKey("dbo.ServiceItemsInDistribution", "IdDistribution", "dbo.Distribution");
            DropForeignKey("dbo.ProductItemsInDistribution", "IdItem", "dbo.ProductItem");
            DropForeignKey("dbo.ProductItemsInDistribution", "IdDistribution", "dbo.Distribution");
            DropForeignKey("dbo.Distribution", "Facture_IdFacture", "dbo.Facture");
            DropForeignKey("dbo.FactureItem", "Facture_IdFacture", "dbo.Facture");
            DropForeignKey("dbo.CurrencyExchange", "Facture_IdFacture", "dbo.Facture");
            DropForeignKey("dbo.Distribution", "DistributionProvider_IdCustomer", "dbo.Firm");
            DropForeignKey("dbo.Distribution", "DistributionCustomer_IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.Distribution", "DistributionCreator_IdUser", "dbo.UserEntity");
            DropForeignKey("dbo.CurrencyExchange", "Currency_CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Complaint", "ServiceItem_IdItem", "dbo.ServiceItem");
            DropForeignKey("dbo.ServiceItem", "SaleType_IdSaleType", "dbo.SaleType");
            DropForeignKey("dbo.ServiceItem", "ItemType_IdItemType", "dbo.ServiceType");
            DropForeignKey("dbo.ServiceType", "ServiceType_IdItemType", "dbo.ServiceType");
            DropForeignKey("dbo.ServiceType", "ItemTax_IdTax", "dbo.TaxEntity");
            DropForeignKey("dbo.AttributeTypesInServiceType", "IdAttributeType", "dbo.SimpleAttributeType");
            DropForeignKey("dbo.AttributeTypesInServiceType", "IdItemType", "dbo.ServiceType");
            DropForeignKey("dbo.AttributesInServiceItems", "IdAttribute", "dbo.ValueAttributeType");
            DropForeignKey("dbo.AttributesInServiceItems", "IdItem", "dbo.ServiceItem");
            DropForeignKey("dbo.Complaint", "ResolverUser_IdUser", "dbo.UserEntity");
            DropForeignKey("dbo.UserInSafetyPointGroups", "IdSafetyPointGroup", "dbo.SafetyPointGroup");
            DropForeignKey("dbo.UserInSafetyPointGroups", "IdUser", "dbo.UserEntity");
            DropForeignKey("dbo.SafetyPointInGroups", "IdSafetyPoint", "dbo.SafetyPoint");
            DropForeignKey("dbo.SafetyPointInGroups", "IdSafetyPointGroup", "dbo.SafetyPointGroup");
            DropForeignKey("dbo.SafetyPoint", "TypeOfSafetyPoint_IdTypeOfSafetyPoint", "dbo.TypeOfSafetyPoint");
            DropForeignKey("dbo.UserEntity", "Role_IdRole", "dbo.RoleEntity");
            DropForeignKey("dbo.Complaint", "ProductItem_IdItem", "dbo.ProductItem");
            DropForeignKey("dbo.ProductItem", "SaleType_IdSaleType", "dbo.SaleType");
            DropForeignKey("dbo.ProductItem", "ItemType_IdItemType", "dbo.ProductType");
            DropForeignKey("dbo.ProductType", "ProductType_IdItemType", "dbo.ProductType");
            DropForeignKey("dbo.ProductType", "ItemTax_IdTax", "dbo.TaxEntity");
            DropForeignKey("dbo.AttributeTypesInProductType", "IdAttributeType", "dbo.SimpleAttributeType");
            DropForeignKey("dbo.AttributeTypesInProductType", "IdItemType", "dbo.ProductType");
            DropForeignKey("dbo.AttributesInProductItems", "IdAttribute", "dbo.ValueAttributeType");
            DropForeignKey("dbo.AttributesInProductItems", "IdItem", "dbo.ProductItem");
            DropForeignKey("dbo.Complaint", "Customer_IdCustomer", "dbo.Customer");
            DropIndex("dbo.SimpleCustomer", new[] { "IdCustomer" });
            DropIndex("dbo.Firm", new[] { "IdCustomer" });
            DropIndex("dbo.ProductItemsInSupplies", new[] { "IdProductItem" });
            DropIndex("dbo.ProductItemsInSupplies", new[] { "IdSupply" });
            DropIndex("dbo.DistributionsInStore", new[] { "IdDistribution" });
            DropIndex("dbo.DistributionsInStore", new[] { "IdStore" });
            DropIndex("dbo.EStoreCategoryInServiceItem", new[] { "IdItem" });
            DropIndex("dbo.EStoreCategoryInServiceItem", new[] { "IdEStoreCategory" });
            DropIndex("dbo.EStoreCategoryInProductItem", new[] { "IdItem" });
            DropIndex("dbo.EStoreCategoryInProductItem", new[] { "IdEStoreCategory" });
            DropIndex("dbo.EStoreInWarehouses", new[] { "IdWarehouse" });
            DropIndex("dbo.EStoreInWarehouses", new[] { "IdEStore" });
            DropIndex("dbo.ServiceWarehousesInBaseWarehouse", new[] { "IdServiceWarehouse" });
            DropIndex("dbo.ServiceWarehousesInBaseWarehouse", new[] { "IdWarehouse" });
            DropIndex("dbo.ServicesInServiceWarehouse", new[] { "IdServiceItem" });
            DropIndex("dbo.ServicesInServiceWarehouse", new[] { "IdServiceWarehouse" });
            DropIndex("dbo.ProductWarehousesInBaseWarehouse", new[] { "IdProductWarehouse" });
            DropIndex("dbo.ProductWarehousesInBaseWarehouse", new[] { "IdWarehouse" });
            DropIndex("dbo.ProductsInProductWarehouse", new[] { "IdProductItem" });
            DropIndex("dbo.ProductsInProductWarehouse", new[] { "IdProductWarehouse" });
            DropIndex("dbo.EStoreInEStoreShipmentType", new[] { "IdEStoreShipmentType" });
            DropIndex("dbo.EStoreInEStoreShipmentType", new[] { "IdEStore" });
            DropIndex("dbo.ServiceItemsInDistribution", new[] { "IdItem" });
            DropIndex("dbo.ServiceItemsInDistribution", new[] { "IdDistribution" });
            DropIndex("dbo.ProductItemsInDistribution", new[] { "IdItem" });
            DropIndex("dbo.ProductItemsInDistribution", new[] { "IdDistribution" });
            DropIndex("dbo.AttributeTypesInServiceType", new[] { "IdAttributeType" });
            DropIndex("dbo.AttributeTypesInServiceType", new[] { "IdItemType" });
            DropIndex("dbo.AttributesInServiceItems", new[] { "IdAttribute" });
            DropIndex("dbo.AttributesInServiceItems", new[] { "IdItem" });
            DropIndex("dbo.UserInSafetyPointGroups", new[] { "IdSafetyPointGroup" });
            DropIndex("dbo.UserInSafetyPointGroups", new[] { "IdUser" });
            DropIndex("dbo.SafetyPointInGroups", new[] { "IdSafetyPoint" });
            DropIndex("dbo.SafetyPointInGroups", new[] { "IdSafetyPointGroup" });
            DropIndex("dbo.AttributeTypesInProductType", new[] { "IdAttributeType" });
            DropIndex("dbo.AttributeTypesInProductType", new[] { "IdItemType" });
            DropIndex("dbo.AttributesInProductItems", new[] { "IdAttribute" });
            DropIndex("dbo.AttributesInProductItems", new[] { "IdItem" });
            DropIndex("dbo.StoreEntity", new[] { "Warehouse_IdWarehouse" });
            DropIndex("dbo.EStoreCategory", new[] { "EStore_IdEStore" });
            DropIndex("dbo.EmployeeEntity", new[] { "UserEntity_IdUser" });
            DropIndex("dbo.FactureItem", new[] { "Facture_IdFacture" });
            DropIndex("dbo.Distribution", new[] { "Facture_IdFacture" });
            DropIndex("dbo.Distribution", new[] { "DistributionProvider_IdCustomer" });
            DropIndex("dbo.Distribution", new[] { "DistributionCustomer_IdCustomer" });
            DropIndex("dbo.Distribution", new[] { "DistributionCreator_IdUser" });
            DropIndex("dbo.CurrencyExchange", new[] { "Facture_IdFacture" });
            DropIndex("dbo.CurrencyExchange", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.ServiceType", new[] { "ServiceType_IdItemType" });
            DropIndex("dbo.ServiceType", new[] { "ItemTax_IdTax" });
            DropIndex("dbo.ServiceItem", new[] { "SaleType_IdSaleType" });
            DropIndex("dbo.ServiceItem", new[] { "ItemType_IdItemType" });
            DropIndex("dbo.SafetyPoint", new[] { "TypeOfSafetyPoint_IdTypeOfSafetyPoint" });
            DropIndex("dbo.UserEntity", new[] { "Role_IdRole" });
            DropIndex("dbo.ProductType", new[] { "ProductType_IdItemType" });
            DropIndex("dbo.ProductType", new[] { "ItemTax_IdTax" });
            DropIndex("dbo.ProductItem", new[] { "SaleType_IdSaleType" });
            DropIndex("dbo.ProductItem", new[] { "ItemType_IdItemType" });
            DropIndex("dbo.Complaint", new[] { "ServiceItem_IdItem" });
            DropIndex("dbo.Complaint", new[] { "ResolverUser_IdUser" });
            DropIndex("dbo.Complaint", new[] { "ProductItem_IdItem" });
            DropIndex("dbo.Complaint", new[] { "Customer_IdCustomer" });
            DropTable("dbo.SimpleCustomer");
            DropTable("dbo.Firm");
            DropTable("dbo.ProductItemsInSupplies");
            DropTable("dbo.DistributionsInStore");
            DropTable("dbo.EStoreCategoryInServiceItem");
            DropTable("dbo.EStoreCategoryInProductItem");
            DropTable("dbo.EStoreInWarehouses");
            DropTable("dbo.ServiceWarehousesInBaseWarehouse");
            DropTable("dbo.ServicesInServiceWarehouse");
            DropTable("dbo.ProductWarehousesInBaseWarehouse");
            DropTable("dbo.ProductsInProductWarehouse");
            DropTable("dbo.EStoreInEStoreShipmentType");
            DropTable("dbo.ServiceItemsInDistribution");
            DropTable("dbo.ProductItemsInDistribution");
            DropTable("dbo.AttributeTypesInServiceType");
            DropTable("dbo.AttributesInServiceItems");
            DropTable("dbo.UserInSafetyPointGroups");
            DropTable("dbo.SafetyPointInGroups");
            DropTable("dbo.AttributeTypesInProductType");
            DropTable("dbo.AttributesInProductItems");
            DropTable("dbo.Supply");
            DropTable("dbo.StoreEntity");
            DropTable("dbo.EStoreCategory");
            DropTable("dbo.ServiceWarehouse");
            DropTable("dbo.ProductWarehouse");
            DropTable("dbo.Warehouse");
            DropTable("dbo.EStoreShipmentType");
            DropTable("dbo.EStore");
            DropTable("dbo.EmployeeEntity");
            DropTable("dbo.FactureItem");
            DropTable("dbo.Facture");
            DropTable("dbo.Distribution");
            DropTable("dbo.CurrencyExchange");
            DropTable("dbo.Currency");
            DropTable("dbo.ServiceType");
            DropTable("dbo.ServiceItem");
            DropTable("dbo.TypeOfSafetyPoint");
            DropTable("dbo.SafetyPoint");
            DropTable("dbo.SafetyPointGroup");
            DropTable("dbo.RoleEntity");
            DropTable("dbo.UserEntity");
            DropTable("dbo.SaleType");
            DropTable("dbo.TaxEntity");
            DropTable("dbo.ProductType");
            DropTable("dbo.ProductItem");
            DropTable("dbo.Customer");
            DropTable("dbo.Complaint");
            DropTable("dbo.SimpleAttributeType");
            DropTable("dbo.ValueAttributeType");
        }
    }
}
