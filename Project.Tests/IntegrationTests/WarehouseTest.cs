using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos;
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
using Project.Controllers.Warehouse;
using Project.Controllers.Warehouse.Distribution;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Project.Tests.IntegrationTests
{
    [TestFixture]
    public class WarehouseTest
    {
        private WarehouseController m_disController;
        private IDataBaseContext m_dataBaseContext;
        [SetUp]
        public void Init()
        {
            m_dataBaseContext = MokujDbIWstawWarehouse();
            RepositoryLogicFactory.DataBaseContext = m_dataBaseContext;
            m_disController = new WarehouseController();
        }

        [Test]
        public void TestIndexMessage__zwraca_view_z_message()
        {
            //act
            var result = m_disController.Index() as ViewResult;
            //assert
            Assert.AreEqual("Your Warehousepage", result.ViewBag.Message);
        }

        [Test]
        public void TestDisplayWarehouses__zwraca_wszystkie_magazyny()
        {
            //act
            var result = m_disController.WarehouseLogic.GetAllWarehouses();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            var warehouse=result.First();
            warehouse.IdWarehouse.Should().Be(1);
            warehouse.Name.Should().BeEquivalentTo(GetWarehouse().Name);
        }

        [Test]
        public void TestAddWarehouse_dla_poprawnego_obiektu__zwraca_prawde()
        {
            var dto = new AddWarehouseDto {Name = "nowy", Address = "nowy"};
            //act
            var result = m_disController.WarehouseLogic.AddWarehouse(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Warehouses.FirstOrDefault(x => x.Name.Equals(dto.Name)).Should().NotBeNull();
        }

        [Test]
        public void TestGetManageWarehouse_dla_istniejacego_magazynu__zwraca_obiekt()
        {
            //act
            var result = m_disController.WarehouseLogic.GetWarehouseForManage(1);
            //assert
            result.Should().NotBeNull();
            result.IdWarehouse.Should().Be(1);
            result.ProductWarehouses.Should().HaveCount(1);
        }

        [Test]
        public void TestGetManageWarehouse_dla_nieistniejacego_magazynu__zwraca_null()
        {
            //act
            var result = m_disController.WarehouseLogic.GetWarehouseForManage(2);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestManageWarehouse_dla_poprawnego_magazynu__zwraca_prawde()
        {
            var dto = m_disController.WarehouseLogic.GetWarehouseForManage(1);
            dto.Name = "test1";
            //act
            var result = m_disController.WarehouseLogic.EditWarehouse(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Warehouses.First().Name.Should().BeEquivalentTo("test1");
        }

        [Test]
        public void TestManageWarehouse_dla_nie_poprawnego_magazynu__zwraca_falsz()
        {
            var dto = m_disController.WarehouseLogic.GetWarehouseForManage(1);
            dto.Name = "test1";
            dto.IdWarehouse = 0;
            //act
            var result = m_disController.WarehouseLogic.EditWarehouse(dto);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestRemoveProductWarehouseFromWarehouse_dla_poprawnych_danych__zwraca_prawde()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveProductWarehouseFromWarehouse(1, 1);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Warehouses.First().ProductWarehouses.Should().HaveCount(0);
        }

        [Test]
        public void TestRemoveProductWarehouseFromWarehouse_magazyn_nie_ma_takiego_product_magazynu__zwraca_falsz()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveProductWarehouseFromWarehouse(1, 2);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Warehouses.First().ProductWarehouses.Should().HaveCount(1);
        }

        [Test]
        public void TestRemoveProductWarehouseFromWarehouse_nie_ma_takiego_magazynu__zwraca_falsz()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveProductWarehouseFromWarehouse(2, 1);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Warehouses.First().ProductWarehouses.Should().HaveCount(1);
        }

        [Test]
        public void TestRemoveServiceWarehouseFromWarehouse_dla_poprawnych_danych__zwraca_prawde()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveServiceWarehouseFromWarehouse(1, 1);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.Warehouses.First().ServiceWarehouses.Should().HaveCount(0);
        }

        [Test]
        public void TestRemoveServiceWarehouseFromWarehouse_magazyn_nie_ma_takiego_product_magazynu__zwraca_falsz()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveServiceWarehouseFromWarehouse(1, 2);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Warehouses.First().ServiceWarehouses.Should().HaveCount(1);
        }

        [Test]
        public void TestRemoveServiceWarehouseFromWarehouse_nie_ma_takiego_magazynu__zwraca_falsz()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveServiceWarehouseFromWarehouse(2, 1);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.Warehouses.First().ServiceWarehouses.Should().HaveCount(1);
        }

        [Test]
        public void TestGetProductWarehouse_magazyn_nie_ma_takiego_product_magazynu__zwraca_null()
        {
            //act
            var result = m_disController.WarehouseLogic.GetProductWarehouseForManage(2, 1);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetProductWarehouse_nie_ma_takiego_magazynu__zwraca_null()
        {
            //act
            var result = m_disController.WarehouseLogic.GetProductWarehouseForManage(1,2);
            //assert
            result.Should().BeNull();
        }

        [Test]
        public void TestGetProductWarehouse_poprawne_dane__zwraca_obiekt()
        {
            //act
            var result = m_disController.WarehouseLogic.GetProductWarehouseForManage(1,1);
            //assert
            result.Should().NotBeNull();
            result.IdProductWarehouse.Should().Be(1);
            result.IdWarehouse.Should().Be(1);
        }

        [Test]
        public void TestManageProductWarehouse_poprawny_obiekt__zwraca_prawde()
        {
            var dto = m_disController.WarehouseLogic.GetProductWarehouseForManage(1, 1);
            dto.Name = "test1";
            //act
            var result = m_disController.WarehouseLogic.EditProductWarehouse(dto);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == 1)
                             .Name.Should()
                             .BeEquivalentTo("test1");
        }

        [Test]
        public void TestManageProductWarehouse_nie_poprawny_obiekt__zwraca_falsz()
        {
            var dto = m_disController.WarehouseLogic.GetProductWarehouseForManage(1, 1);
            dto.Name = "test1";
            dto.IdWarehouse = 2;
            dto.IdProductWarehouse = 2;
            //act
            var result = m_disController.WarehouseLogic.EditProductWarehouse(dto);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == 1)
                             .Name.Should()
                             .NotBe("test1");
        }

        [Test]
        public void TestRemoveItemFromProductWarehouse_poprawne_dane__zwraca_prawde()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveProductItemFromProductWarehouse(6, 1);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == 1)
                             .ProductItems.Should()
                             .HaveCount(4);
        }

        [Test]
        public void TestRemoveItemFromProductWarehouse_nie_ma_takiego_produktu_w_magazynie__zwraca_falsz()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveProductItemFromProductWarehouse(1, 1);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == 1)
                             .ProductItems.Should()
                             .HaveCount(5);
        }

        [Test]
        public void TestRemoveItemFromProductWarehouse_nie_ma_takiego_magazynu__zwraca_falsz()
        {
            //act
            var result = m_disController.WarehouseLogic.RemoveProductItemFromProductWarehouse(6, 2);
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


            var taxes = GetTaxes();
            var productTypes = GetProductTypes(taxes);
            productTypes.ForEach(x=>context.ProductTypes.Add(x));

            var saleTypes = GetSaleTypes();
            saleTypes.ForEach(x=>context.SaleTypes.Add(x));

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
    }
}
