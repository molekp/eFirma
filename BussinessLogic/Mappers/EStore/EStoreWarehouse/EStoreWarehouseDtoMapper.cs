using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.EStore;
using BussinessLogic.DTOs.EStore.EStoreWarehouseDto;
using BussinessLogic.DTOs.WarehouseDtos;
using Entit = Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.EStore.EStoreWarehouse
{
    public class EStoreWarehouseDtoMapper
    {
        public EStoreWarehouseDtoMapper() // sprawdzic czy to ma sens
        {
            Mapper.CreateMap<Entit.Warehouse, EStoreWarehouseDto>()
                .ForMember(desc => desc.IdWarehouse, s => s.MapFrom(src => src.IdWarehouse))
                .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name));

        }

        public EStoreWarehouseDto MapEntityToDto(Entit.Warehouse a_warehouse)
        {
            return Mapper.Map<Entit.Warehouse, EStoreWarehouseDto>(a_warehouse);
        }

        public IEnumerable<EStoreWarehouseDto> MapCollectionOfEntityToDto(IEnumerable<Entit.Warehouse> a_warehouses)
        {
            var list = new List<EStoreWarehouseDto>();
            if (a_warehouses != null)
            {
                foreach(var item in a_warehouses)
                {
                    list.Add(MapEntityToDto(item));
                }
            }
            
            return list;
        }
    }
}
