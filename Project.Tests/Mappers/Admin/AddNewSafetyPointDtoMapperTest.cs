using BussinessLogic.DTOs.Admin;
using BussinessLogic.Mappers.Safety;
using Database.Entities.Safety;
using NUnit.Framework;

namespace Project.Tests.Mappers.Safety
{
    /// <summary>
    /// Summary description for LogOnDtoMapperTest
    /// </summary>
    [TestFixture]
    public class AddNewSafetyPointDtoMapperTest
    {
        [Test]
        public void AddNewSafetyPointDto_na_UserEntity_Test()
        {
            var mapper = new AddNewSafetyPointDtoMapper();
            var addNewSafetyPointDto = new AddNewSafetyPointDto { NameOfsafetyPoint = "Warehouse1 RW", IdTypeOfSafetyPoint = 1, IdOfPointInTable = 1, Read = true, Write = true };
            var typeOfSafetyPoint = new TypeOfSafetyPoint {IdTypeOfSafetyPoint = 1, Name = "Warehouse"};
            SafetyPoint resultSafetyPoint;
            //---
            resultSafetyPoint = mapper.MapDtoToEntity(addNewSafetyPointDto,typeOfSafetyPoint);
            //---

            Assert.AreEqual("Warehouse", resultSafetyPoint.TypeOfSafetyPoint.Name);
            Assert.AreEqual("Warehouse1 RW", resultSafetyPoint.NameOfsafetyPoint);
            Assert.IsTrue(resultSafetyPoint.Write);
            Assert.IsTrue(resultSafetyPoint.Read);
            Assert.AreEqual(1,resultSafetyPoint.IdOfPointInTable);
        }

        
    }
}