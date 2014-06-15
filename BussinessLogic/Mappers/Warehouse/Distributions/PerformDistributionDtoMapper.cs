using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers.Customers;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Distributions
{
    public class PerformDistributionDtoMapper
    {
        public PerformDistributionDtoMapper()
        {
            var customerMapper = new DisplayCustomerDtoMapper();
            var itemsMapper = new DisplayItemDtoMapper();

            Mapper.CreateMap<Distribution, PerformDistributionDto>()
                  .ForMember(desc => desc.IdDistribution, s => s.MapFrom(src => src.IdDistribution))
                  .ForMember(desc => desc.DistributionCreateTime, s => s.MapFrom(src => src.DistributionCreateTime))
                  .ForMember(desc => desc.DistributionCreatorName, s => s.MapFrom(src => src.DistributionCreator.UserName))
                  .ForMember(desc => desc.DistributionTime, s => s.MapFrom(src => src.DistributionTime))
                  .ForMember(desc => desc.State, s => s.MapFrom(src => src.State))
                  .ForMember(desc => desc.Customer, s => s.MapFrom(src =>
                      {
                          if (src.DistributionCustomer == null) return null;
                          return customerMapper.MapEntityToDto(src.DistributionCustomer);
                      }))
                  .ForMember(desc => desc.IsPerformed, s => s.MapFrom(src =>
                      {
                          if (src.State == DistributionState.Performed) return true;
                          return false;
                      }))
                  .ForMember(desc => desc.TotalCost, s => s.MapFrom(src =>
                      {
                          var sum = src.ProductItems.Sum(x => x.Price);
                          sum += src.ServiceItems.Sum(x => x.Price);
                          return sum;
                      }))
                  .ForMember(desc => desc.Items, s => s.MapFrom(src =>
                      {
                          var list = src.ProductItems.Select(item => itemsMapper.MapEntityToDto(item)).ToList();
                          list.AddRange(src.ServiceItems.Select(item => itemsMapper.MapEntityToDto(item)).ToList());
                          return list;
                      }));

        }

        public PerformDistributionDto MapEntityToDto(Distribution a_distribution)
        {
            return Mapper.Map<Distribution, PerformDistributionDto>(a_distribution);
        }
    }
}
