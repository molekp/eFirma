using System.Collections.Generic;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.Mappers.Admin;
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
    public class DisplaySafetyPointGroupDtoMapperTest
    {
        [Test]
        public void DisplaySafetyPointGroupDto_na_DisplaySafetyPointDto()
        {
            var mapper = new DispalySafetyPointGroupDtoMapper();
            var safetyPoints = new List<SafetyPoint> {new SafetyPoint(), new SafetyPoint()};
            var safetyPointGroup = new SafetyPointGroup()
                {
                        GroupName = "nowa",
                        IdSafetyPointGroup = 1,
                        SafetyPoints = safetyPoints
                };
            var expected = new DisplaySafetyPointGroupDto
                {
                        IdSafetyPointGroup = 1,
                        NameOfsafetyPointGroup = "nowa",
                        NumberOfSafetyPointsInGroup = 2,
                        NumberOfUsersInGroup = 0
                };
            //---
             DisplaySafetyPointGroupDto result = mapper.MapEntityToDto(safetyPointGroup, 0);
            //---

            result.ShouldHave().AllProperties().EqualTo(expected,"nie wszystkie property zostaly przepisane");
        }

        
    }
}