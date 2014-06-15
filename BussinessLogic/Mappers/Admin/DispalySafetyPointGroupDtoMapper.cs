using System.Linq;
using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Admin
{
    public class DispalySafetyPointGroupDtoMapper
    {
        public DispalySafetyPointGroupDtoMapper()
        {
            Mapper.CreateMap<SafetyPointGroup, DisplaySafetyPointGroupDto>()
                  .ForMember(desc => desc.NameOfsafetyPointGroup, s => s.MapFrom(src => src.GroupName))
                  .ForMember(desc => desc.IdSafetyPointGroup, s => s.MapFrom(src => src.IdSafetyPointGroup))
                  .ForMember(desc => desc.NumberOfSafetyPointsInGroup, s => s.ResolveUsing(src =>
                      {
                          if (src.SafetyPoints == null) return 0;
                          return src.SafetyPoints.Count();
                      }));

        }

        public DisplaySafetyPointGroupDto MapEntityToDto(SafetyPointGroup a_safetyPointGroup,int a_numberOfUsersInGroup)
        {
            var group =  Mapper.Map<SafetyPointGroup, DisplaySafetyPointGroupDto>(a_safetyPointGroup);
            group.NumberOfUsersInGroup = a_numberOfUsersInGroup;
            return group;
        }
    }
}
