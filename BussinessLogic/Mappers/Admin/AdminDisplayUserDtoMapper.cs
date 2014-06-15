using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Admin
{
    public class AdminDisplayUserDtoMapper
    {
        public AdminDisplayUserDtoMapper()
        {
            Mapper.CreateMap<UserEntity, AdminDisplayUserDto>()
                  .ForMember(desc => desc.IdUser, s => s.MapFrom(src => src.IdUser))
                  .ForMember(desc => desc.UserName, s => s.MapFrom(src => src.UserName))
                  .ForMember(desc => desc.EMail, s => s.MapFrom(src => src.EMail))
                  .ForMember(desc => desc.RoleName, s => s.MapFrom(src =>
                      {
                          if (src.Role == null) return "";
                          return src.Role.NameRole;
                      }));

        }

        public AdminDisplayUserDto MapEntityToDto(UserEntity a_userEntity)
        {
            return Mapper.Map<UserEntity, AdminDisplayUserDto>(a_userEntity);
        }
    }
}
