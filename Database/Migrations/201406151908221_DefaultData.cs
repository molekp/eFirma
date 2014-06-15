namespace Database.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Security;
    using Database.Entities;
    using Database.Entities.Customers;
    using Database.Entities.EStore;
    using Database.Entities.EStore.Category;
    using Database.Entities.Factures;
    using Database.Entities.Safety;
    using Database.Entities.Stores;
    using Database.Entities.WarehouseEntities;
    using System.Linq;
    using Database.Entities.WarehouseEntities.Currencies;
    using Database.Entities.WarehouseEntities.Product;
    using Database.Entities.WarehouseEntities.Service;
    using System.Data.Entity.Migrations;
    using Database.Core;

    public partial class DefaultData : DbMigration
    {
        public override void Up()
        {
            using (var ctx = new DataBaseContext())
            {
                this.CreateData(ctx);
            }
        }

        public override void Down()
        {
            //using(var ctx = new DataBaseContext())
            //{
            //    RoleEntity.
            //    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [{0}]");
            //}
        }


        private void CreateData(DataBaseContext a_context)
        {
            List<RoleEntity> roles = CreateListWithRoles();
            roles.ForEach(r => a_context.Roles.Add(r));
            a_context.SaveChanges();

            List<UserEntity> users = CreateListWithUsers(roles);
            users.ForEach(u => a_context.Users.Add(u));
            a_context.SaveChanges();
            //safety
            a_context.TypesOfSafetyPoints.Add(new TypeOfSafetyPoint
            {
                IdTypeOfSafetyPoint = 1,
                Name = "Warehouse"
            });
            a_context.TypesOfSafetyPoints.Add(new TypeOfSafetyPoint
            {
                IdTypeOfSafetyPoint = 2,
                Name = "Warehouse Products"
            });
            a_context.SaveChanges();


            //warehouse

            List<TaxEntity> taxes = CreateTaxes();
            taxes.ForEach(t => a_context.Taxes.Add(t));
            a_context.SaveChanges();

            List<ProductType> productTypes = CreateProductTypes(taxes);
            productTypes.ForEach(i => a_context.ProductTypes.Add(i));
            a_context.SaveChanges();

            List<ServiceType> serviceTypes = CreateServiceTypes(taxes);
            serviceTypes.ForEach(i => a_context.ServiceTypes.Add(i));
            a_context.SaveChanges();

            List<SaleType> saleTypes = CreateSaleTypes();
            saleTypes.ForEach(x => a_context.SaleTypes.Add(x));
            a_context.SaveChanges();

            var attributeTypes = CreateAttributeTypes();
            attributeTypes.ForEach(x => a_context.AttributeTypes.Add(x));
            a_context.SaveChanges();

            List<ProductItem> products = CreateProducts(productTypes, saleTypes);
            products.ForEach(i => a_context.ProductItems.Add(i));
            a_context.SaveChanges();

            List<ServiceItem> services = CreateServices(serviceTypes, saleTypes);
            services.ForEach(i => a_context.ServiceItems.Add(i));
            a_context.SaveChanges();

            List<Warehouse> warehouses = CreateWarehouses();
            warehouses.ForEach(w => a_context.Warehouses.Add(w));
            a_context.SaveChanges();

            List<Supply> supplies = CreateSupplies();
            supplies.ForEach(w => a_context.Supplies.Add(w));
            a_context.SaveChanges();

            warehouses.First().ProductWarehouses.Add(new ProductWarehouse
            {
                IdProductWarehouse = 1,
                Name = "sekcja produktów",
                ProductItems = products,
            });
            a_context.SaveChanges();

            warehouses.First().ServiceWarehouses.Add(new ServiceWarehouse
            {
                Name = "sekcja uslug",
                ServiceItems = services
            });
            a_context.SaveChanges();

            List<SimpleCustomer> customers = CreateCustomers();
            customers.ForEach(w => a_context.Customers.Add(w));
            a_context.SaveChanges();

            List<EmployeeEntity> employees = CreateEmployees(users);
            employees.ForEach(x => a_context.Employees.Add(x));
            a_context.SaveChanges();

            List<Firm> firms = CreateFirms();
            firms.ForEach(a => a_context.Customers.Add(a));
            a_context.SaveChanges();

            List<Distribution> distributions = CreateDistributions(products, services, firms, users);
            distributions.ForEach(w => a_context.Distributions.Add(w));
            a_context.SaveChanges();


            List<FactureItem> factureItems = CreateFactureItems(products);
            factureItems.ForEach(a => a_context.FactureItems.Add(a));
            a_context.SaveChanges();


            // eStore
            List<EStoreShipmentType> shipmentTypes = CreateEStoreShipmentType();
            shipmentTypes.ForEach(a => a_context.EStoreShipmentTypes.Add(a));
            a_context.SaveChanges();

            List<EStore> eStores = CreateEStores(warehouses, shipmentTypes);
            eStores.ForEach(a => a_context.EStore.Add(a));
            a_context.SaveChanges();

            List<EStoreCategory> eStoresCategory = CreateEStoreCategory(eStores);
            eStoresCategory.ForEach(a => a_context.EStoreCategory.Add(a));
            a_context.SaveChanges();

            List<StoreEntity> stores = CreateStores(warehouses);
            stores.ForEach(a => a_context.Stores.Add(a));
            a_context.SaveChanges();

            List<Currency> currencies = CreateCurrencies();
            currencies.ForEach(a => a_context.Currencies.Add(a));
            a_context.SaveChanges();

            List<CurrencyExchange> currencyExchanges = CreateCurrencyExchanges(currencies);
            currencyExchanges.ForEach(a => a_context.CurrencyExchanges.Add(a));
            a_context.SaveChanges();

            List<Facture> factures = CreateFactures(firms, factureItems, currencyExchanges);
            factures.ForEach(a => a_context.Factures.Add(a));
            a_context.SaveChanges();
        }


        #region list getters

        private List<RoleEntity> CreateListWithRoles()
        {
            return new List<RoleEntity> {
                new RoleEntity { IdRole = 1, NameRole = "admin"},
                new RoleEntity { IdRole = 2, NameRole = "employee" },
                new RoleEntity { IdRole = 3, NameRole = "customer" },
            };
        }

        private List<UserEntity> CreateListWithUsers(List<RoleEntity> a_roles)
        {
            return new List<UserEntity> {
                new UserEntity {
                    IdUser = 1,
                    UserName = "admin",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "md5"),
                    EMail = "admin@admin.com",
                    Role = a_roles[0]
                },
                new UserEntity {
                    IdUser = 3,
                    UserName = "admin1",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("admin1", "md5"),
                    EMail = "admin@admin.com",
                    Role = a_roles[0]
                },
                new UserEntity {
                    IdUser = 4,
                    UserName = "acki",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("acki", "md5"),
                    EMail = "acki@acki.com",
                    Role = a_roles[1]
                },
                new UserEntity {
                    IdUser = 5,
                    UserName = "ziomek",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("ziomek", "md5"),
                    EMail = "ziomek@ziomek.com",
                    Role = a_roles[1]
                },
                new UserEntity {
                    IdUser = 2,
                    UserName = "sprawdz",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("sprawdz", "md5"),
                    EMail = "sprawdz@sprawdz.com",
                    Role = a_roles[1]
                }
            };
        }
        private List<Warehouse> CreateWarehouses()
        {
            return new List<Warehouse> {
                new Warehouse { IdWarehouse = 1, Name = "Warehouse primary",ProductWarehouses = new List<ProductWarehouse>(),ServiceWarehouses = new List<ServiceWarehouse>(),Address = "Kraków ul.Wielicka 12, 32-100 Kraków"},
                new Warehouse { IdWarehouse = 2, Name= "Warehouse secondary",ProductWarehouses = new List<ProductWarehouse>(),ServiceWarehouses = new List<ServiceWarehouse>(),Address = "Kraków ul.Rostworowskiego 30, 32-100 Kraków"}
            };
        }

        private List<TaxEntity> CreateTaxes()
        {
            return new List<TaxEntity>{
                new TaxEntity{ IdTax = 1, TaxName = "A",TaxValue = 0.23},
                new TaxEntity{ IdTax = 2, TaxName = "B", TaxValue = 0.08},
                new TaxEntity{ IdTax = 3, TaxName = "C",TaxValue = 0.4},
                new TaxEntity{ IdTax = 4, TaxName = "D", TaxValue = 0.0}
            };
        }

        private List<SimpleAttributeType> CreateAttributeTypes()
        {
            return new List<SimpleAttributeType>
                {
                        new SimpleAttributeType{IdAttributeType = 1,Name = "weight"},
                        new SimpleAttributeType{IdAttributeType = 2,Name = "color"},
                        new SimpleAttributeType{IdAttributeType = 3,Name = "height"},
                        new SimpleAttributeType{IdAttributeType = 4,Name = "width"},
                };
        }

        private List<ProductType> CreateProductTypes(List<TaxEntity> a_taxes)
        {
            return new List<ProductType>{
                new ProductType{ IdItemType = 1, Name = "art spozywcze mleczne",ItemTax = a_taxes[0]},
                new ProductType{ IdItemType = 2, Name = "art przymyslowe meble",ItemTax = a_taxes[1]},
                new ProductType{ IdItemType = 3, Name = "art papiernicze",ItemTax = a_taxes[2]}
            };
        }

        private List<ServiceType> CreateServiceTypes(List<TaxEntity> a_taxes)
        {
            return new List<ServiceType>{
                new ServiceType{ IdItemType = 1, Name = "us³uga spo¿ywcza",ItemTax = a_taxes[0]},
                new ServiceType{ IdItemType = 2, Name = "us³uga materia³owa",ItemTax = a_taxes[1]},
                new ServiceType{ IdItemType = 3, Name = "us³uga transportowa",ItemTax = a_taxes[2]}
            };
        }

        private List<SaleType> CreateSaleTypes()
        {
            return new List<SaleType>
                {
                        new SaleType{ IdSaleType = 1,Name = "kg"},
                        new SaleType{ IdSaleType = 2,Name = "art"},
                        new SaleType{ IdSaleType = 3,Name = "l"}
                };
        }

        private List<Supply> CreateSupplies()
        {
            return new List<Supply>
                {
                        new Supply{ IdSupply = 1,Firm = "Comarch", State = 1, RealizationTime = new DateTime(2013, 01, 11)},
                        new Supply{ IdSupply = 2,Firm = "Tesco", State = 1, RealizationTime = new DateTime(2013, 01, 11)},
                        new Supply{ IdSupply = 3,Firm = "Kaufland", State = 1, RealizationTime = new DateTime(2013, 01, 11)}
                };
        }

        private List<ProductItem> CreateProducts(List<ProductType> a_itemtypes, List<SaleType> a_saleTypes)
        {
            return new List<ProductItem>{
                new ProductItem{ IdItem = 1, Name = "Skarpety", Price = new decimal(2.0), Quantity = 100, Vin = "35674768568", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[1]},
                new ProductItem{ IdItem = 2, Name = "Ser zó³ty gouda", Price = new decimal(17.90), Quantity = 10, Vin = "6573683758", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now,ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]},
                new ProductItem{ IdItem = 3, Name = "Mleko", Price = new decimal(3.95), Quantity = 24, Vin = "8735742567", ItemState =ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[2]},
                new ProductItem{ IdItem = 4, Name = "Spodnie", Price = new decimal(24.90), Quantity = 22, Vin = "145767567", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[1]},
                new ProductItem{ IdItem = 5, Name = "Kurtki", Price = new decimal(49.99), Quantity = 13, Vin = "245767672", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[0]}
            };
        }

        private List<ServiceItem> CreateServices(List<ServiceType> a_itemtypes, List<SaleType> a_saleTypes)
        {
            return new List<ServiceItem>{
                new ServiceItem{ IdItem = 1, Name = "Produkcja Skarpety", Price = new decimal(2.0), Quantity = 100, Vin = "35674768568", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[1]},
                new ServiceItem{ IdItem = 2, Name = "Dojenie krowy", Price = new decimal(17.90), Quantity = 10, Vin = "6573683758", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now,ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]},
                new ServiceItem{ IdItem = 3, Name = "Gotowanie ciasta", Price = new decimal(3.95), Quantity = 24, Vin = "8735742567", ItemState =ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[2]},
                new ServiceItem{ IdItem = 4, Name = "Pranie", Price = new decimal(24.90), Quantity = 22, Vin = "145767567", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[1]},
                new ServiceItem{ IdItem = 5, Name = "Szycie", Price = new decimal(49.99), Quantity = 13, Vin = "245767672", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]}
            };
        }

        private List<SimpleCustomer> CreateCustomers()
        {
            return new List<SimpleCustomer>
                {
                        new SimpleCustomer {Name = "Customer 1", Address = "Kraków"},
                        new SimpleCustomer {Name = "Customer 2", Address = "Warszawa"},
                        new SimpleCustomer {Name = "Customer 3", Address = "London"},
                        new SimpleCustomer {Name = "Customer 4", Address = "Berlin"},
                };
        }

        private List<Firm> CreateFirms()
        {
            return new List<Firm>
                {
                        new Firm{ Name = "Firm 1", Address = "krakow", NIP = "123456789",BankInfo = "PKO",BankAccountNumber = "12312311",Phone = "+48 23422 1 ",Info ="innfo1"},
                        new Firm{ Name = "Firm 2", Address = "radom", NIP = "123456789",BankInfo = "bph",BankAccountNumber = "12312312",Phone = "+48 23422 2 ",Info ="innfo2"},
                        new Firm{ Name = "Firm 3", Address = "katowice", NIP = "123456789",BankInfo = "wbk",BankAccountNumber = "12312313",Phone = "+48 23422 3 ",Info ="innfo3"},
                        new Firm{ Name = "Firm 4", Address = "wawa", NIP = "123456789",BankInfo = "ING",BankAccountNumber = "12312314",Phone = "+48 23422 4 ",Info ="innfo4"},
                };
        }

        private List<FactureItem> CreateFactureItems(List<ProductItem> a_productItems)
        {
            var list = new List<FactureItem>();
            foreach (var productItem in a_productItems)
            {
                list.Add(new FactureItem { IdItem = productItem.IdItem, Name = productItem.Name, Price = productItem.Price, SaleTypeName = productItem.SaleType.Name, Quantity = productItem.Quantity, PKWiU = "12.34.56.78", TaxValue = productItem.ItemType.ItemTax.TaxValue });
            }
            return list;
        }

        private List<Facture> CreateFactures(List<Firm> a_firms, List<FactureItem> a_factureItems, List<CurrencyExchange> a_currencyExchanges)
        {
            var items1 = a_factureItems.GetRange(0, 2);
            var items2 = a_factureItems.GetRange(2, 3);
            var client1 = a_firms[2];
            var client2 = a_firms[0];
            var provider1 = a_firms[3];
            var provider2 = a_firms[1];
            var exchange1 = a_currencyExchanges.GetRange(0, 5);
            var exchange2 = a_currencyExchanges.GetRange(5, 5);
            return new List<Facture>
            {
                new Facture{ClientName = client1.Name, ClientAddress = client1.Address, ClientNIP = client1.NIP, ClientInfo = client1.Info, ProviderName = provider1.Name, ProviderAddress = provider1.Address, ProviderNIP = provider1.NIP, ProviderInfo = provider1.Info, ProviderBankAccountNumber = provider1.BankAccountNumber, ProviderBankInfo = provider1.BankInfo, CreationTime = DateTime.Now.AddDays(-1), ExpirationTime = DateTime.Now.AddDays(15), Issuer = "Sprzedawca Pawe³ Mirek", Reciver = "Odbiorca Andrzej Nieciekawy", RealizationTime = DateTime.Now.AddDays(-2), Items = items2, Paid = new decimal(99.87), Sum = new decimal(1292.47), SumString = "tysi¹c dwieœcie dziewiêædziesi¹t dwa z³ote czterdzieœci siedem groszy", CreationPlace = "Warszawa", Payment = "gotówka", Exchanges = exchange1},
                new Facture{ClientName = client2.Name, ClientAddress = client2.Address, ClientNIP = client2.NIP, ClientInfo = client2.Info, ProviderName = provider2.Name, ProviderAddress = provider2.Address, ProviderNIP = provider2.NIP, ProviderInfo = provider2.Info, ProviderBankAccountNumber = provider2.BankAccountNumber, ProviderBankInfo = provider2.BankInfo,  CreationTime = DateTime.Now, ExpirationTime = DateTime.Now.AddDays(14), Issuer = "Sprzedawca Jan Nowak", Reciver = "Odbiorca Adam Lol", RealizationTime = DateTime.Now.AddDays(-1), Items = items1, Paid = new decimal(10.00), Sum = new decimal(379), SumString = "trzysta siedemdziesi¹t dziewiêæ z³otych", CreationPlace = "Kraków", Payment = "przelew", Exchanges = exchange2},
            };
        }

        private List<Distribution> CreateDistributions(List<ProductItem> a_products, List<ServiceItem> a_services, List<Firm> a_customers, List<UserEntity> a_users)
        {
            return new List<Distribution>
                {
                        new Distribution{DistributionCustomer = a_customers.ElementAt(1),  DistributionProvider = a_customers.ElementAt(0), ProductItems = a_products, State = 0, DistributionCreateTime = DateTime.Now, DistributionCreator = a_users.First(), DistributionTime = DateTime.Now.AddDays(7)},
                        new Distribution{DistributionCustomer = a_customers.ElementAt(2),  DistributionProvider = a_customers.ElementAt(3), ServiceItems = a_services, State = DistributionState.Prepared, DistributionCreateTime = DateTime.Now, DistributionCreator = a_users.ElementAt(1),DistributionTime = DateTime.Now.AddDays(7)}
                };
        }


        private List<EmployeeEntity> CreateEmployees(List<UserEntity> a_users)
        {
            return new List<EmployeeEntity>
                {
                        new EmployeeEntity
                            {
                                    UserEntity = a_users[ 0 ],
                                    FirstName = "Fajny",
                                    LastName = "Koles",
                                    Address = "Krakow",
                                    BankAccountNumber = "122",
                                    Phone = "+4823 234 234",
                                    Salary = (decimal) 2000.00
                            },
                        new EmployeeEntity
                            {
                                    UserEntity = a_users[ 1 ],
                                    FirstName = "Fajny1",
                                    LastName = "Koles1",
                                    Address = "Krakow1",
                                    BankAccountNumber = "1221",
                                    Phone = "+4823 234 2341",
                                    Salary = (decimal) 2001.00
                            },
                        new EmployeeEntity
                            {
                                    UserEntity = a_users[ 2 ],
                                    FirstName = "Fajny2",
                                    LastName = "Koles2",
                                    Address = "Krakow2",
                                    BankAccountNumber = "234234",
                                    Phone = "+4823 234 2342",
                                    Salary = (decimal) 2002.00
                            },
                        new EmployeeEntity
                            {
                                    UserEntity = a_users[ 3 ],
                                    FirstName = "Fajny3",
                                    LastName = "Koles3",
                                    Address = "Krakow3",
                                    BankAccountNumber = "1223",
                                    Phone = "+4823 234 2343",
                                    Salary = (decimal) 2003.00
                            },

                };
        }

        private List<EStore> CreateEStores(List<Warehouse> warehouses, List<EStoreShipmentType> a_shipmentTypes)
        {
            return new List<EStore>
                {
                        new EStore() { Name = "eSklep spo¿ywczy", UniqHash =  Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToString())) , Warehouses = warehouses, EStoreShipmentType = a_shipmentTypes},
                        new EStore() { Name = "eSklep odzie¿",  UniqHash =  Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToString())), Warehouses = new List<Warehouse>(), EStoreShipmentType = new List<EStoreShipmentType>()}
                };
        }

        private List<StoreEntity> CreateStores(List<Warehouse> a_warehouses)
        {
            int i = 1;
            return a_warehouses.Select(warehouse => new StoreEntity
            {
                Name = (i++) + "",
                Warehouse = warehouse
            }).ToList();
        }

        private List<EStoreCategory> CreateEStoreCategory(List<EStore> a_eStore)
        {
            return new List<EStoreCategory>()
                      {
                          new EStoreCategory(){ Name = "Nabial" , SortOrder = 1, EStore = a_eStore.FirstOrDefault(), ProductItems = new List<ProductItem>(), ServiceItems = new List<ServiceItem>()},
                          new EStoreCategory(){ Name = "Odziez" , SortOrder = 2, EStore = a_eStore.FirstOrDefault(), ProductItems = new List<ProductItem>(), ServiceItems = new List<ServiceItem>()}
                      };
        }


        private List<Currency> CreateCurrencies()
        {
            return new List<Currency>
                {
                        new Currency {Name = "Polski z³oty", Code = "PLN"},
                        new Currency {Name = "Dolar amerykañski", Code = "USD"},
                        new Currency {Name = "Frank szwajcarski", Code = "CHF"},
                        new Currency {Name = "Funt szterling", Code = "GBP"},
                        new Currency {Name = "Euro", Code = "EUR"},
                };
        }

        private List<CurrencyExchange> CreateCurrencyExchanges(List<Currency> a_currencies)
        {
            var teraz = DateTime.Now;
            var wczoraj = new DateTime(teraz.Ticks - 86400000);
            var jutro = teraz.AddDays(1);
            return new List<CurrencyExchange>
                {
                        new CurrencyExchange{ Currency = a_currencies[0], Exchange = new decimal(1.0), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{ Currency = a_currencies[1], Exchange = new decimal(3.2865), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{ Currency = a_currencies[2], Exchange = new decimal(4.2345), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{ Currency = a_currencies[3], Exchange = new decimal(5.5234), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{ Currency = a_currencies[4], Exchange = new decimal(2.3452), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{ Currency = a_currencies[0], Exchange = new decimal(1.0), ExchangeCourseDate = teraz},
                        new CurrencyExchange{ Currency = a_currencies[1], Exchange = new decimal(3.2524), ExchangeCourseDate = teraz},
                        new CurrencyExchange{ Currency = a_currencies[2], Exchange = new decimal(4.1573), ExchangeCourseDate = teraz},
                        new CurrencyExchange{ Currency = a_currencies[3], Exchange = new decimal(5.7153), ExchangeCourseDate = teraz},
                        new CurrencyExchange{ Currency = a_currencies[4], Exchange = new decimal(2.5713), ExchangeCourseDate = teraz},
                        new CurrencyExchange{ Currency = a_currencies[0], Exchange = new decimal(1.0), ExchangeCourseDate = jutro},
                        new CurrencyExchange{ Currency = a_currencies[1], Exchange = new decimal(3.2224), ExchangeCourseDate = jutro},
                        new CurrencyExchange{ Currency = a_currencies[2], Exchange = new decimal(4.4731), ExchangeCourseDate = jutro},
                        new CurrencyExchange{ Currency = a_currencies[3], Exchange = new decimal(5.7341), ExchangeCourseDate = jutro},
                        new CurrencyExchange{ Currency = a_currencies[4], Exchange = new decimal(2.4731), ExchangeCourseDate = jutro},
                };
        }

        private List<EStoreShipmentType> CreateEStoreShipmentType()
        {
            return new List<EStoreShipmentType>
                {
                        new EStoreShipmentType(){ Name = "List ekonomiczny", Info = "dostawa 6-10 dni", PriceShipment = 6, SortOrder = 1},
                        new EStoreShipmentType(){ Name = "List polecony", Info = "dostawa 3-6 dni", PriceShipment = 7, SortOrder = 2},
                        new EStoreShipmentType(){ Name = "List polecony priorytet", Info = "dostawa 2-4 dni", PriceShipment = 9, SortOrder = 3},
                        new EStoreShipmentType(){ Name = "Kurier", Info = "dostawa 1-3 dni", PriceShipment = 15, SortOrder = 4},
                        new EStoreShipmentType(){ Name = "Kurier pobranie", Info = "dostawa 1-3 dni", PriceShipment = 18, SortOrder = 5}
                };
        }
        #endregion

    }
}
