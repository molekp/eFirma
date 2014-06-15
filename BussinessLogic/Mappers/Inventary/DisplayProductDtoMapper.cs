using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Inventary;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Inventary
{
    public class DisplayProductDtoMapper
    {
        public DisplayProductDtoMapper()
        {
            Mapper.CreateMap<ProductItem, DisplayProductDto>()
                  .ForMember(i => i.IdProduct, s => s.MapFrom(src => src.IdItem))
                  .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.ProductTypeName, s => s.MapFrom(src => src.ItemType.Name))
                  .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                  .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                  .ForMember(i => i.ItemState, s => s.MapFrom(src => src.ItemState))
                  .ForMember(i => i.ExpirationTime, s => s.MapFrom(src => src.ExpirationTime))
                  .ForMember(i => i.TaxName, s => s.MapFrom(src => src.ItemType.ItemTax.TaxName))
                  .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.ItemType.ItemTax.TaxValue))
                  .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleType.Name));
        }

        public DisplayProductDto MapEntityToDto(ProductItem a_productItem,List<DisplayProductAttributeDto> a_attributes)
        {
            var tmp = Mapper.Map<ProductItem, DisplayProductDto>(a_productItem);
            tmp.DisplayProductAttributeDtos = a_attributes;
            return tmp;
        }
    }
}
