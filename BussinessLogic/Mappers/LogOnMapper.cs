using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.Helpers;
using Database.Entities;

namespace BussinessLogic.Mappers
{
    public class LogOnMapper
    {
        public LogOnMapper()
        {
            Mapper.CreateMap<LogOnDto, UserEntity>()
                .ForMember(desc => desc.UserName, s => s.MapFrom(src => src.UserName))
                .ForMember(desc => desc.Password, s => s.MapFrom(src => src.Password));

            Mapper.CreateMap<UserEntity, LogOnDto>()
                .ForMember(desc => desc.UserName, s => s.MapFrom(src => src.UserName))
                .ForMember(desc => desc.Password, s => s.MapFrom(src => src.Password));
        }

        public UserEntity MapDtoToEntity(LogOnDto a_userDto)
        {
            UserEntity userEntity = Mapper.Map<LogOnDto, UserEntity>(a_userDto);
            userEntity.Password = Hasher.HashPassword(userEntity.Password);
            return userEntity;
        }

        public LogOnDto MapEntityToDto(UserEntity a_userEntity)
        {
            return Mapper.Map<UserEntity, LogOnDto>(a_userEntity);
        }
    }
}
