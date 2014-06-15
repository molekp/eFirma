using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers.Customers;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Distributions
{
    public class EditDistributionDtoMapper
    {
        public EditDistributionDtoMapper()
        {
            var itemsMapper = new DisplayItemDtoMapper();

            Mapper.CreateMap<Distribution, EditDistributionDto>()
                  .ForMember(desc => desc.IdDistribution, s => s.MapFrom(src => src.IdDistribution))
                  .ForMember(desc => desc.DistributionCreateTime, s => s.MapFrom(src => src.DistributionCreateTime))
                  .ForMember(desc => desc.DistributionCreatorName, s => s.MapFrom(src => src.DistributionCreator.UserName))
                  .ForMember(desc => desc.DistributionTime, s => s.MapFrom(src => src.DistributionTime))
                  .ForMember(desc => desc.IsPerformed, s => s.ResolveUsing(src =>
                      {
                          if (src.State == DistributionState.Performed)
                          {
                              return true;
                          }
                          return false;
                      }))
                  .ForMember(desc => desc.Items, s => s.ResolveUsing(src =>
                      {
                          var list = src.ProductItems.Select(item => itemsMapper.MapEntityToDto(item)).ToList();
                          list.AddRange(src.ServiceItems.Select(item => itemsMapper.MapEntityToDto(item)).ToList());
                          return list;
                      }));

        }

        public EditDistributionDto MapEntityToDto(Distribution a_distribution, IEnumerable<SelectListItem> a_customerChoices)
        {
            var tmp= Mapper.Map<Distribution, EditDistributionDto>(a_distribution);
            tmp.ChoicesCustomer = a_customerChoices;
            return tmp;
        }
    }
}
