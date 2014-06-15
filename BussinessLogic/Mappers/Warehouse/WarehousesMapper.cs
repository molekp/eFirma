using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse
{
    public class WarehousesMapper
    {
        public WarehousesMapper()
        {
            Mapper.CreateMap<Database.Entities.WarehouseEntities.Warehouse, WarehouseDto>()
                    .ForMember(desc => desc.IdWarehouse, s => s.MapFrom(src => src.IdWarehouse))
                    .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name));
        }

        public WarehouseDto MapEntityToDto(Database.Entities.WarehouseEntities.Warehouse a_warehouse)
        {
            return Mapper.Map<Database.Entities.WarehouseEntities.Warehouse, WarehouseDto>(a_warehouse);
        }

        public List<WarehouseDto> MapEntitiesToWarehousesWithProductDtos(IEnumerable<Database.Entities.WarehouseEntities.Warehouse> a_warehouses)
        {
            var list = new List<WarehouseDto>();
            if(a_warehouses == null)
            {
                return list;
            }
            foreach (var warehouse in a_warehouses)
            {
                if(warehouse.ProductWarehouses.Count > 0)
                {
                    list.Add(MapEntityToDto(warehouse));
                }                
            }
            return list;
        }
    }
}
