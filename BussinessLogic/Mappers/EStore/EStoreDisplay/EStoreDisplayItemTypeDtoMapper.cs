using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreDisplayDto;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.Mappers.EStore.EStoreDisplay
{
    public class EStoreDisplayItemTypeDtoMapper
    {
        public EStoreDisplayItemTypeDtoMapper()
        {
            Mapper.CreateMap<IItemType, EStoreDisplayItemTypeDto>()
                  .ForMember(desc => desc.IdItemType, s => s.MapFrom(src => src.IdItemType))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.ItemTypeNodes, s => s.MapFrom(src =>
                      {
                          if (src is ProductType)
                          {
                              return this.MapCollectionOfEntityItemTypeToDto(((ProductType)src).ProductTypeNodes);
                          }
                          else
                          {
                              return this.MapCollectionOfEntityItemTypeToDto(((ServiceType)src).ServiceTypeNodes);
                          }
                              
                          
                      }));

        }

        public EStoreDisplayItemTypeDto MapEntityToDto(IItemType a_itemType)
        {
            return Mapper.Map<IItemType, EStoreDisplayItemTypeDto>(a_itemType);
        }

        public IEnumerable<EStoreDisplayItemTypeDto> MapCollectionOfEntityItemTypeToDto(IEnumerable<IItemType> a_itemType)
        {
            var list = new List<EStoreDisplayItemTypeDto>();
            if (a_itemType != null)
            {
                foreach (var item in a_itemType)
                {
                    list.Add(MapEntityToDto(item));
                }
            }

            return list;
        }
    }
}