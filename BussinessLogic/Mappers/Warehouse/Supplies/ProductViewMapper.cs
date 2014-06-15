using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class ProductViewMapper
    {
        public ProductViewMapper()
        {
            Mapper.CreateMap<ProductItem, ProductViewDto>()
                    .ForMember(i => i.IdProduct, s => s.MapFrom(src => src.IdItem))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                    .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity));

        }

        public ProductViewDto MapEntityToDto(ProductItem a_productItem)
        {
            return Mapper.Map<ProductItem, ProductViewDto>(a_productItem);
        }
    }
}
