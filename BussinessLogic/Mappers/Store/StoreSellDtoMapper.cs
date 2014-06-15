using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Store;
using Database.Entities.Stores;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Store
{
    public class StoreSellDtoMapper
    {
        public StoreSellDtoMapper()
        {
            Mapper.CreateMap<StoreEntity, StoreSellDto>()
                  .ForMember(i => i.StoreId, s => s.MapFrom(src => src.IdStore));
        }

        public StoreSellDto MapEntityToDto(StoreEntity a_storeEntity, int a_distributionId, IEnumerable<IItem> a_items)
        {
            var mapper = new SimpleSellItemDtoMapper();
            var tmp= Mapper.Map<StoreEntity, StoreSellDto>(a_storeEntity);
            tmp.DistributionId = a_distributionId;
            var enumerable = a_items as IList<IItem> ?? a_items.ToList();
            tmp.Items = enumerable.Select(x => mapper.MapEntityToDto(x));
            tmp.TotalPrice = enumerable.Select(x=>mapper.MapEntityToDto(x)).Sum(x=>x.TotalPrice);
            return tmp;
        }
    }
}
