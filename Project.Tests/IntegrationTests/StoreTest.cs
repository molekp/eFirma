using System;
using System.Collections.Generic;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.Store;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Distributions;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Customers;
using Database.Entities.Stores;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using NUnit.Framework;
using Project.Controllers.Store;
using Project.Controllers.Warehouse.Distribution;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;

namespace Project.Tests.IntegrationTests
{
    [TestFixture]
    public class StoreTest
    {
        private StoreController m_disController;
        private IDataBaseContext m_dataBaseContext;
        [SetUp]
        public void Init()
        {
            m_dataBaseContext = MokujDbIWstawSklepy();
            RepositoryLogicFactory.DataBaseContext = m_dataBaseContext;
            m_disController = new StoreController();
        }


        [Test]
        public void TestGetAllDisplayStore__zwraca_liste_sklepow()
        {
            //act
            var result = m_disController.StoreLogic.GetAllDisplayStoreDto();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Test]
        public void TestGetAllDisplayStore_nie_ma_sklepow__zwraca_pusta_liste()
        {
            m_dataBaseContext.Stores = new FakeDbSet<StoreEntity>();
            //act
            var result = m_disController.StoreLogic.GetAllDisplayStoreDto();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Test]
        public void TestGetDisplayStore_dla_istniejacego_sklepu__zwraca_obiekt()
        {
            //act
            var result = m_disController.StoreLogic.GetDisplayStoreDto(1);
            //assert
            result.Should().NotBeNull();
            result.Name.Should().BeEquivalentTo("store1");
            result.Sells.Should().HaveCount(1);
        }

        [Test]
        public void TestGetDisplayStore_dla_nie_istniejacego_sklepu__zwraca_null()
        {
            //act
            var result = m_disController.StoreLogic.GetDisplayStoreDto(3);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetAllSoldItem_dla_istniejacego_sklepu_bez_sprzedanych_itemow__zwraca_liste_sprzedanych_w_sklepie_itemow()
        {
            //act
            var result = m_disController.StoreLogic.GetAllSoldItemDto(2);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Test]
        public void TestGetAllSoldItem_dla_istniejacego_sklepu__zwraca_liste_sprzedanych_w_sklepie_itemow()
        {
            //act
            var result = m_disController.StoreLogic.GetAllSoldItemDto(1);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }


        [Test]
        public void TestGetAllSoldItem_dla_nie_istniejacego_sklepu__zwraca_null()
        {
            //act
            var result = m_disController.StoreLogic.GetAllSoldItemDto(3);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetComplaint_dla_poprawnych_danych__zwraca_obiekt()
        {
            //act
            var result = m_disController.StoreLogic.GetComplaintDto(1, 1, ItemTypeEnum.Product);
            //assert
            result.Should().NotBeNull();
            result.DistributionId.Should().Be(1);
            result.ItemId.Should().Be(1);
            result.ItemType.Should().Be(ItemTypeEnum.Product);
        }

        [Test]
        public void TestGetComplaint_dla_itemu_nie_z_dystrybucji__zwraca_null()
        {
            //act
            var result = m_disController.StoreLogic.GetComplaintDto(1, 6, ItemTypeEnum.Product);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetComplaint_dla_nie_istniejacej_dystrybucji__zwraca_null()
        {
            //act
            var result = m_disController.StoreLogic.GetComplaintDto(2, 1, ItemTypeEnum.Product);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetComplaint_dla_zlozonego_juz_zazalenia__zwraca_null()
        {
            var item = m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem == 1);
            m_dataBaseContext.Complaints.Add(new Complaint {ProductItem = item});
            //act
            var result = m_disController.StoreLogic.GetComplaintDto(1, 1, ItemTypeEnum.Product);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestAddComplaint_dla_poprawnego_zazalenia__zwraca_prawde()
        {
            var dto= m_disController.StoreLogic.GetComplaintDto(1, 1, ItemTypeEnum.Product);
            dto.ComplaintDescription = "blabla";
            //act
            var result = m_disController.StoreLogic.AddComplaint(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Complaints.Should().HaveCount(1);
            m_dataBaseContext.Complaints.FirstOrDefault().Description.Should().BeEquivalentTo(dto.ComplaintDescription);
        }

        [Test]
        public void TestAddComplaint_dla_zazalenia_bez_opisu__zwraca_falsz()
        {
            var dto = m_disController.StoreLogic.GetComplaintDto(1, 1, ItemTypeEnum.Product);
            //act
            var result = m_disController.StoreLogic.AddComplaint(dto);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Complaints.Should().HaveCount(0);
        }

        [Test]
        public void TestReturnItem_dla_poprawnego_zwrotu__zwraca_prawde()
        {
            var dto = m_disController.StoreLogic.GetReturnDto(1, 1, ItemTypeEnum.Product);
            dto.SelectedWarehouse = 1;
            //act
            var result = m_disController.StoreLogic.ReturnItem(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == 1)
                             .ProductItems.Should()
                             .HaveCount(6);
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem == 1)
                             .ItemState.Should()
                             .Be(ItemState.InWarehouse);
        }

        [Test]
        public void TestReturnItem_dla_niepoprawnego_zwrotu__zwraca_falsz()
        {
            var dto = m_disController.StoreLogic.GetReturnDto(1, 1, ItemTypeEnum.Product);
            //act
            var result = m_disController.StoreLogic.ReturnItem(dto);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == 1)
                             .ProductItems.Should()
                             .HaveCount(5);
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem == 1)
                             .ItemState.Should()
                             .Be(ItemState.PreDistributed);
            m_dataBaseContext.Distributions.FirstOrDefault(x => x.IdDistribution == 1)
                             .ProductItems.Should()
                             .HaveCount(5);
        }

        [Test]
        public void TestReturnItem_dla_nullowego_zwrotu__zwraca_falsz()
        {
            var dto = new ReturnDto();
            //act
            var result = m_disController.StoreLogic.ReturnItem(dto);
            //assert
            result.Should().BeFalse();
        }

        private IDataBaseContext MokujDbIWstawSklepy()
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
            context.Stores = new FakeDbSet<StoreEntity>();
            context.Complaints = new FakeDbSet<Complaint>(); ;
            

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

            var distribution = GetDistribution(products, creator, provider, customer);
            context.Distributions.Add(distribution);

            GetStores(warehouse,distribution).ForEach(x=>context.Stores.Add(x));

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

        private List<StoreEntity> GetStores(Warehouse a_warehouses, Distribution a_distribution)
        {
            var tmp= new List<StoreEntity>
                {
                        new StoreEntity
                            {
                                    Distributions = new List<Distribution>{},
                                    Name = "store1",
                                    Warehouse = a_warehouses,
                                    IdStore = 1
                            },
                        new StoreEntity
                            {
                                    Distributions = new List<Distribution>(),
                                    Name = "store2",
                                    Warehouse = a_warehouses,
                                    IdStore = 2
                            }
                };
            tmp[0].Distributions.Add(a_distribution);
            return tmp;
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
    }
}
