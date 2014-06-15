using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.Mappers.Admin;
using Database.Entities.Safety;
using NUnit.Framework;
using FluentAssertions;

namespace Project.Tests.Mappers.Admin
{
    [TestFixture]
    public class SafetyPointGroupForUserManageMapperTest
    {
        [Test]
        public void MapEntityToDto()
        {
            var mapper = new SafetyPointGroupForUserManageMapper();
            var group = new SafetyPointGroup {IdSafetyPointGroup = 1, GroupName = "test"};
            var expected = new SafetyPointGroupForUserManage {IdSafetyPointGroup = 1, NameOfsafetyPointGroup = "test"};
            //
            var result = mapper.MapEntityToDto(group);
            //
            result.Should().NotBeNull("wynik jest nullem");
            result.ShouldHave().AllProperties().EqualTo(expected);
        }
    }
}
