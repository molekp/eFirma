using AutoMapper;
using BussinessLogic.DTOs.Inventary;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Inventary
{
    public class ManageProductAttributeDtoMapper
    {
        public ManageProductAttributeDtoMapper()
        {
            Mapper.CreateMap<ValueAttributeType, ManageProductAttributeDto>()
                  .ForMember(i => i.IdAttribute, s => s.MapFrom(src => src.IdAttributeType))
                  .ForMember(i => i.AttributeTypeName, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.AttributeValue, s => s.MapFrom(src => src.Value));
        }

        public ManageProductAttributeDto MapEntityToDto(ValueAttributeType a_valueAttributeType)
        {
            return Mapper.Map<ValueAttributeType, ManageProductAttributeDto>(a_valueAttributeType);
        }
    }
}
