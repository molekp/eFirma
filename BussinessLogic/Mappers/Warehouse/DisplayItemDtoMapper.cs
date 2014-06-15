using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse
{
    public class DisplayItemDtoMapper
    {
        public DisplayItemDtoMapper()
        {
            Mapper.CreateMap<IItem, DisplayItemDto>()
                  .ForMember(desc => desc.IdItem, s => s.MapFrom(src => src.IdItem))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.ItemType, s => s.MapFrom(src =>
                      {
                          if (src is ProductItem) return ItemTypeEnum.Product;
                          return ItemTypeEnum.Service;
                      }))
                      .ForMember(desc => desc.Price, s => s.MapFrom(src => src.Price))
                      .ForMember(desc => desc.ItemState, s => s.MapFrom(src => src.ItemState))
                      .ForMember(desc => desc.Quantity, s => s.MapFrom(src => src.Quantity))
                      .ForMember(desc => desc.Vin, s => s.MapFrom(src => src.Vin))
                      .ForMember(desc => desc.ExpirationTime, s => s.MapFrom(src => src.ExpirationTime))
                      .ForMember(desc => desc.SaleType, s => s.MapFrom(src => src.SaleType.Name));

        }

        public DisplayItemDto MapEntityToDto(IItem a_item)
        {
            return Mapper.Map<IItem, DisplayItemDto>(a_item);
        }
    }
}
