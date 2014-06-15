using System;
using System.Collections.Generic;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Distributions;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Customers;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using NUnit.Framework;
using Project.Controllers.Warehouse.Distribution;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;

namespace Project.Tests.IntegrationTests
{
    [TestFixture]
    public class DistributionTest
    {
        private DistributionController m_disController;
        private IDataBaseContext m_dataBaseContext;
        [SetUp]
        public void Init()
        {
            m_dataBaseContext = MokujDbIWstawDystrybucje();
            RepositoryLogicFactory.DataBaseContext = m_dataBaseContext;
            m_disController = new DistributionController();
        }

        

        [Test]
        public void TestDisplayDistribution_dla_istniejacej_dystrybucji__zwraca_poprawny_obiekt()
        {
            //arrange
            var mapper = new DisplayItemDtoMapper();
            var dto = new DisplayDistributionDto
                {
                        DistributionCreatorName = "test",
                        IdDistribution = 1,
                        Customer =
                                new DisplayCustomerDto
                                    {
                                            Address = "testowa firma 1",
                                            Name = "Test Firm Customer",
                                            IdCustomer = 2,
                                            IsFirm = true
                                    },
                        State = (int) DistributionState.Prepared,
                        Items = m_dataBaseContext.ProductItems.Select(x=>mapper.MapEntityToDto(x))
                };
            //act
            var result = m_disController.DistributionLogic.GetDisplayDistribution(1);
            //assert
            result.Should().NotBeNull();
            result.ShouldHave().Properties(x=>x.DistributionCreatorName).Properties(x=>x.IdDistribution).Properties(x=>x.State).EqualTo(dto);
            result.Customer.ShouldHave().AllProperties().EqualTo(dto.Customer);
            int i = 0;
            foreach (var item in result.Items)
            {
                item.ShouldHave().AllProperties().EqualTo(dto.Items.ElementAt(i++));
            }
                
        }

        [Test]
        public void TestDisplayDistribution_dla_nie_istniejacej_dystrybucji__zwraca_null()
        {
            //act
            var result = m_disController.DistributionLogic.GetDisplayDistribution(2);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetAddDistribution__zwraca_poprawny_obiekt()
        {
            //act
            var result = m_disController.DistributionLogic.GetAddDistributionDto();
            //assert
            result.Should().NotBeNull();
            result.ChoicesCustomer.Should().NotBeEmpty();
            result.Items.Should().NotBeEmpty();
            result.Items.Should().HaveCount(5);
            result.ChoicesProvider.Should().NotBeEmpty();
            result.ChoicesCustomer.Should().HaveCount(3);
            result.ChoicesProvider.Should().HaveCount(2);
        }

        [Test]
        public void TestAddDistribution_dla_poprawnego_obiektu_male_item_quantity__zwraca_prawde_i_tworzy_itemy()
        {
            var now = DateTime.Now;
            var dto = new AddDistributionDto { DistributionDate = now.AddDays(2), DistributionTime = now.AddDays(2) ,SelectedCustomer = 1,SelectedProvider = 2,SelectedItems = new SelectedItem[]{new SelectedItem{ItemId = 6,ItemQuantity = 0.1,ItemTypeEnum = ItemTypeEnum.Product}, new SelectedItem{ItemId = 7,ItemQuantity = 0.1,ItemTypeEnum = ItemTypeEnum.Product} }};
            //act
            var result = m_disController.DistributionLogic.AddDistribution(dto,"test");
            //assert
            result.Should().NotBe(-1);
            var distribution = m_dataBaseContext.Distributions.FirstOrDefault(x => x.IdDistribution == result);
            distribution.Should().NotBeNull();
            distribution.State.Should().Be(DistributionState.Prepared);
            distribution.ProductItems.Should().HaveCount(2);
            distribution.ProductItems.First().Quantity.Should().BeLessOrEqualTo(0.1);
            distribution.DistributionCustomer.IdCustomer.Should().Be(1);
            distribution.DistributionProvider.IdCustomer.Should().Be(2);
            distribution.DistributionCreator.UserName.Should().Be("test");
            m_dataBaseContext.ProductItems.Should().HaveCount(12);
        }

        [Test]
        public void TestAddDistribution_dla_poprawnego_obiektu_max_quantity__zwraca_prawde_nie_tworzy_nowych_produktow()
        {
            var now = DateTime.Now;
            var dto = new AddDistributionDto { DistributionDate = now.AddDays(2), DistributionTime = now.AddDays(2), SelectedCustomer = 1, SelectedProvider = 2, SelectedItems = new SelectedItem[] { new SelectedItem { ItemId = 6, ItemQuantity = 100, ItemTypeEnum = ItemTypeEnum.Product }, new SelectedItem { ItemId = 7, ItemQuantity = 10, ItemTypeEnum = ItemTypeEnum.Product } } };
            //act
            var result = m_disController.DistributionLogic.AddDistribution(dto, "test");
            //assert
            result.Should().NotBe(-1);
            var distribution = m_dataBaseContext.Distributions.FirstOrDefault(x => x.IdDistribution == result);
            distribution.Should().NotBeNull();
            distribution.State.Should().Be(DistributionState.Prepared);
            distribution.ProductItems.Should().HaveCount(2);
            distribution.ProductItems.First().Quantity.Should().BeLessOrEqualTo(100);
            distribution.DistributionCustomer.IdCustomer.Should().Be(1);
            distribution.DistributionProvider.IdCustomer.Should().Be(2);
            distribution.DistributionCreator.UserName.Should().Be("test");
            m_dataBaseContext.ProductItems.Should().HaveCount(10);
        }

        [Test]
        public void TestAddDistribution_dla_poprawnego_obiektu_z_zerowym_quantity__zwraca_falsz()
        {
            var now = DateTime.Now;
            var dto = new AddDistributionDto { DistributionDate = now.AddDays(2), DistributionTime = now.AddDays(2), SelectedCustomer = 1, SelectedProvider = 2, SelectedItems = new SelectedItem[] { new SelectedItem { ItemId = 6, ItemQuantity = 0.0, ItemTypeEnum = ItemTypeEnum.Product }, new SelectedItem { ItemId = 7, ItemQuantity = 0.1, ItemTypeEnum = ItemTypeEnum.Product } } };
            //act
            var result = m_disController.DistributionLogic.AddDistribution(dto, "test");
            //assert
            result.Should().Be(-1);
        }

        [Test]
        public void TestAddDistribution_dla_poprawnego_obiektu_z_quantity_wiekszym_niz_product_quantity__zwraca_falsz()
        {
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem == 6).Quantity = 0.0;
            var now = DateTime.Now;
            var dto = new AddDistributionDto { DistributionDate = now.AddDays(2), DistributionTime = now.AddDays(2), SelectedCustomer = 1, SelectedProvider = 2, SelectedItems = new SelectedItem[] { new SelectedItem { ItemId = 6, ItemQuantity = 1.0, ItemTypeEnum = ItemTypeEnum.Product }, new SelectedItem { ItemId = 7, ItemQuantity = 0.1, ItemTypeEnum = ItemTypeEnum.Product } } };
            //act
            var result = m_disController.DistributionLogic.AddDistribution(dto, "test");
            //assert
            result.Should().Be(-1);
        }

        [Test]
        public void TestAddDistribution_dla_niepoprawnego_obiektu__zwraca_ze_nie_dodano()
        {
            var now = DateTime.Now;
            var dto = new AddDistributionDto { DistributionDate = now.AddDays(2), DistributionTime = now.AddDays(2)};
            //act
            var result = m_disController.DistributionLogic.AddDistribution(dto, "test");
            //assert
            result.Should().Be(-1);
        }

        [Test]
        public void TestPerformDistribution_dla_istniejacej_przygotowanej_dystrybucji__zwraca_prawde()
        {
            //act
            var result = m_disController.DistributionLogic.PerformDistribution(1);
            //assert
            result.Should().BeTrue();
            var distribution= m_dataBaseContext.Distributions.FirstOrDefault(x => x.IdDistribution == 1);
            distribution.State.Should().Be(DistributionState.Performed);
            distribution.ProductItems.ForEach(x=>x.ItemState.Should().Be(ItemState.Distributed));
        }

        [Test]
        public void TestPerformDistribution_dla_nie_istniejacej_przygotowanej_dystrybucji__zwraca_falsz()
        {
            //act
            var result = m_disController.DistributionLogic.PerformDistribution(2);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestGePerformDto_dla_istniejacej_przygotowanej_dystrybucji__zwraca_obiekt()
        {
            //act
            var result = m_disController.DistributionLogic.GetPerformDistribution(1);
            //assert
            result.Should().NotBeNull();
            result.IsPerformed.Should().BeFalse();
        }

        [Test]
        public void TestGePerformDto_dla_istniejacej_wykonanej_dystrybucji__zwraca_obiekt()
        {
            m_dataBaseContext.Distributions.FirstOrDefault(x=>x.IdDistribution==1).State = DistributionState.Performed;
            //act
            var result = m_disController.DistributionLogic.GetPerformDistribution(1);
            //assert
            result.Should().NotBeNull();
            result.IsPerformed.Should().BeTrue();
        }

        [Test]
        public void TestGePerformDto_dla_nie_istniejacej_przygotowanej_dystrybucji__zwraca_obiekt()
        {
            //act
            var result = m_disController.DistributionLogic.GetPerformDistribution(2);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetPerformDistributionDto_dla_nie_istniejacej_przygotowanej_dystrybucji__zwraca_null()
        {
            //act
            var result = m_disController.DistributionLogic.GetPerformDistribution(0);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetPerformDistributionDto_dla_istniejacej_przygotowanej_dystrybucji__zwraca_obiekt()
        {
            //act
            var result = m_disController.DistributionLogic.GetPerformDistribution(1);
            //assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(5);
            result.State.Should().Be((int) DistributionState.Prepared);
            result.IsPerformed.Should().BeFalse();
            result.Customer.IdCustomer.Should().Be(2);
            var distribution = m_dataBaseContext.Distributions.FirstOrDefault(x => x.IdDistribution == 1);
            var cost = distribution.ProductItems.Sum(x => x.Price);
            result.TotalCost.Should().BeGreaterOrEqualTo(cost);
        }

        [Test]
        public void TestGetAll_dla_istniejacej_przygotowanej_dystrybucji__zwraca_obiekt()
        {
            //act
            var result = m_disController.DistributionLogic.GetAllDistributionQueue();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            var distribution = result.First();
            distribution.ItemsCount.Should().Be(5);
            distribution.IdDistribution.Should().Be(1);
            distribution.State.Should().Be((int) DistributionState.Prepared);
        }

        [Test]
        public void TestGetEditDto_dla_istniejacej_przygotowanej_dystrybucji__zwraca_obiekt()
        {
            //act
            var result = m_disController.DistributionLogic.GetEditDistributionDto(1);
            //assert
            result.Should().NotBeNull();
            result.ChoicesCustomer.Should().HaveCount(3);
            result.IdDistribution.Should().Be(1);
            result.IsPerformed.Should().BeFalse();
        }


        [Test]
        public void TestGetEditDto_dla_istniejacej_wykonanej_dystrybucji__zwraca_obiekt()
        {
            m_dataBaseContext.Distributions.FirstOrDefault(x=>x.IdDistribution==1).State = DistributionState.Performed;
            //act
            var result = m_disController.DistributionLogic.GetEditDistributionDto(1);
            //assert
            result.Should().NotBeNull();
            result.ChoicesCustomer.Should().HaveCount(3);
            result.IdDistribution.Should().Be(1);
            result.IsPerformed.Should().BeTrue();
        }

        [Test]
        public void TestGetEditDto_dla_nie_istniejacej_dystrybucji__zwraca_null()
        {
            //act
            var result = m_disController.DistributionLogic.GetEditDistributionDto(2);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestEditDistribution_dla_poprawnego_obiektu__zwraca_prawde()
        {
            var dto = m_disController.DistributionLogic.GetEditDistributionDto(1);
            dto.SelectedChoiceCustomer = 2;
            dto.SelectedChoiceState = DistributionState.Performed;
            //act
            var result = m_disController.DistributionLogic.EditDistributionDto(dto);
            //assert
            result.Should().BeTrue();
        }

        [Test]
        public void TestEditDistribution_dla_nie_poprawnego_obiektu__zwraca_falsz()
        {
            var dto = m_disController.DistributionLogic.GetEditDistributionDto(1);
            dto.SelectedChoiceCustomer = 2;
            dto.SelectedChoiceState = DistributionState.Performed;
            dto.IdDistribution = 2;
            //act
            var result = m_disController.DistributionLogic.EditDistributionDto(dto);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProductItemFromDistribution_dla_poprawnego_obiektu__zwraca_prawde()
        {
            //act
            var result = m_disController.DistributionLogic.RemoveProductItemFrom(1, 1, ItemTypeEnum.Product);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Distributions.First(x => x.IdDistribution == 1).ProductItems.Should().HaveCount(4);
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem == 1)
                             .ItemState.Should()
                             .Be(ItemState.InWarehouse);
        }

        [Test]
        public void TestRemoveProductItemFromDistribution_usuwanie_itemu_ktory_nie_byl_w_dystrybucji__zwraca_falsz()
        {
            //act
            var result = m_disController.DistributionLogic.RemoveProductItemFrom(1, 6, ItemTypeEnum.Product);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProductItemFromDistribution_dla_nie_istniejacej_dystrybucji__zwraca_falsz()
        {
            //act
            var result = m_disController.DistributionLogic.RemoveProductItemFrom(2, 6, ItemTypeEnum.Product);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProductItemFromDistribution_dla_niepoprawnego_typu__zwraca_falsz()
        {
            //act
            var result = m_disController.DistributionLogic.RemoveProductItemFrom(1, 1, ItemTypeEnum.Service);
            //assert
            result.Should().BeFalse();
        }


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

            context.Distributions.Add(GetDistribution(products, creator, provider, customer));

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
