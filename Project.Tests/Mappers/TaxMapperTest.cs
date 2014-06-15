using System.ComponentModel.DataAnnotations;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Warehouse.Taxes;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using NUnit.Framework;

namespace Project.Tests.Mappers
{
    /// <summary>
    /// Summary description for LogOnDtoMapperTest
    /// </summary>
    [TestFixture]
    public class TaxMapperTest
    {
        [Test]
        public void TaxEntityToTaxDtoTest()
        {
            var mapper = new TaxMapper();
            
            var taxEntity = new TaxEntity{
                IdTax = 1,
                TaxName = "A",
                TaxValue = 0.23
            };

            TaxDto result = mapper.MapEntityToDto(taxEntity);
            //---
            Assert.AreEqual("A", result.TaxName);
            Assert.AreEqual(0.23, result.TaxValue);
        }
    }
}