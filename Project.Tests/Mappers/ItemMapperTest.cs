using System.ComponentModel.DataAnnotations;
using BussinessLogic.DTOs;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Inventary;
using BussinessLogic.Mappers.Warehouse.Supplies;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using NUnit.Framework;

namespace Project.Tests.Mappers
{
    /// <summary>
    /// Summary description for LogOnDtoMapperTest
    /// </summary>
    [TestFixture]
    public class ItemMapperTest
    {
        [Test]
        public void ItemEntityToItemDtoTest()
        {
            var mapper = new SearchItemDtoMapper();
            
            var tax = new TaxEntity{
                IdTax = 1,
                TaxName = "A",
                TaxValue = 0.23
            };
            var itemType = new ProductType{
                IdItemType = 1,
                Name = "Product",
            };
            var itemEntity = new ProductItem {
                IdItem = 1,
                Name = "Wibrator",
                Price = new decimal(9.99),
                Quantity = 69,
                Vin = "696969696969",
                ItemState = ItemState.InWarehouse,
                ItemType = itemType
            };

            SearchItemDto resultSearchItemDto = mapper.MapEntityToDto(itemEntity,"","", 0,"");
            //---
            Assert.AreEqual("Wibrator", resultSearchItemDto.ItemName);
            Assert.AreEqual(new decimal(9.99), resultSearchItemDto.ItemPrice);
        }
    }
}