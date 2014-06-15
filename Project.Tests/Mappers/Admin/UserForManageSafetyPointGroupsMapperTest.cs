using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.Mappers.Admin;
using Database.Entities;
using NUnit.Framework;
using FluentAssertions;

namespace Project.Tests.Mappers.Admin
{
    [TestFixture]
    public class UserForManageSafetyPointGroupsMapperTest
    {
        [Test]
        public void MapEntityToDto()
        {
            var mapper = new UserForManageSafetyPointGroupsMapper();
            var user = new UserEntity {IdUser = 1, UserName = "test"};
            var expected = new UserForManageSafetyPointGroups{IdUser = 1,UserName = "test"};
            //
            var result = mapper.MapEntityToDto(user, null, null);
            //
            result.ShouldHave().AllProperties().EqualTo(expected);
        }
    }
}
