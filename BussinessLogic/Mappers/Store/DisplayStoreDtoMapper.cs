using AutoMapper;
using BussinessLogic.DTOs.Store;
using Database.Entities.Stores;

namespace BussinessLogic.Mappers.Store
{
    public class DisplayStoreDtoMapper
    {
        public DisplayStoreDtoMapper()
        {
            Mapper.CreateMap<StoreEntity, DisplayStoreDto>()
                  .ForMember(i => i.IdStore, s => s.MapFrom(src => src.IdStore))
                  .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.WarehouseAddress, s => s.MapFrom(src => src.Warehouse.Address))
                  .ForMember(i => i.WarehouseName, s => s.MapFrom(src => src.Warehouse.Name));
        }

        public DisplayStoreDto MapEntityToDto(StoreEntity a_storeEntity)
        {
            return Mapper.Map<StoreEntity, DisplayStoreDto>(a_storeEntity);
        }
    }
}
