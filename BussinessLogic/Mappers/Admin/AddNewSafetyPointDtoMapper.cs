using AutoMapper;
using BussinessLogic.DTOs.Admin;
using Database.Entities.Safety;

namespace BussinessLogic.Mappers.Safety
{
    public class AddNewSafetyPointDtoMapper
    {
        public AddNewSafetyPointDtoMapper()
        {
            Mapper.CreateMap<AddNewSafetyPointDto, SafetyPoint>()
                .ForMember(desc => desc.NameOfsafetyPoint, s => s.MapFrom(src => src.NameOfsafetyPoint))
                .ForMember(desc => desc.Read, s => s.MapFrom(src => src.Read))
                .ForMember(desc => desc.Write, s => s.MapFrom(src => src.Write))
                .ForMember(desc => desc.IdOfPointInTable, s => s.MapFrom(src => src.IdOfPointInTable));

        }

        public SafetyPoint MapDtoToEntity(AddNewSafetyPointDto a_addNewSafetyPointDto,TypeOfSafetyPoint a_typeOfSafetyPoint)
        {
            SafetyPoint safetyPoint = Mapper.Map<AddNewSafetyPointDto, SafetyPoint>(a_addNewSafetyPointDto);
            safetyPoint.TypeOfSafetyPoint = a_typeOfSafetyPoint;//mapper shouldn't search for typeOfSafetyPoint, that is not his job
            
            return safetyPoint;
        }

       
    }
}
