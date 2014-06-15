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
    public class DisplaySafetyPointDtoMapperTest
    {
        [Test]
        public void DisplaySafetyPointDto_na_UserEntity_Test()
        {
            var mapper = new DispalySafetyPointDtoMapper();
            var typeOfSafetyPoint = new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 1, Name = "Warehouse" };
            var safetyPoint = new SafetyPoint {IdSafetyPoint = 1,NameOfsafetyPoint = "Warehouse1 RW", TypeOfSafetyPoint = typeOfSafetyPoint, IdOfPointInTable = 1, Read = true, Write = true };
            var NameRecordInTable = "warehouse1";
            var expected = new DisplaySafetyPointDto
                {
                        IdSafetyPoint = 1,
                        NameOfsafetyPoint = "Warehouse1 RW",
                        NameRecordInTable = NameRecordInTable,
                        NameTypeOfSafetyPoint = typeOfSafetyPoint.Name,
                        Read = true,
                        Write = true
                };
            //---
             DisplaySafetyPointDto result = mapper.MapEntityToDto(safetyPoint, NameRecordInTable);
            //---

            result.ShouldHave().AllProperties().EqualTo(expected,"nie wszystkie property zostaly przepisane");
        }

        
    }
}