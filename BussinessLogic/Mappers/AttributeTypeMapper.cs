using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.Helpers;
using Database.Entities;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers
{
    public class AttributeTypeMapper
    {
        public AttributeTypeMapper()
        {
            Mapper.CreateMap<IAttributeType, AttributeTypeDto>()
                    .ForMember(i => i.IdAttributeType, s => s.MapFrom(src => src.IdAttributeType))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name));
           
            Mapper.CreateMap<AttributeTypeDto, IAttributeType>()
                               .ForMember(i => i.IdAttributeType, s => s.MapFrom(src => src.IdAttributeType))
                               .ForMember(i => i.Name, s => s.MapFrom(src => src.Name));

        }
        public AttributeTypeDto MapEntityToDto(IAttributeType a_attributeTypeEntity)
        {
            return Mapper.Map<IAttributeType, AttributeTypeDto>(a_attributeTypeEntity);
        }

        public IAttributeType MapDtoToEntity(AttributeTypeDto a_attributeTypeDto)
        {
            return Mapper.Map<AttributeTypeDto, IAttributeType>(a_attributeTypeDto);
        }
    }
}
