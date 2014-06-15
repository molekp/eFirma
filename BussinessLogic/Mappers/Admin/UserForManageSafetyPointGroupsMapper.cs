using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities;

namespace BussinessLogic.Mappers.Admin
{
    public class UserForManageSafetyPointGroupsMapper
    {
        public UserForManageSafetyPointGroupsMapper()
        {
            Mapper.CreateMap<UserEntity, UserForManageSafetyPointGroups>()
                  .ForMember(desc => desc.IdUser, s => s.MapFrom(src => src.IdUser))
                  .ForMember(desc => desc.UserName, s => s.MapFrom(src => src.UserName));

        }

        public UserForManageSafetyPointGroups MapEntityToDto(UserEntity a_userEntity,List<SafetyPointGroupForUserManage> a_currentGroups,List<SelectListItem> a_choiceGroups)
        {
            var dto= Mapper.Map<UserEntity, UserForManageSafetyPointGroups>(a_userEntity);
            dto.UserCurrentSafetyPointGroups = a_currentGroups;
            dto.SafetyPointGroupChoicesToAdd = a_choiceGroups;

            return dto;
        }
    }
}
