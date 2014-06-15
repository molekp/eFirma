using BussinessLogic.DTOs.Admin;
using BussinessLogic.Mappers.Safety;
using Database.Entities.Safety;
using NUnit.Framework;
using FluentAssertions;

namespace Project.Tests.Mappers.Safety
{
    /// <summary>
    /// Summary description for LogOnDtoMapperTest
    /// </summary>
    [TestFixture]
    public class AddSafetyPointGroupMapperTest
    {
        [Test]
        public void AddNewSafetyPointDto_na_UserEntity_Test()
        {
            var mapper = new AddSafetyPointGroupMapper();
            var addSafetyPointGroup = new AddSafetyPointGroup() {GroupName = "nowa"};
            //---
            var result = mapper.MapDtoToEntity(addSafetyPointGroup);
            //---
            result.Should().NotBeNull();
            result.GroupName.Should().BeEquivalentTo("nowa");
        }

        
    }
}