using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.Mappers.Warehouse
{
    public class DisplaySpecyficWarehouseMapper
    {
        public DisplaySpecyficWarehouseMapper()
        {
            Mapper.CreateMap<ProductWarehouse, DisplaySpecyficWarehouse>()
                  .ForMember(desc => desc.IdProductWarehouse, s => s.MapFrom(src => src.IdProductWarehouse))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.HowManyItems, s => s.MapFrom(src =>
                      {
                          if (src.ProductItems != null)
                          {
                              return src.ProductItems.Count;
                          }
                          return 0;
                      }));

            Mapper.CreateMap<ServiceWarehouse, DisplaySpecyficWarehouse>()
                  .ForMember(desc => desc.IdProductWarehouse, s => s.MapFrom(src => src.IdServiceWarehouse))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.HowManyItems, s => s.MapFrom(src =>
                  {
                      if (src.ServiceItems != null)
                      {
                          return src.ServiceItems.Count;
                      }
                      return 0;
                  }));

        }

        public DisplaySpecyficWarehouse MapEntityToDto(ProductWarehouse a_warehouse)
        {
            return Mapper.Map<ProductWarehouse, DisplaySpecyficWarehouse>(a_warehouse);
        }

        public DisplaySpecyficWarehouse MapEntityToDto(ServiceWarehouse a_warehouse)
        {
            return Mapper.Map<ServiceWarehouse, DisplaySpecyficWarehouse>(a_warehouse);
        }
    }
}
