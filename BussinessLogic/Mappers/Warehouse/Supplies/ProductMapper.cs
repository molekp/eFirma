using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class ProductMapper
    {
        public ProductMapper()
        {
            Mapper.CreateMap<ProductItem, ProductDto>()
                .ForMember(i => i.IdProduct, s => s.MapFrom(src => src.IdItem))
                .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                .ForMember(i => i.IdProductType, s => s.MapFrom(src => src.ItemType.IdItemType))
                .ForMember(i => i.State, s => s.MapFrom(src => src.ItemState))
                .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleType.Name));

            Mapper.CreateMap<ProductDto, ProductItem>()
                    .ForMember(i => i.IdItem, s => s.MapFrom(src => src.IdProduct))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                    .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                    .ForMember(i => i.Vin, s => s.MapFrom(src => src.Vin));

        }

        public ProductItem MapDtoToEntity(ProductDto a_productDto)
        {
            return Mapper.Map<ProductDto, ProductItem>(a_productDto);
        }

        public ProductDto MapEntityToDto(ProductItem a_productItem)
        {
            return Mapper.Map<ProductItem, ProductDto>(a_productItem);
        }
    }
}
