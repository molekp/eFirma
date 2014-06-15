using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;

namespace BussinessLogic.Mappers.Warehouse.Product
{
    public class ProductWarehousesMapper
    {
        public ProductWarehousesMapper()
        {
            Mapper.CreateMap<Database.Entities.WarehouseEntities.Product.ProductWarehouse, ProductWarehouseDto>()
                    .ForMember(desc => desc.IdProductWarehouse, s => s.MapFrom(src => src.IdProductWarehouse))
                    .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name));
        }

        public ProductWarehouseDto MapEntityToDto(Database.Entities.WarehouseEntities.Product.ProductWarehouse a_productWarehouse)
        {
            return Mapper.Map<Database.Entities.WarehouseEntities.Product.ProductWarehouse, ProductWarehouseDto>(a_productWarehouse);
        }
    }
}
