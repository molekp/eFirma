using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.Mappers.Customers;
using Database.Entities.Customers;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Distributions
{
    public class DisplayDistributionDtoMapper
    {
        public DisplayDistributionDtoMapper()
        {
            var customerMapper = new DisplayCustomerDtoMapper();
            var itemsMapper = new DisplayItemDtoMapper();

            Mapper.CreateMap<Distribution, DisplayDistributionDto>()
                  .ForMember(desc => desc.IdDistribution, s => s.MapFrom(src => src.IdDistribution))
                  .ForMember(desc => desc.DistributionCreateTime, s => s.MapFrom(src => src.DistributionCreateTime))
                  .ForMember(desc => desc.DistributionCreatorName, s => s.MapFrom(src => src.DistributionCreator.UserName))
                  .ForMember(desc => desc.DistributionTime, s => s.MapFrom(src => src.DistributionTime))
                  .ForMember(desc => desc.State, s => s.MapFrom(src => src.State))
                  .ForMember(desc => desc.Customer, s => s.ResolveUsing(src =>
                      {
                          if (src.DistributionCustomer == null) return null;
                          return customerMapper.MapEntityToDto(src.DistributionCustomer);
                      }))
                  .ForMember(desc => desc.Items, s => s.ResolveUsing(src =>
                      {
                          var list = src.ProductItems.Select(item => itemsMapper.MapEntityToDto(item)).ToList();
                          list.AddRange(src.ServiceItems.Select(item => itemsMapper.MapEntityToDto(item)).ToList());
                          return list;
                      }));

        }

        public DisplayDistributionDto MapEntityToDto(Distribution a_distribution)
        {
            return Mapper.Map<Distribution, DisplayDistributionDto>(a_distribution);
        }
    }
}
