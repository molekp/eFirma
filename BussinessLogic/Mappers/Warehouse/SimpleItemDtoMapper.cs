using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse
{
    public class SimpleItemDtoMapper
    {
        public IWarehouseRepository WarehouseRepository { get; set; }

        public SimpleItemDtoMapper()
        {
            Mapper.CreateMap<IItem, SimpleItemDto>()
                  .ForMember(desc => desc.IdItem, s => s.MapFrom(src => src.IdItem))
                  .ForMember(desc => desc.ItemType, s => s.ResolveUsing(src =>
                  {
                      if (src is ProductItem) return ItemTypeEnum.Product;
                      return ItemTypeEnum.Service;
                  }))
                   .ForMember(desc => desc.Price, s => s.MapFrom(src => src.Price))
                   .ForMember(desc => desc.Vin, s => s.MapFrom(src => src.Vin))
                   .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                      .ForMember(desc => desc.Quantity, s => s.MapFrom(src => src.Quantity))
                      .ForMember(desc => desc.SaleType, s => s.MapFrom(src => src.SaleType.Name));

        }

        public SimpleItemDto MapEntityToDto(IItem a_item, Database.Entities.WarehouseEntities.Warehouse a_warehouse,string a_itemWarehouseName )
        {
            var tmp= Mapper.Map<IItem, SimpleItemDto>(a_item);
            tmp.WarehouseName = a_warehouse.Name;
            tmp.WarehouseAddress = a_warehouse.Address;
            tmp.ItemWarehouseName = a_itemWarehouseName;
            return tmp;
        }
    }
}
