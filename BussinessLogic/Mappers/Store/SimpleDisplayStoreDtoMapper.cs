using AutoMapper;
using BussinessLogic.DTOs.Store;
using Database.Entities.Stores;
using System.Linq;

namespace BussinessLogic.Mappers.Store
{
    public class SimpleDisplayStoreDtoMapper
    {
        public SimpleDisplayStoreDtoMapper()
        {
            Mapper.CreateMap<StoreEntity, SimpleDisplayStoreDto>()
                  .ForMember(i => i.IdStore, s => s.MapFrom(src => src.IdStore))
                  .ForMember(i => i.Sells, s => s.MapFrom(src => src.Distributions.Select(x=> new SellDto
                      {
                              DistributionId = x.IdDistribution,
                              DistributionState = x.State,
                              ExecuteTime = x.DistributionTime
                      })))
                  .ForMember(i => i.Name, s => s.MapFrom(src => src.Name));
        }

        public SimpleDisplayStoreDto MapEntityToDto(StoreEntity a_storeEntity)
        {
            return Mapper.Map<StoreEntity, SimpleDisplayStoreDto>(a_storeEntity);
        }
    }
}
