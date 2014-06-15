using AutoMapper;
using BussinessLogic.DTOs.Inventary;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Inventary
{
    public class DisplayProductAttributeDtoMapper
    {
        public DisplayProductAttributeDtoMapper()
        {
            Mapper.CreateMap<ValueAttributeType, DisplayProductAttributeDto>()
                  .ForMember(i => i.AttributeTypeName, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.AttributeValue, s => s.MapFrom(src => src.Value));
        }

        public DisplayProductAttributeDto MapEntityToDto(ValueAttributeType a_valueAttributeType)
        {
            return Mapper.Map<ValueAttributeType, DisplayProductAttributeDto>(a_valueAttributeType);
        }
    }
}
