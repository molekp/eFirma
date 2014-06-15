using AutoMapper;
using BussinessLogic.DTOs.Account;
using Database.Entities;

namespace BussinessLogic.Mappers.Account
{
    public class DetailsUserToDtoMapper
    {
        public DetailsUserToDtoMapper()
        {
            Mapper.CreateMap<UserEntity, DetailsUserDto>()
                  .ForMember(i => i.Email, s => s.MapFrom(src => src.EMail))
                  .ForMember(i => i.UserName, s => s.MapFrom(src => src.UserName));
        }

        public DetailsUserDto MapEntityToDto(UserEntity a_user)
        {
            return Mapper.Map<UserEntity, DetailsUserDto>(a_user);
        }
    }
}
