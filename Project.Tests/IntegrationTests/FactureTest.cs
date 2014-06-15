using System;
using System.Collections.Generic;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.Factures;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Distributions;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Customers;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Currencies;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using NUnit.Framework;
using Project.Controllers.Facture;
using Project.Controllers.Warehouse.Distribution;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;

namespace Project.Tests.IntegrationTests
{
    [TestFixture]
    public class FactureTest
    {
        private FactureController m_controller;
        private IDataBaseContext m_dataBaseContext;
        [SetUp]
        public void Init()
        {
            m_dataBaseContext = MokujDbIWstawDystrybucje();
            RepositoryLogicFactory.DataBaseContext = m_dataBaseContext;
            m_controller = new FactureController();
        }

        [Test]
        public void TestGetAllFactures()
        {
            // act
            var result = m_controller.FactureLogic.GetAllFactures();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Test]
        public void TestViewFacture_goodId()
        {
            // act
            var result = m_controller.FactureLogic.ViewFacture(1);
            //assert
            result.Should().NotBeNull();
            result.IdFacture.Should().Be(1);
        }

        [Test]
        public void TestViewFacture_wrongId()
        {
            // act
            var result = m_controller.FactureLogic.ViewFacture(100);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetAllFirms()
        {
            // act
            var result = m_controller.FactureLogic.GetAllFirms();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(6);
        }

        [Test]
        public void TestViewFirm_goodId()
        {
            // act
            var result = m_controller.FactureLogic.ViewFirm(11);
            //assert
            result.Should().NotBeNull();
            result.IdFirm.Should().Be(11);
            result.Name.Should().Be("Firm 2");
            result.NIP.Should().Be("123456789");
        }

        [Test]
        public void TestViewFirm_wrongId()
        {
            // act
            var result = m_controller.FactureLogic.ViewFirm(9);
            //assert
            result.Should().BeNull();
        }
        
        [Test]
        public void TestViewFirm_customerId()
        {
            // act
            var result = m_controller.FactureLogic.ViewFirm(3);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestAddFacture_wrongDistributionId()
        {
            //act
            var result = m_controller.FactureLogic.AddFacture(100);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestAddFacture_providerNull()
        {
            //act
            var result = m_controller.FactureLogic.AddFacture(10);
            //assert
            result.Should().BeNull();
        }
        
        [Test]
        public void TestAddFacture_customerNull()
        {
            //act
            var result = m_controller.FactureLogic.AddFacture(11);
            //assert
            result.Should().BeNull();
        }
        
        [Test]
        public void TestAddFacture_simpleCustomer()
        {
            //act
            var result = m_controller.FactureLogic.AddFacture(12);
            //assert
            result.Should().BeNull();
        }
        
        [Test]
        public void TestAddFacture_goodId_firms()
        {
            //act
            var result = m_controller.FactureLogic.AddFacture(1);
            //assert
            result.Should().NotBeNull();
            result.ClientName.Should().Be(GetCustomer().Name);
            result.ProviderName.Should().Be(GetFirm().Name);
            result.ProviderNIP.Should().Be(GetFirm().NIP);
        }

        [Test]
        public void TestSaveFacture_nullDto()
        {
            var dto = (FactureAddDto)null;
            //act
            var result = m_controller.FactureLogic.SaveFacture(dto);
            //assert
            result.Should().Be(0);
        }

        [Test]
        public void TestSaveFacture_nullItemIds()
        {
            var dto = new FactureAddDto{ItemIds = null};
            //act
            var result = m_controller.FactureLogic.SaveFacture(dto);
            //assert
            result.Should().Be(0);
        }

        [Test]
        public void TestSaveFacture_emptyIds()
        {
            var dto = new FactureAddDto
                          {
                                  ClientName = "test", 
                                  ClientAddress = "test", 
                                  ProviderName = "test",
                                  ProviderAddress = "test",
                                  CreationTime = DateTime.Now,
                                  RealizationTime = DateTime.Now,
                                  ExpirationTime = DateTime.Now,
                                  Issuer = "test",
                                  Reciver = "test",
                                  ItemIds = new int[0],
                                  CreationPlace = "test"
                          };
            //act
            var result = m_controller.FactureLogic.SaveFacture(dto);
            //assert
            result.Should().Be(m_dataBaseContext.Factures.FirstOrDefault(x=>x.ClientName.Equals("test")).IdFacture);
        }

        [Test]
        public void TestSaveFacture()
        {
            var dto = new FactureAddDto
                          {
                                  ClientName = "test", 
                                  ClientAddress = "test", 
                                  ProviderName = "test",
                                  ProviderAddress = "test",
                                  CreationTime = DateTime.Now,
                                  RealizationTime = DateTime.Now,
                                  ExpirationTime = DateTime.Now,
                                  Issuer = "test",
                                  Reciver = "test",
                                  ItemIds = new []{2,3},
                                  CreationPlace = "test"
                          };
            //act
            var result = m_controller.FactureLogic.SaveFacture(dto);
            //assert
            result.Should().Be(m_dataBaseContext.Factures.FirstOrDefault(x=>x.ClientName.Equals("test")).IdFacture);
        }
        
        [Test]
        public void TestGetItems_nullItemIds()
        {
            var list = (List<int>)null;
            //act
            var result = m_controller.FactureLogic.getItems(list);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestGetItems_emptyItemIds()
        {
            var list = new List<int>();
            //act
            var result = m_controller.FactureLogic.getItems(list);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestGetItems()
        {
            var list = new List<int> { 2, 3 };
            //act
            var result = m_controller.FactureLogic.getItems(list);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Test] 
        public void TestIdFacture_noFacture()
        {
            //act
            var result = m_controller.FactureLogic.idFacture(1);
            //assert
            result.Should().Be(0);
        }

        [Test] 
        public void TestIdFacture()
        {
            //act
            var result = m_controller.FactureLogic.idFacture(12);
            //assert
            result.Should().Be(m_dataBaseContext.Factures.First().IdFacture);
        }

        /*
         * Co można jeszcze testować :
         * 
        FactureAddDto getSum(FactureAddDto a_factureAddDto);
        List<SelectListItem> getCurrencies();
        ExchangeDto getExchange(int a_idFacture, int a_idCurrency);
        int AddFirm(FirmAddDto a_addFirmDto);
        FirmAddDto GetFirm(int a_idFirm);
        bool SaveFirm(FirmAddDto a_firmAddDto);
        bool RemoveFirm(int a_idFirm);         
        */

        private IDataBaseContext MokujDbIWstawDystrybucje()
        {
            var context = MockRepository.GenerateStub<IDataBaseContext>();
            context.ProductItems = new FakeDbSet<ProductItem>();
            context.Roles = new FakeDbSet<RoleEntity>();
            context.Customers = new FakeDbSet<Customer>();
            context.Distributions = new FakeDbSet<Distribution>();
            context.ProductTypes = new FakeDbSet<ProductType>();
            context.SaleTypes = new FakeDbSet<SaleType>();
            context.Users = new FakeDbSet<UserEntity>();
            context.ServiceItems = new FakeDbSet<ServiceItem>();
            context.Warehouses = new FakeDbSet<Warehouse>();
            context.ProductWarehouses = new FakeDbSet<ProductWarehouse>();
            context.FactureItems = new FakeDbSet<FactureItem>();
            context.Factures = new FakeDbSet<Facture>();
            context.Currencies = new FakeDbSet<Currency>();
            context.CurrencyExchanges = new FakeDbSet<CurrencyExchange>();
            
            var productTypes = GetProductTypes(CreateTaxes());
            productTypes.ForEach(x=>context.ProductTypes.Add(x));

            var saleTypes = GetSaleTypes();
            saleTypes.ForEach(x=>context.SaleTypes.Add(x));

            var products = GetProductsPreDistributed(productTypes, saleTypes);
            products.ForEach(x=>context.ProductItems.Add(x));

            var warehouse = GetWarehouse();
            context.Warehouses.Add(warehouse);

            var productsInWarehouse = GetProductsInWarehouse(productTypes, saleTypes);
            productsInWarehouse.ForEach(x=>context.ProductItems.Add(x));
            var productWarehouse = new ProductWarehouse
                {
                        IdProductWarehouse = 1,
                        Name = "sekcja produktów",
                        ProductItems = productsInWarehouse
                };
            context.ProductWarehouses.Add(productWarehouse);
            warehouse.ProductWarehouses.Add(productWarehouse);
            
            var provider = GetFirm();
            context.Customers.Add(provider);

            var role = new RoleEntity { IdRole = 2, NameRole = "employee" };
            context.Roles.Add(role);
            var creator = GetUser(role);
            context.Users.Add(creator);

            var customer = GetCustomer();
            context.Customers.Add(customer);
            context.Customers.Add(GetSimpleCustomer());

            context.Distributions.Add(GetDistribution(products, creator, provider, customer));
            context.Distributions.Add(GetDistributionNullProvider(products, creator, provider, customer));
            context.Distributions.Add(GetDistributionNullCustomer(products, creator, provider, customer));

            var firms = GetFirms();
            firms.ForEach(x => context.Customers.Add(x));

            var factureItems = GetFactureItems(products);
            factureItems.ForEach(x => context.FactureItems.Add(x));

            var currencies = GetCurrencies();
            currencies.ForEach(x => context.Currencies.Add(x));

            var currencyExchanges = GetCurrencyExchanges(currencies);
            currencyExchanges.ForEach(x => context.CurrencyExchanges.Add(x));

            var factures = GetFactures(firms, factureItems, currencyExchanges);
            factures.ForEach(x => context.Factures.Add(x));

            context.Distributions.Add(GetDistributionSimpleCustomer(products, creator, provider, GetSimpleCustomer(),factures.First()));

            context.SaveChanges();

            return context;
        }


        private Distribution GetDistribution(List<ProductItem> a_productItems, UserEntity a_creator, Firm a_provider, Customer a_customer)
        {
            return new Distribution
                {
                        ProductItems = a_productItems,
                        State = DistributionState.Prepared,
                        DistributionTime = DateTime.Now.AddDays(2),
                        DistributionCreateTime = DateTime.Now,
                        DistributionCreator = a_creator,
                        DistributionCustomer = a_customer,
                        DistributionProvider = a_provider,
                        IdDistribution = 1,
                        ServiceItems = new List<ServiceItem>()
                };
        }

        private Distribution GetDistributionNullProvider(List<ProductItem> a_productItems, UserEntity a_creator, Firm a_provider, Customer a_customer)
        {
            return new Distribution
            {
                ProductItems = a_productItems,
                State = DistributionState.Prepared,
                DistributionTime = DateTime.Now.AddDays(2),
                DistributionCreateTime = DateTime.Now,
                DistributionCreator = a_creator,
                DistributionCustomer = a_customer,
                DistributionProvider = null,
                IdDistribution = 10,
                ServiceItems = new List<ServiceItem>()
            };
        }

        private Distribution GetDistributionNullCustomer(List<ProductItem> a_productItems, UserEntity a_creator, Firm a_provider, Customer a_customer)
        {
            return new Distribution
            {
                ProductItems = a_productItems,
                State = DistributionState.Prepared,
                DistributionTime = DateTime.Now.AddDays(2),
                DistributionCreateTime = DateTime.Now,
                DistributionCreator = a_creator,
                DistributionCustomer = null,
                DistributionProvider = a_provider,
                IdDistribution = 11,
                ServiceItems = new List<ServiceItem>()
            };
        }

        private Distribution GetDistributionSimpleCustomer(List<ProductItem> a_productItems, UserEntity a_creator, Firm a_provider, Customer a_customer, Facture a_first)
        {
            return new Distribution
            {
                ProductItems = a_productItems,
                State = DistributionState.Prepared,
                DistributionTime = DateTime.Now.AddDays(2),
                DistributionCreateTime = DateTime.Now,
                DistributionCreator = a_creator,
                DistributionCustomer = a_customer,
                DistributionProvider = a_provider,
                IdDistribution = 12,
                ServiceItems = new List<ServiceItem>(),
                Facture = a_first
            };
        }

        private Warehouse GetWarehouse()
        {
            return new Warehouse
                {
                        IdWarehouse = 1,
                        Name = "Warehouse primary",
                        ProductWarehouses = new List<ProductWarehouse>(),
                        ServiceWarehouses = new List<ServiceWarehouse>(),
                        Address = "Kraków ul.Wielicka 12, 32-100 Kraków"
                };
        }

        private Customer GetCustomer()
        {
            return new Firm
            {
                Name = "Test Firm Customer",
                IdCustomer = 2,
                Address = "testowa firma 1",
                NIP = "2342423412679",
                BankAccountNumber = "2323424342",
                BankInfo = "BPH",
                Phone = "+483242342340"
            };
        }

        private Customer GetSimpleCustomer()
        {
            return new SimpleCustomer
            {
                Name = "Test Simple Customer",
                IdCustomer = 3,
                Address = "testowy simple"
            };
        }

        private Firm GetFirm()
        {
            return new Firm
            {
                Name = "Test Firm",
                IdCustomer = 1,
                Address = "testowa 1",
                NIP = "0821193412679",
                BankAccountNumber = "2342342",
                BankInfo = "PKO",
                Phone = "+4830492340"
            };
            
        }

        private UserEntity GetUser(RoleEntity role)
        {
            return new UserEntity
            {
                IdUser = 1,
                UserName = "test",
                Password = "test",
                EMail = "test@test.pl",
                Role = role
            };
        }

        private List<ProductItem> GetProductsPreDistributed(List<ProductType> a_itemtypes, List<SaleType> a_saleTypes)
        {
            return new List<ProductItem>{
                new ProductItem{ IdItem = 1, Name = "Skarpety", Price = new decimal(2.0), Quantity = 100, Vin = "35674768568", ItemState = ItemState.PreDistributed,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[1]},
                new ProductItem{ IdItem = 2, Name = "Ser zółty gouda", Price = new decimal(17.90), Quantity = 10, Vin = "6573683758", ItemState = ItemState.PreDistributed,ExpirationTime =DateTime.Now,ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]},
                new ProductItem{ IdItem = 3, Name = "Mleko", Price = new decimal(3.95), Quantity = 24, Vin = "8735742567", ItemState =ItemState.PreDistributed,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[2]},
                new ProductItem{ IdItem = 4, Name = "Spodnie", Price = new decimal(24.90), Quantity = 22, Vin = "145767567", ItemState = ItemState.PreDistributed,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[1]},
                new ProductItem{ IdItem = 5, Name = "Kurtki", Price = new decimal(49.99), Quantity = 13, Vin = "245767672", ItemState = ItemState.PreDistributed,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[0]}
            };
        }

        private List<ProductItem> GetProductsInWarehouse(List<ProductType> a_itemtypes, List<SaleType> a_saleTypes)
        {
            return new List<ProductItem>{
                new ProductItem{ IdItem = 6, Name = "Skarpety in", Price = new decimal(2.0), Quantity = 100, Vin = "35674768568", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[1]},
                new ProductItem{ IdItem = 7, Name = "Ser zółty gouda in", Price = new decimal(17.90), Quantity = 10, Vin = "6573683758", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now,ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]},
                new ProductItem{ IdItem = 8, Name = "Mleko in", Price = new decimal(3.95), Quantity = 24, Vin = "8735742567", ItemState =ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[2]},
                new ProductItem{ IdItem = 9, Name = "Spodnie in", Price = new decimal(24.90), Quantity = 22, Vin = "145767567", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[1]},
                new ProductItem{ IdItem = 10, Name = "Kurtki in", Price = new decimal(49.99), Quantity = 13, Vin = "245767672", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[0]}
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

        private List<SaleType> GetSaleTypes()
        {
            return new List<SaleType>
                {
                        new SaleType{ IdSaleType = 1,Name = "kg"},
                        new SaleType{ IdSaleType = 2,Name = "art"},
                        new SaleType{ IdSaleType = 3,Name = "l"}
                };
        }

        private List<ProductType> GetProductTypes(List<TaxEntity> a_taxes)
        {
            return new List<ProductType>{
                new ProductType{ IdItemType = 1, Name = "art spozywcze mleczne",ItemTax = a_taxes[0]},
                new ProductType{ IdItemType = 2, Name = "art przymyslowe meble",ItemTax = a_taxes[1]},
                new ProductType{ IdItemType = 3, Name = "art papiernicze",ItemTax = a_taxes[2]}
            };
        }
        
        private List<Firm> GetFirms()
        {
            return new List<Firm>
                {
                        new Firm{ IdCustomer = 10, Name = "Firm 1", Address = "krakow", NIP = "123456789",BankInfo = "PKO",BankAccountNumber = "12312311",Phone = "+48 23422 1 ",Info ="innfo1"},
                        new Firm{ IdCustomer = 11, Name = "Firm 2", Address = "radom", NIP = "123456789",BankInfo = "bph",BankAccountNumber = "12312312",Phone = "+48 23422 2 ",Info ="innfo2"},
                        new Firm{ IdCustomer = 12, Name = "Firm 3", Address = "katowice", NIP = "123456789",BankInfo = "wbk",BankAccountNumber = "12312313",Phone = "+48 23422 3 ",Info ="innfo3"},
                        new Firm{ IdCustomer = 13, Name = "Firm 4", Address = "wawa", NIP = "123456789",BankInfo = "ING",BankAccountNumber = "12312314",Phone = "+48 23422 4 ",Info ="innfo4"},
                };
        }

        private List<FactureItem> GetFactureItems(List<ProductItem> a_productItems)
        {
            var list = new List<FactureItem>();
            foreach (var productItem in a_productItems)
            {
                list.Add(new FactureItem {IdItem = productItem.IdItem, Name = productItem.Name, Price = productItem.Price, SaleTypeName = productItem.SaleType.Name, Quantity = productItem.Quantity, PKWiU = "12.34.56.78", TaxValue = productItem.ItemType.ItemTax.TaxValue });
            }
            return list;
        }


        private List<Facture> GetFactures(List<Firm> a_firms, List<FactureItem> a_factureItems, List<CurrencyExchange> a_currencyExchanges)
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
                new Facture{IdFacture = 1, ClientName = client1.Name, ClientAddress = client1.Address, ClientNIP = client1.NIP, ClientInfo = client1.Info, ProviderName = provider1.Name, ProviderAddress = provider1.Address, ProviderNIP = provider1.NIP, ProviderInfo = provider1.Info, ProviderBankAccountNumber = provider1.BankAccountNumber, ProviderBankInfo = provider1.BankInfo, CreationTime = DateTime.Now.AddDays(-1), ExpirationTime = DateTime.Now.AddDays(15), Issuer = "Sprzedawca Paweł Mirek", Reciver = "Odbiorca Andrzej Nieciekawy", RealizationTime = DateTime.Now.AddDays(-2), Items = items2, Paid = new decimal(99.87), Sum = new decimal(1292.47), SumString = "tysiąc dwieście dziewięćdziesiąt dwa złote czterdzieści siedem groszy", CreationPlace = "Warszawa", Payment = "gotówka", Exchanges = exchange1},
                new Facture{IdFacture = 2, ClientName = client2.Name, ClientAddress = client2.Address, ClientNIP = client2.NIP, ClientInfo = client2.Info, ProviderName = provider2.Name, ProviderAddress = provider2.Address, ProviderNIP = provider2.NIP, ProviderInfo = provider2.Info, ProviderBankAccountNumber = provider2.BankAccountNumber, ProviderBankInfo = provider2.BankInfo,  CreationTime = DateTime.Now, ExpirationTime = DateTime.Now.AddDays(14), Issuer = "Sprzedawca Jan Nowak", Reciver = "Odbiorca Adam Lol", RealizationTime = DateTime.Now.AddDays(-1), Items = items1, Paid = new decimal(10.00), Sum = new decimal(379), SumString = "trzysta siedemdziesiąt dziewięć złotych", CreationPlace = "Kraków", Payment = "przelew", Exchanges = exchange2},
            };
        }

        private List<Currency> GetCurrencies()
        {
            return new List<Currency>
                {
                        new Currency {CurrencyId = 1, Name = "Polski złoty", Code = "PLN"},
                        new Currency {CurrencyId = 2, Name = "Dolar amerykański", Code = "USD"},
                        new Currency {CurrencyId = 3, Name = "Frank szwajcarski", Code = "CHF"},
                        new Currency {CurrencyId = 4, Name = "Funt szterling", Code = "GBP"},
                        new Currency {CurrencyId = 5, Name = "Euro", Code = "EUR"},
                };
        }

        private List<CurrencyExchange> GetCurrencyExchanges(List<Currency> a_currencies)
        {
            var teraz = DateTime.Now;
            var wczoraj = new DateTime(teraz.Ticks - 86400000);
            var jutro = teraz.AddDays(1);
            return new List<CurrencyExchange>
                {
                        new CurrencyExchange{CurrencyExchangeId = 1, Currency = a_currencies[0], Exchange = new decimal(1.0), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{CurrencyExchangeId = 2, Currency = a_currencies[1], Exchange = new decimal(3.2865), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{CurrencyExchangeId = 3, Currency = a_currencies[2], Exchange = new decimal(4.2345), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{CurrencyExchangeId = 4, Currency = a_currencies[3], Exchange = new decimal(5.5234), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{CurrencyExchangeId = 5, Currency = a_currencies[4], Exchange = new decimal(2.3452), ExchangeCourseDate = wczoraj},
                        new CurrencyExchange{CurrencyExchangeId = 6, Currency = a_currencies[0], Exchange = new decimal(1.0), ExchangeCourseDate = teraz},
                        new CurrencyExchange{CurrencyExchangeId = 7, Currency = a_currencies[1], Exchange = new decimal(3.2524), ExchangeCourseDate = teraz},
                        new CurrencyExchange{CurrencyExchangeId = 8, Currency = a_currencies[2], Exchange = new decimal(4.1573), ExchangeCourseDate = teraz},
                        new CurrencyExchange{CurrencyExchangeId = 9, Currency = a_currencies[3], Exchange = new decimal(5.7153), ExchangeCourseDate = teraz},
                        new CurrencyExchange{CurrencyExchangeId = 10, Currency = a_currencies[4], Exchange = new decimal(2.5713), ExchangeCourseDate = teraz},
                        new CurrencyExchange{CurrencyExchangeId = 11, Currency = a_currencies[0], Exchange = new decimal(1.0), ExchangeCourseDate = jutro},
                        new CurrencyExchange{CurrencyExchangeId = 12, Currency = a_currencies[1], Exchange = new decimal(3.2224), ExchangeCourseDate = jutro},
                        new CurrencyExchange{CurrencyExchangeId = 13, Currency = a_currencies[2], Exchange = new decimal(4.4731), ExchangeCourseDate = jutro},
                        new CurrencyExchange{CurrencyExchangeId = 14, Currency = a_currencies[3], Exchange = new decimal(5.7341), ExchangeCourseDate = jutro},
                        new CurrencyExchange{CurrencyExchangeId = 15, Currency = a_currencies[4], Exchange = new decimal(2.4731), ExchangeCourseDate = jutro},
                };
        }

    }
}
