using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Safety
{
    public class AddSafetyPointGroupMapper
    {
        public AddSafetyPointGroupMapper()
        {
            Mapper.CreateMap<AddSafetyPointGroup, SafetyPointGroup>()
                  .ForMember(desc => desc.GroupName, s => s.MapFrom(src => src.GroupName));

        }

        public SafetyPointGroup MapDtoToEntity(AddSafetyPointGroup a_addSafetyPointGroup)
        {
           var group= Mapper.Map<AddSafetyPointGroup, SafetyPointGroup>(a_addSafetyPointGroup);
            group.SafetyPoints = new List<SafetyPoint>();
            return group;
        }
    }
}
