using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;

namespace BussinessLogic.Mappers.Warehouse
{
    public class DisplayWarehouseMapper
    {
        public DisplayWarehouseMapper()
        {
            Mapper.CreateMap<Database.Entities.WarehouseEntities.Warehouse, DisplayWarehouseDto>()
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.IdWarehouse, s => s.MapFrom(src => src.IdWarehouse))
                  .ForMember(desc => desc.Address, s => s.MapFrom(src => src.Address));

        }

        public DisplayWarehouseDto MapEntityToDto(Database.Entities.WarehouseEntities.Warehouse a_warehouse)
        {
            return Mapper.Map<Database.Entities.WarehouseEntities.Warehouse, DisplayWarehouseDto>(a_warehouse);
        }
    }
}
