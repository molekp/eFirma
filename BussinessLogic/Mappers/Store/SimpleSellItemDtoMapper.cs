using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Store;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.Mappers.Store
{
    public class SimpleSellItemDtoMapper
    {
        public SimpleSellItemDtoMapper()
        {
            Mapper.CreateMap<IItem, SimpleSellItemDto>()
                  .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                  .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                  .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleType.Name))
                  .ForMember(i => i.TotalPrice, s => s.ResolveUsing(src =>
                      {
                          decimal price = src.Price * (decimal) src.Quantity;
                          if (src.Discount != null) price *= (decimal) src.Discount;
                          if (src is ProductItem)
                          {
                              return price + price * (decimal) ((ProductItem) src).ItemType.ItemTax.TaxValue;
                          }
                          if (src is ServiceItem)
                          {
                              return price + price * (decimal)((ServiceItem)src).ItemType.ItemTax.TaxValue;
                          }
                          throw new Exception("Couldn't count product price.");
                      }));
        }

        public SimpleSellItemDto MapEntityToDto(IItem a_item)
        {
            return Mapper.Map<IItem, SimpleSellItemDto>(a_item);
        }
    }
}
