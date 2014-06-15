using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Safety
{
    public class EditSafetyPointGroupDtoMapper
    {
        public EditSafetyPointGroupDtoMapper()
        {
            Mapper.CreateMap<SafetyPointGroup, EditSafetyPointGroupDto>()
                  .ForMember(desc => desc.IdSafetyPointGroup, s => s.MapFrom(src => src.IdSafetyPointGroup))
                  .ForMember(desc => desc.NameOfsafetyPointGroup, s => s.MapFrom(src => src.GroupName));

        }

        public EditSafetyPointGroupDto MapEntityToDto(SafetyPointGroup a_safetyPoint, List<DisplaySafetyPointDto> a_displaySafetyPointDtos, List<SelectListItem> a_selectListChoicesToAdd)
        {
            var tmp= Mapper.Map<SafetyPointGroup, EditSafetyPointGroupDto>(a_safetyPoint);
            tmp.SafetyPoints = a_displaySafetyPointDtos;
            tmp.ChoicesToAddSafetyPointToGroup = a_selectListChoicesToAdd;
            return tmp;
        }

    }
}
