using System.Collections.Generic;
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
    public class EditSafetyPointGroupDtoMapperTest
    {
        [Test]
        public void EditSafetyPointGroupDtoMapper_na_EditSafetyPointGroupDto_dla_poprawnej_entity__zwraca_obiekt_dto()
        {
            var mapper = new EditSafetyPointGroupDtoMapper();
            var safetyPoints = new List<DisplaySafetyPointDto> { new DisplaySafetyPointDto(), new DisplaySafetyPointDto() };
            var safetyPointGroup = new SafetyPointGroup()
                {
                        GroupName = "nowa",
                        IdSafetyPointGroup = 1,
                        SafetyPoints = new List<SafetyPoint>()
                };
            var expected = new EditSafetyPointGroupDto
                {
                        IdSafetyPointGroup = 1,
                        NameOfsafetyPointGroup = "nowa",
                };
            //---
            EditSafetyPointGroupDto result = mapper.MapEntityToDto(safetyPointGroup, safetyPoints, null);
            //---
            
            result.ShouldHave().AllProperties().But(x=>x.SafetyPoints).EqualTo(expected,"nie wszystkie property zostaly przepisane");
        }

        
    }
}