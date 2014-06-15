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
   public class AdminDisplayUserDtoMapperTest
    {

         [Test]
         public void MapEntityToDto()
         {
             var mapper = new AdminDisplayUserDtoMapper();
             var userEntity = new UserEntity {UserName = "nowy",IdUser = 1, EMail = "nowy", Role = new RoleEntity {NameRole = "rola"}};
             var expected = new AdminDisplayUserDto {IdUser = 1, EMail = "nowy", RoleName = "rola", UserName = "nowy"};
             //
             var result = mapper.MapEntityToDto(userEntity);
             //
             result.Should().NotBeNull("wynik jest nullem");
             result.ShouldHave().AllProperties().EqualTo(expected);
         }
    }
}
