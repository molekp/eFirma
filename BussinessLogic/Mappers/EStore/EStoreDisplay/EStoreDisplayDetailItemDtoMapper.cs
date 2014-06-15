using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreDisplayDto;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.EStore.EStoreDisplay
{
    public class EStoreDisplayDetailItemDtoMapper
    {
        public EStoreDisplayDetailItemDtoMapper()
        {
            Mapper.CreateMap<IItem, EStoreDisplayDetailItemDto>()
                .ForMember(i => i.IdItem, s => s.MapFrom(src => src.IdItem))
                .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                .ForMember(i => i.Price, s => s.MapFrom(src => src.Price));
        }

        public EStoreDisplayDetailItemDto MapEntityToDto(IItem a_productItem)
        {
            return Mapper.Map<IItem, EStoreDisplayDetailItemDto>(a_productItem);
        }         
    }
}