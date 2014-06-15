using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse.Product
{
    public class ProductItemForManageProductWarehouseMapper
    {
        public ProductItemForManageProductWarehouseMapper()
        {
            Mapper.CreateMap<ProductItem, ProductItemForManageProductWarehouseDto>()
                .ForMember(i => i.IdProduct, s => s.MapFrom(src => src.IdItem))
                .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity));
        }

        public ProductItemForManageProductWarehouseDto MapEntityToDto(ProductItem a_productItem)
        {
            return Mapper.Map<ProductItem, ProductItemForManageProductWarehouseDto>(a_productItem);
        }
    }
}
