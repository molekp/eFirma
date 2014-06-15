using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.Mappers.Customers;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Distributions
{
    public class SearchDistributionDtoMapper
    {

        public SearchDistributionDtoMapper()
        {

            Mapper.CreateMap<Distribution, SearchDistributionDto>()
                  .ForMember(desc => desc.IdDistribution, s => s.MapFrom(src => src.IdDistribution))
                  .ForMember(desc => desc.DistributionCreateTime, s => s.MapFrom(src => src.DistributionCreateTime))
                  .ForMember(desc => desc.DistributionCreatorName, s => s.ResolveUsing(src =>
                      {
                          if (src.DistributionCreator == null) return null;
                          return src.DistributionCreator.UserName;
                      }))
                  .ForMember(desc => desc.DistributionTime, s => s.MapFrom(src => src.DistributionTime))
                  .ForMember(desc => desc.State, s => s.MapFrom(src => src.State))
                  .ForMember(desc => desc.CustomerName, s => s.ResolveUsing(src =>
                      {
                          if (src.DistributionCreator == null) return null;
                          return src.DistributionCustomer.Name;
                      }))
                  .ForMember(desc => desc.CustomerAddress, s => s.ResolveUsing(src =>
                      {
                          if (src.DistributionCreator == null) return null;
                          return src.DistributionCustomer.Address;
                      }))
                  .ForMember(desc => desc.ItemsCount, s => s.ResolveUsing(src => src.ProductItems.Count + src.ServiceItems.Count));

        }

        public SearchDistributionDto MapEntityToDto(Distribution a_distribution)
        {
            return Mapper.Map<Distribution, SearchDistributionDto>(a_distribution);
        }
    }
}
