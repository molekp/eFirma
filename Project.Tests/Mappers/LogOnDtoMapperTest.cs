using BussinessLogic.DTOs;
using BussinessLogic.Mappers;
using Database.Entities;
using NUnit.Framework;

namespace Project.Tests.Mappers
{
    /// <summary>
    /// Summary description for LogOnDtoMapperTest
    /// </summary>
    [TestFixture]
    public class LogOnDtoMapperTest
    {
        [Test]
        public void LogOnDto_na_UserEntity_Test()
        {
            var mapper = new LogOnMapper();
            var logOnDto = new LogOnDto { UserName = "Stefan", Password = "stefan" };

            UserEntity resultUserEntity;
            //---
            resultUserEntity = mapper.MapDtoToEntity(logOnDto);
            //---
            Assert.AreEqual("Stefan", resultUserEntity.UserName);
            Assert.AreEqual("2E970E822E1A8834203D06ABB60F59EC", resultUserEntity.Password);
        }

        [Test]
        public void UserEntity_na_LogOnDto_Test()
        {
            var mapper = new LogOnMapper();
            var userEntity = new UserEntity {
                UserName = "Stefan",
                Password = "2E970E822E1A8834203D06ABB60F59EC",
                Role = new RoleEntity(),
                IdUser = 1,
                EMail = "asd@vsoft.pl"
            };

            LogOnDto resultLogOnDto;
            //---
            resultLogOnDto = mapper.MapEntityToDto(userEntity);
            //---
            Assert.AreEqual("Stefan", resultLogOnDto.UserName);
            Assert.AreEqual("2E970E822E1A8834203D06ABB60F59EC", resultLogOnDto.Password);
        }
    }
}