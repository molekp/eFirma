using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse.Product
{
    public class ManageProductWarehouseMapper
    {
        public ManageProductWarehouseMapper()
        {
            Mapper.CreateMap<ProductWarehouse, ManageProductWarehouseDto>()
                  .ForMember(desc => desc.IdProductWarehouse, s => s.MapFrom(src => src.IdProductWarehouse))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name));
        }

        public ManageProductWarehouseDto MapEntityToDto(ProductWarehouse a_productWarehouse, IEnumerable<ProductItemForManageProductWarehouseDto> a_products)
        {
            var warehouse =Mapper.Map<ProductWarehouse, ManageProductWarehouseDto>(a_productWarehouse);
            warehouse.ProductItems = a_products;
            return warehouse;
        }
    }
}
