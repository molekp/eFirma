using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Inventary;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Inventary
{
    public class ManageProductDtoMapper
    {
        public ManageProductDtoMapper()
        {
            Mapper.CreateMap<ProductItem, ManageProductDto>()
                  .ForMember(i => i.IdProduct, s => s.MapFrom(src => src.IdItem))
                  .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.ProductTypeName, s => s.MapFrom(src => src.ItemType.Name))
                  .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                  .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                  .ForMember(i => i.ItemState, s => s.MapFrom(src => src.ItemState))
                  .ForMember(i => i.ExpirationDate, s => s.MapFrom(src => src.ExpirationTime))
                  .ForMember(i => i.TaxName, s => s.MapFrom(src => src.ItemType.ItemTax.TaxName))
                  .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.ItemType.ItemTax.TaxValue))
                  .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleType.Name));

        }

        public ManageProductDto MapEntityToDto(ProductItem a_productItem, List<ManageProductAttributeDto> a_attributes, List<SelectListItem> a_choicesProductTypes, List<SelectListItem> a_choicesSaleTypes, List<SelectListItem> a_choicesAttributeTypes)
        {
            var tmp = Mapper.Map<ProductItem, ManageProductDto>(a_productItem);
            tmp.ManageProductAttributeDtos = a_attributes;
            tmp.ChoicesProductTypes = a_choicesProductTypes;
            tmp.ChoicesSaleTypes = a_choicesSaleTypes;
            tmp.AddAttributeDto = new AddAttributeDto
                {
                        AttributeTypes = a_choicesAttributeTypes
                };
            return tmp;
        }

      
    }
}
