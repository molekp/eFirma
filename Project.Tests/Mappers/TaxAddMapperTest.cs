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
    /// test add tax mapper
    /// </summary>
    [TestFixture]
    public class TaxAddMapperTest
    {
        [Test]
        public void TaxEntityToTaxDtoTest()
        {
            var mapper = new TaxAddMapper();
            
            var taxAddDto = new TaxAddDto{
                IdTax = 1,
                TaxName = "A",
                TaxValue = 0.23
            };

            TaxEntity result = mapper.MapDtoToEntity(taxAddDto);
            //---
            Assert.AreEqual("A", result.TaxName);
            Assert.AreEqual(0.23, result.TaxValue);
        }
    }
}