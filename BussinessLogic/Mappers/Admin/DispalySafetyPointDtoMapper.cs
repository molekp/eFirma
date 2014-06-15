using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Safety
{
    public class DispalySafetyPointDtoMapper
    {
        public DispalySafetyPointDtoMapper()
        {
            Mapper.CreateMap<SafetyPoint, DisplaySafetyPointDto>()
                .ForMember(desc => desc.NameOfsafetyPoint, s => s.MapFrom(src => src.NameOfsafetyPoint))
                .ForMember(desc => desc.NameTypeOfSafetyPoint, s => s.MapFrom(src => src.TypeOfSafetyPoint.Name))
                .ForMember(desc => desc.Write, s => s.MapFrom(src => src.Write))
                .ForMember(desc => desc.Read, s => s.MapFrom(src => src.Read))
                .ForMember(desc => desc.IdSafetyPoint, s => s.MapFrom(src => src.IdSafetyPoint));

        }

        public DisplaySafetyPointDto MapEntityToDto(SafetyPoint a_safetyPoint, string a_nameRecordInTable)
        {
            var displaySafetyPoint = Mapper.Map<SafetyPoint, DisplaySafetyPointDto>(a_safetyPoint);
            displaySafetyPoint.NameRecordInTable = a_nameRecordInTable;

            return displaySafetyPoint;
        }
    }
}
