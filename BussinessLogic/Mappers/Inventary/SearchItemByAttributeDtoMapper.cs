using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Inventary;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using ResourceLibrary;

namespace BussinessLogic.Mappers.Inventary
{
    public class SearchItemByAttributeDtoMapper
    {
        public SearchItemByAttributeDtoMapper()
        {
            Mapper.CreateMap<ProductItem, SearchItemByAttributeDto>()
                  .ForMember(i => i.ItemId, s => s.MapFrom(src => src.IdItem))
                  .ForMember(i => i.ItemName, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.ItemPrice, s => s.MapFrom(src => src.Price))
                  .ForMember(i => i.ItemTypeName, s => s.MapFrom(src => src.ItemType.Name))
                  .ForMember(i => i.ItemState, s => s.MapFrom(src =>
                  {
                      switch (src.ItemState)
                      {
                          case ItemState.Supplied:
                              return Resource.itemStateSupplied;
                          case ItemState.InWarehouse:
                              return Resource.itemStateInWarehouse;
                          case ItemState.PreDistributed:
                              return Resource.itemStatePreDistributed;
                          case ItemState.Distributed:
                              return Resource.itemStateDistributed;
                      }
                      return "";
                  }));

            Mapper.CreateMap<ServiceItem, SearchItemByAttributeDto>()
                  .ForMember(i => i.ItemId, s => s.MapFrom(src => src.IdItem))
                  .ForMember(i => i.ItemName, s => s.MapFrom(src => src.Name))
                  .ForMember(i => i.ItemPrice, s => s.MapFrom(src => src.Price))
                  .ForMember(i => i.ItemTypeName, s => s.MapFrom(src => src.ItemType.Name))
                  .ForMember(i => i.ItemState, s => s.MapFrom(src =>
                  {
                      switch (src.ItemState)
                      {
                          case ItemState.Supplied:
                              return Resource.itemStateSupplied;
                          case ItemState.InWarehouse:
                              return Resource.itemStateInWarehouse;
                          case ItemState.PreDistributed:
                              return Resource.itemStatePreDistributed;
                          case ItemState.Distributed:
                              return Resource.itemStateDistributed;
                      }
                      return "";
                  }));
        }


        public SearchItemByAttributeDto MapEntityToDto(ProductItem a_warehouse, ValueAttributeType a_valueAttributeType)
        {
            var tmp= Mapper.Map<ProductItem, SearchItemByAttributeDto>(a_warehouse);
            tmp.AttributeTypeName = a_valueAttributeType.Name;
            tmp.AttributeValue = a_valueAttributeType.Value;
            return tmp;
        }

        public SearchItemByAttributeDto MapEntityToDto(ServiceItem a_productWarehouse, ValueAttributeType a_valueAttributeType)
        {
            var tmp = Mapper.Map<ServiceItem, SearchItemByAttributeDto>(a_productWarehouse);
            tmp.AttributeTypeName = a_valueAttributeType.Name;
            tmp.AttributeValue = a_valueAttributeType.Value;
            return tmp;
        }
    }
}
