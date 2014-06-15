using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Admin
{
    public class SafetyPointGroupForUserManageMapper
    {
        public SafetyPointGroupForUserManageMapper()
        {
            Mapper.CreateMap<SafetyPointGroup, SafetyPointGroupForUserManage>()
                  .ForMember(desc => desc.NameOfsafetyPointGroup, s => s.MapFrom(src => src.GroupName))
                  .ForMember(desc => desc.IdSafetyPointGroup, s => s.MapFrom(src => src.IdSafetyPointGroup));

        }

        public SafetyPointGroupForUserManage MapEntityToDto(SafetyPointGroup a_safetyPointGroup)
        {
            return Mapper.Map<SafetyPointGroup, SafetyPointGroupForUserManage>(a_safetyPointGroup);
        }
    }
}
