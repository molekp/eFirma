using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Account;
using Database.Entities;

namespace BussinessLogic.Mappers.Account
{
    public class EditUserToDtoMapper
    {
        public EditUserToDtoMapper()
        {
            Mapper.CreateMap<UserEntity, EditUserDto>()
                  .ForMember(i => i.Email, s => s.MapFrom(src => src.EMail))
                  .ForMember(i => i.IdUser, s => s.MapFrom(src => src.IdUser));
        }

        public EditUserDto MapEntityToDto(UserEntity a_productItem)
        {
            return Mapper.Map<UserEntity, EditUserDto>(a_productItem);
        }
    }
}
