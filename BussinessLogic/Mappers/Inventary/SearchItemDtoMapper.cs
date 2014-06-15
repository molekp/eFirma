using AutoMapper;
using BussinessLogic.DTOs;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using ResourceLibrary;

namespace BussinessLogic.Mappers.Inventary 
{
    public class SearchItemDtoMapper
    {
        public SearchItemDtoMapper()
        {
            Mapper.CreateMap<ProductItem, SearchItemDto>()
                    .ForMember(i => i.IdItem, s => s.MapFrom(src => src.IdItem))
                    .ForMember(i => i.ItemName, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.ItemTypeName, s => s.MapFrom(src => src.ItemType.Name))
                    .ForMember(i => i.ItemPrice, s => s.MapFrom(src => src.Price))
                    .ForMember(i => i.ItemState, s => s.ResolveUsing(src =>
                                                                    {
                                                                        switch (src.ItemState)
                                                                        {
                                                                            case ItemState.Supplied:
                                                                                return Resource.itemStateSupplied;
                                                                            case ItemState.InWarehouse:
                                                                                return Resource.itemStateInWarehouse;
                                                                            case ItemState.PreDistributed:
                                                                                return Resource.itemStatePreDistributed;
                                                                            case ItemState.Distributed:
                                                                                return Resource.itemStateDistributed;
                                                                        }
                                                                        return "";
                                                                    }));
        }

        public SearchItemDto MapEntityToDto(ProductItem a_productItem, string a_warehouseName, string a_typeOfSpecyficWarehouse, int a_idSpecyficWarehouse, string a_specyficWarehouseName)
        {
            var tmp= Mapper.Map<ProductItem, SearchItemDto>(a_productItem);
            tmp.WarehouseName = a_warehouseName;
            tmp.TypeOfSpecyficWarehouse = a_typeOfSpecyficWarehouse;
            tmp.SpecyficWarehouseName = a_specyficWarehouseName;
            tmp.IdSpecyficWarehouse = a_idSpecyficWarehouse;
            return tmp;
        }
    }
}
