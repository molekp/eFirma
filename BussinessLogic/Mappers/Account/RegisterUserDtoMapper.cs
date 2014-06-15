using AutoMapper;
using BussinessLogic.DTOs.Account;
using BussinessLogic.Helpers;
using Database.Entities;

namespace BussinessLogic.Mappers.Account
{
    public class RegisterUserDtoMapper
    {
        public RegisterUserDtoMapper()
        {
            Mapper.CreateMap<RegisterUserDto,UserEntity>()
                .ForMember(desc => desc.UserName, s => s.MapFrom(src => src.UserName))
                .ForMember(desc => desc.EMail, s => s.MapFrom(src => src.Email))
                .ForMember(desc => desc.Password, s => s.MapFrom(src =>  Hasher.HashPassword(src.Password)));

        }

        public UserEntity MapDtoToEntity(RegisterUserDto a_userDto, RoleEntity a_role)
        {
            UserEntity userEntity = Mapper.Map<RegisterUserDto, UserEntity>(a_userDto);
            userEntity.Role = a_role;
            return userEntity;
        }

    }
}
