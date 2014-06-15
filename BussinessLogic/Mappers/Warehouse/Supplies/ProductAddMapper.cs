using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class ProductAddMapper
    {
        public ProductAddMapper()
        {
            Mapper.CreateMap<ProductAddDto, ProductItem>()
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                    .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                    .ForMember(i => i.Attributes, s => s.MapFrom(src => new List<ValueAttributeType>()))
                    .ForMember(i => i.ExpirationTime, s => s.MapFrom(src => src.ExpirationTime));
        }

        public ProductItem MapDtoToEntity(ProductAddDto a_productAddDto, IProductTypeRepository a_productTypesRepository, ISaleTypeRepository a_saleTypesRepository)
        {
            var productItem = Mapper.Map<ProductAddDto, ProductItem>(a_productAddDto);
            productItem.ItemType = a_productTypesRepository.GetProductType(a_productAddDto.IdProductType);
            productItem.SaleType = a_saleTypesRepository.Get(a_productAddDto.IdSaleType);
            productItem.Attributes = new List<ValueAttributeType>();
            return productItem;
        }
    }
}
