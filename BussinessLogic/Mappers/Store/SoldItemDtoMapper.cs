using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Store;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Store
{
    public class SoldItemDtoMapper
    {
        public SoldItemDtoMapper()
        {
            Mapper.CreateMap<IItem, SoldItemDto>()
                  .ForMember(i => i.ItemId, s => s.MapFrom(src => src.IdItem))
                  .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.Price, s => s.MapFrom(src => src.Price))
                  .ForMember(i => i.ItemType, s => s.MapFrom(src =>
                      {
                          if (src is ProductItem)
                          {
                              return ItemTypeEnum.Product;
                          }
                          return ItemTypeEnum.Service;
                      }))
                  .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                  .ForMember(i => i.Vin, s => s.MapFrom(src => src.Vin))
                  .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleType.Name));
        }

        public SoldItemDto MapEntityToDto(IItem a_item, Distribution a_distribution)
        {
            var result= Mapper.Map<IItem, SoldItemDto>(a_item);
            result.DistributionId = a_distribution.IdDistribution;
            result.DistributionTime = a_distribution.DistributionTime;
            if (a_distribution.DistributionCustomer != null)
            {
                result.CustomerName = a_distribution.DistributionCustomer.Name;
            }
            return result;
        }
    }
}
