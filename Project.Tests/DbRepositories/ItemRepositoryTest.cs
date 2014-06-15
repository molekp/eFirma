using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using NUnit.Framework;
using Rhino.Mocks;

namespace Project.Tests.DbRepositories
{
    [TestFixture]
    public class ItemRepositoryTest 
    {
        private IItemRepository m_itemRepository;

        [SetUp]
        public void Init()
        {
            m_itemRepository = new ItemRepository
                {
                    DataBaseContext = MokujDataBaseContextIWprowadzItemy()
                };
        }

        [Test]
        public void GetAllItemsTest()
        {
            //
            List<ProductItem> result = m_itemRepository.GetAllProductItems().ToList();
            //
            Assert.IsNotNull(result, "nie znaleziono itemów");
            Assert.AreEqual(5, result.Count(), "znaleziono nie wszystkie itemy");
        }

       
        private IDataBaseContext MokujDataBaseContextIWprowadzItemy()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            dataBaseContext.ProductItems = new FakeDbSet<ProductItem>();

            foreach (ProductItem itemEntity in ItemsList())
                dataBaseContext.ProductItems.Add(itemEntity);
            return dataBaseContext;
        }

        private List<Database.Entities.WarehouseEntities.Warehouse> WarehousesList()
        {
            return new List<Database.Entities.WarehouseEntities.Warehouse> {
                new Database.Entities.WarehouseEntities.Warehouse { IdWarehouse = 1, Name = "Warehouse primary"},
                new Database.Entities.WarehouseEntities.Warehouse { IdWarehouse = 2, Name= "Warehouse secondary"}
            };
        }

        private List<TaxEntity> TaxesList()
        {
            return new List<TaxEntity>{
                new TaxEntity{ IdTax = 1, TaxName = "A",TaxValue = 0.23},
                new TaxEntity{ IdTax = 2, TaxName = "B", TaxValue = 0.08},
                new TaxEntity{ IdTax = 3, TaxName = "C",TaxValue = 0.4},
                new TaxEntity{ IdTax = 4, TaxName = "D", TaxValue = 0.0}
            };
        }

        private List<ProductType> ItemTypesList()
        {
            return new List<ProductType>{
                new ProductType{ IdItemType = 1, Name = "Product"},
                new ProductType{ IdItemType = 2, Name = "Service"}
            };
        }


        private List<ProductItem> ItemsList()
        {
            List<TaxEntity> taxes = TaxesList();
            List<ProductType> itemtypes = ItemTypesList();
            List<Database.Entities.WarehouseEntities.Warehouse> warehouses = WarehousesList();
            return new List<ProductItem>{
                new ProductItem{ IdItem = 1, Name = "Skarpety", Price = new decimal(2.0), Quantity = 100, Vin = "35674768568", ItemState = ItemState.InWarehouse, ItemType = itemtypes[0]},
                new ProductItem{ IdItem = 2, Name = "Majty", Price = new decimal(17.90), Quantity = 65, Vin = "6573683758", ItemState = ItemState.InWarehouse,  ItemType = itemtypes[0]},
                new ProductItem{ IdItem = 3, Name = "Koszule", Price = new decimal(14.95), Quantity = 24, Vin = "8735742567", ItemState = ItemState.InWarehouse, ItemType = itemtypes[0]},
                new ProductItem{ IdItem = 4, Name = "Spodnie", Price = new decimal(24.90), Quantity = 22, Vin = "145767567", ItemState = ItemState.InWarehouse,  ItemType = itemtypes[0]},
                new ProductItem{ IdItem = 5, Name = "Kurtki", Price = new decimal(49.99), Quantity = 13, Vin = "245767672", ItemState = ItemState.InWarehouse, ItemType = itemtypes[0]}
            };
        }
    }
}