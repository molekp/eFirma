using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;

namespace BussinessLogic.Mappers.Warehouse
{
    public class EditWarehouseDtoMapper
    {
        public EditWarehouseDtoMapper()
        {
            Mapper.CreateMap<Database.Entities.WarehouseEntities.Warehouse, EditWarehouseDto>()
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.IdWarehouse, s => s.MapFrom(src => src.IdWarehouse))
                  .ForMember(desc => desc.Address, s => s.MapFrom(src => src.Address));

        }

        
        public EditWarehouseDto MapEntityToDto(Database.Entities.WarehouseEntities.Warehouse a_warehouse, List<DisplaySpecyficWarehouse> a_productDtos, List<DisplaySpecyficWarehouse> a_servicesWDtos, List<SelectListItem> a_selectLChoicesProductW, IEnumerable<SelectListItem> a_selectLChoicesServiceW)
        {
            var tmp =  Mapper.Map<Database.Entities.WarehouseEntities.Warehouse, EditWarehouseDto>(a_warehouse);
            tmp.ProductWarehouses = a_productDtos;
            tmp.ServiceWarehouses = a_servicesWDtos;
            tmp.ChoicesProductWarehouses = a_selectLChoicesProductW;
            tmp.ChoicesServicesWarehouses = a_selectLChoicesServiceW;

            return tmp;
        }
    }
}
