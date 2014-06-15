using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.Helpers;
using Database.Entities;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers
{
    public class AttributeMapper
    {
        public AttributeTypeMapper AttributeTypeMapper { get; set; }

        public AttributeMapper()
        {

            Mapper.CreateMap<ValueAttributeType, AttributeDto>()
                    .ForMember(i => i.Value, s => s.MapFrom(src => src.Value))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.IdAttributeType, s => s.MapFrom(src => src.IdAttributeType));
           
            Mapper.CreateMap<AttributeDto, ValueAttributeType>()
                    .ForMember(i => i.Value, s => s.MapFrom(src => src.Value))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.IdAttributeType, s => s.MapFrom(src => src.IdAttributeType));

        }

        public AttributeDto MapEntityToDto(ValueAttributeType a_valueAttributeTypeEntity)
        {
            return Mapper.Map<ValueAttributeType, AttributeDto>(a_valueAttributeTypeEntity);
        }

        public ValueAttributeType MapDtoToEntity(AttributeDto a_attributeDto)
        {
            return Mapper.Map<AttributeDto, ValueAttributeType>(a_attributeDto);
        }

        public List<AttributeDto> MapEntitiesToDtos(IEnumerable<ValueAttributeType> a_attributeEntities)
        {
            var list = new List<AttributeDto>();
            if(a_attributeEntities == null)
            {
                return list;
            }
            foreach (var attributeEntity in a_attributeEntities)
            {
                list.Add(MapEntityToDto(attributeEntity));
            }
            return list;
        }
    }
}
