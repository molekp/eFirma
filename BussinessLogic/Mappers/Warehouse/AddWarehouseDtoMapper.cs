using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.Mappers.Warehouse
{
    public class AddWarehouseDtoMapper
    {
        public AddWarehouseDtoMapper()
        {
            Mapper.CreateMap<AddWarehouseDto, Database.Entities.WarehouseEntities.Warehouse>()
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.Address, s => s.MapFrom(src => src.Address));

        }

        public Database.Entities.WarehouseEntities.Warehouse MapDtoToEntity(AddWarehouseDto a_addSafetyPointGroup)
        {
            var tmp = Mapper.Map<AddWarehouseDto, Database.Entities.WarehouseEntities.Warehouse>(a_addSafetyPointGroup);
            tmp.ProductWarehouses = new List<ProductWarehouse>();
            tmp.ServiceWarehouses = new List<ServiceWarehouse>();
            return tmp;
        }
    }
}
