using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using BussinessLogic.DatabaseLogic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using NUnit.Framework;
using Project.Controllers.Warehouse.Supply;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Project.Tests.IntegrationTests
{
    [TestFixture]
    public class SupplyTest
    {
        private SupplyController m_disController;
        private IDataBaseContext m_dataBaseContext;

        [SetUp]
        public void Init()
        {
            m_dataBaseContext = MokujDbIWstawWarehouse();
            RepositoryLogicFactory.DataBaseContext = m_dataBaseContext;
            m_disController = new SupplyController();
        }

        [Test]
        public void TestGetProductTypes_existingTypes()
        {
            //act
            var result = m_disController.SupplyLogic.GetProductTypes(2);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Test]
        public void TestGetSaleTypes_existingTypes()
        {
            //act
            var result = m_disController.SupplyLogic.GetSaleTypes();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Test]
        public void TestGetAttributeTypes_toSmallId()
        {
            //act
            var result = m_disController.SupplyLogic.GetAttributeTypes(0);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestGetAttributeTypes_toBigId()
        {
            //act
            var result = m_disController.SupplyLogic.GetAttributeTypes(100);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestGetAttributeTypes_goodId()
        {
            //act
            var result = m_disController.SupplyLogic.GetAttributeTypes(6);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestAddSupply_resultTrue()
        {
            var dto = new SupplyAddDto { Firm = "testowa", RealizationTime = DateTime.Now };
            //act
            var result = m_disController.SupplyLogic.AddSupply(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.Firm.Equals(dto.Firm)).Should().NotBeNull();
        }

        [Test]
        public void TestAddSupply_resultFalse()
        {
            var dto = (SupplyAddDto)null;
            //act
            var result = m_disController.SupplyLogic.AddSupply(dto);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestGetAllSupplies()
        {
            //act
            var result = m_disController.SupplyLogic.GetAllSupplies();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Test]
        public void TestGetSupply_wrongId()
        {
            //act
            var result = m_disController.SupplyLogic.GetSupply(0);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetSupply_goodId()
        {
            //act
            var result = m_disController.SupplyLogic.GetSupply(2);
            //assert
            result.Should().NotBeNull();
            result.IdSupply.Should().Be(2);
        }

        [Test]
        public void TestSaveSupply_true()
        {
            var dto = new SupplyEditDto{IdSupply = 2, Firm = "testowa", State = 1, RealizationTime = DateTime.Now};
            //act
            var result = m_disController.SupplyLogic.SaveSupply(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(dto.IdSupply)).Firm.Should().Be(dto.Firm);
        }

        [Test]
        public void TestSaveSupply_false()
        {
            var dto = new SupplyEditDto { IdSupply = 100, Firm = "testowa", State = 1, RealizationTime = DateTime.Now };
            //act
            var result = m_disController.SupplyLogic.SaveSupply(dto);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(dto.IdSupply)).Should().BeNull();
        }

        [Test]
        public void TestRemoveSupply_false()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveSupply(0);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveSupply_true()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveSupply(2);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).Should().BeNull();
        }

        [Test]
        public void TestSendSupply_true()
        {
            //act
            var result = m_disController.SupplyLogic.SendSupply(2);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).State.Should().Be(3);
        }

        [Test]
        public void TestSendSupply_false()
        {
            //act
            var result = m_disController.SupplyLogic.SendSupply(100);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(100)).Should().BeNull();
        }

        [Test]
        public void TestAddProduct_fail()
        {
            var dto = new ProductAddDto{Name = "testowy"};
            //act
            var result = m_disController.SupplyLogic.AddProduct(dto);
            //assert
            result.Should().Be(0);
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.Name.Equals(dto.Name)).IdItem.Should().Be(0);
        }

        [Test]
        public void TestAddProduct_correct()
        {
            var dto = new ProductAddDto
                          {
                                  Name = "testowy",
                                  Price = new decimal(49.99),
                                  Quantity = 13,
                                  IdSupply = 2,
                                  IdSaleType = 1,
                                  SaleTypes = new List<SelectListItem>(),
                                  IdProductType = 1,
                                  ProductTypes = new List<SelectListItem>(),
                                  ExpirationTime = DateTime.Now,
                                  Attributes = new List<AttributeDto>()
                          };
            //act
            var result = m_disController.SupplyLogic.AddProduct(dto);
            //assert
            result.Should().Be(m_dataBaseContext.ProductItems.FirstOrDefault(x => x.Name.Equals(dto.Name)).IdItem);
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.Name.Equals(dto.Name)).Should().NotBeNull();
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.Name.Equals(dto.Name)).Price.Should().Be(dto.Price);
        }

        [Test]
        public void TestAddProductToSupply_goodProductId_goodSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.AddProductToSupply(2, 6);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).ProductItems.Should().Contain(x => x.IdItem.Equals(6));
        }

        [Test]
        public void TestAddProductToSupply_wrongProductId_goodSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.AddProductToSupply(2,11);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).ProductItems.Find(x => x.IdItem.Equals(11)).Should().BeNull();
        }

        [Test]
        public void TestAddProductToSupply_goodProductId_wrongSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.AddProductToSupply(100,6);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(100)).Should().BeNull();
        }

        [Test]
        public void TestAddProductToSupply_wrongProductId_wrongSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.AddProductToSupply(100,11);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(100)).Should().BeNull();
        }

        [Test]
        public void TestGetAttributes_goodId()
        {
            //act
            var result = m_disController.SupplyLogic.GetAttributes(6);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Test]
        public void TestGetAttributes_wrongId()
        {
            //act
            var result = m_disController.SupplyLogic.GetAttributes(11);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestStoreProduct_goodId()
        {
            //act
            var result = m_disController.SupplyLogic.StoreProduct(6);
            //assert
            result.Should().NotBeNull();
            result.IdProduct.Should().Be(6);
            result.Attributes.Should().NotBeNull();
        }

        [Test]
        public void TestStoreProduct_wrongId()
        {
            //act
            var result = m_disController.SupplyLogic.StoreProduct(11);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestStoreProduct_goodDto()
        {
            var dto = new ProductDto
                          {
                                  Name = "testowy",
                                  Price = new decimal(49.99),
                                  IdProduct = 6,
                                  IdProductWarehouse = 1
                          };
            //act
            var result = m_disController.SupplyLogic.StoreProduct(dto);
            //assert
            result.Should().Be(m_dataBaseContext.ProductItems.FirstOrDefault(x => x.Name.Equals(dto.Name)).IdItem);
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.Name.Equals(dto.Name)).Price.Equals(dto.Price);
        }

        [Test]
        public void TestStoreProduct_wrongDto()
        {
            var dto = new ProductDto();
            //act
            var result = m_disController.SupplyLogic.StoreProduct(dto);
            //assert
            result.Should().Be(0);
        }

        [Test]
        public void TestViewSupply_goodId()
        {
            //act
            var result = m_disController.SupplyLogic.ViewSupply(2);
            //assert
            result.Should().NotBeNull();
            result.Firm.Should().Be(m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).Firm);
        }

        [Test]
        public void TestViewSupply_wrongId()
        {
            //act
            var result = m_disController.SupplyLogic.ViewSupply(0);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetWarehousesWithProducts_wrongId()
        {
            //act
            var result = m_disController.SupplyLogic.GetWarehousesWithProducts(0);
            //assert
            result.Should().NotBeNull();
            result.Find(x => x.Selected.Equals(true)).Should().BeNull();
        }

        [Test]
        public void TestGetWarehousesWithProducts_goodId()
        {
            //act
            var result = m_disController.SupplyLogic.GetWarehousesWithProducts(1);
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Test]
        public void TestGetProductWarehouses_goodWarehouseId_goodProductWarehouseId()
        {
            //act
            var result = m_disController.SupplyLogic.GetProductWarehouses(1,1);
            //assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }

        [Test]
        public void TestGetProductWarehouses_goodWarehouseId_wrongProductWarehouseId()
        {
            //act
            var result = m_disController.SupplyLogic.GetProductWarehouses(1,100);
            //assert
            result.Should().NotBeNull();
            result.Find(x => x.Selected.Equals(true)).Should().BeNull();
        }

        [Test]
        public void TestGetProductWarehouses_wrongWarehouseId_goodProductWarehouseId()
        {
            //act
            var result = m_disController.SupplyLogic.GetProductWarehouses(100,1);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestGetProductWarehouses_wrongWarehouseId_wrongProductWarehouseId()
        {
            //act
            var result = m_disController.SupplyLogic.GetProductWarehouses(100, 100);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestRemoveProduct_goodProductId_goodSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProduct(6, 2);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem.Equals(6)).Should().BeNull();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).ProductItems.Find(x => x.IdItem.Equals(6)).Should().BeNull();
        }

        [Test]
        public void TestRemoveProduct_goodProductId_wrongSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProduct(6, 100);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem.Equals(6)).Should().BeNull();
        }

        [Test]
        public void TestRemoveProduct_wrongProductId_goodSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProduct(100, 2);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProduct_wrongProductId_wrongSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProduct(100, 100);
            //assert
            result.Should().BeFalse();
        }
        [Test]
        public void TestRemoveProductFromSupply_goodProductId_goodSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProductFromSupply(6, 2);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Supplies.FirstOrDefault(x => x.IdSupply.Equals(2)).ProductItems.Find(x => x.IdItem.Equals(6)).Should().BeNull();
        }

        [Test]
        public void TestRemoveProductFromSupply_goodProductId_wrongSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProductFromSupply(6, 100);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProductFromSupply_wrongProductId_goodSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProductFromSupply(100, 2);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProductFromSupply_wrongProductId_wrongSupplyId()
        {
            //act
            var result = m_disController.SupplyLogic.RemoveProductFromSupply(100, 100);
            //assert
            result.Should().BeFalse();
        }
        
        private IDataBaseContext MokujDbIWstawWarehouse()
        {
            var context = MockRepository.GenerateStub<IDataBaseContext>();
            context.ProductItems = new FakeDbSet<ProductItem>();
            context.ProductTypes = new FakeDbSet<ProductType>();
            context.SaleTypes = new FakeDbSet<SaleType>();
            context.ServiceTypes = new FakeDbSet<ServiceType>();
            context.ServiceItems = new FakeDbSet<ServiceItem>();
            context.ServiceItems = new FakeDbSet<ServiceItem>();
            context.Warehouses = new FakeDbSet<Warehouse>();
            context.ProductWarehouses = new FakeDbSet<ProductWarehouse>();
            context.ServiceWarehouses = new FakeDbSet<ServiceWarehouse>();
            context.Supplies = new FakeDbSet<Supply>();

            var taxes = GetTaxes();
            var productTypes = GetProductTypes(taxes);
            productTypes.ForEach(x=>context.ProductTypes.Add(x));

            var saleTypes = GetSaleTypes();
            saleTypes.ForEach(x=>context.SaleTypes.Add(x));

            var warehouse = GetWarehouse();
            context.Warehouses.Add(warehouse);

            var supplies = GetSupplies();
            supplies.ForEach(x => context.Supplies.Add(x));

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

            var serviceTypes = GetServiceTypes(taxes);
            serviceTypes.ForEach(x=>context.ServiceTypes.Add(x));
            var services = GetServices(serviceTypes, saleTypes);
            services.ForEach(x=>context.ServiceItems.Add(x));
            var serviceWarehouse = new ServiceWarehouse
                {
                        Name = "service warehouse",
                        IdServiceWarehouse = 1,
                        ServiceItems = services
                };
            context.ServiceWarehouses.Add(serviceWarehouse);
            warehouse.ServiceWarehouses.Add(serviceWarehouse);
            
            context.SaveChanges();

            return context;
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

        private List<TaxEntity> GetTaxes()
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

        private List<ServiceType> GetServiceTypes(List<TaxEntity> a_taxes)
        {
            return new List<ServiceType>{
                new ServiceType{ IdItemType = 1, Name = "usługa spożywcza",ItemTax = a_taxes[0]},
                new ServiceType{ IdItemType = 2, Name = "usługa materiałowa",ItemTax = a_taxes[1]},
                new ServiceType{ IdItemType = 3, Name = "usługa transportowa",ItemTax = a_taxes[2]}
            };
        }

        private List<ServiceItem> GetServices(List<ServiceType> a_itemtypes, List<SaleType> a_saleTypes)
        {
            return new List<ServiceItem>{
                new ServiceItem{ IdItem = 1, Name = "Produkcja Skarpety", Price = new decimal(2.0), Quantity = 100, Vin = "35674768568", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[1]},
                new ServiceItem{ IdItem = 2, Name = "Dojenie krowy", Price = new decimal(17.90), Quantity = 10, Vin = "6573683758", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now,ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]},
                new ServiceItem{ IdItem = 3, Name = "Gotowanie ciasta", Price = new decimal(3.95), Quantity = 24, Vin = "8735742567", ItemState =ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[2]},
                new ServiceItem{ IdItem = 4, Name = "Pranie", Price = new decimal(24.90), Quantity = 22, Vin = "145767567", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[1],SaleType = a_saleTypes[1]},
                new ServiceItem{ IdItem = 5, Name = "Szycie", Price = new decimal(49.99), Quantity = 13, Vin = "245767672", ItemState = ItemState.InWarehouse,ExpirationTime =DateTime.Now, ItemType = a_itemtypes[0],SaleType = a_saleTypes[0]}
            };
        }
        
        private List<Supply> GetSupplies()
        {
            return new List<Supply>
                {
                        new Supply{ IdSupply = 1,Firm = "Comarch", State = 1, RealizationTime = new DateTime(2013, 01, 11), ProductItems = new List<ProductItem>()},
                        new Supply{ IdSupply = 2,Firm = "Tesco", State = 1, RealizationTime = new DateTime(2013, 01, 11), ProductItems = new List<ProductItem>()},
                        new Supply{ IdSupply = 3,Firm = "Kaufland", State = 1, RealizationTime = new DateTime(2013, 01, 11), ProductItems = new List<ProductItem>()}
                };
        }
    }
}
