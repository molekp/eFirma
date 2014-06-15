using BussinessLogic.DTOs.Admin;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.Mappers.Safety;
using BussinessLogic.Mappers.Warehouse;
using NUnit.Framework;
using FluentAssertions;

namespace Project.Tests.Mappers.Warehouse
{
    /// <summary>
    /// Summary description for LogOnDtoMapperTest
    /// </summary>
    [TestFixture]
    public class AddWarehouseMapperTest
    {
        [Test]
        public void AddWarehouseDto_na_Warehouse()
        {
            var mapper = new AddWarehouseDtoMapper();
            var addSafetyPointGroup = new AddWarehouseDto {Name = "nowa",Address = "test"};
            //---
            var result = mapper.MapDtoToEntity(addSafetyPointGroup);
            //---
            result.Should().NotBeNull();
            result.Name.Should().BeEquivalentTo("nowa");
            result.Address.Should().BeEquivalentTo("test");
        }

        
    }
}