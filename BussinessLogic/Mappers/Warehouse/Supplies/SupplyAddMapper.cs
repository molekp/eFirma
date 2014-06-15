using System;
using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class SupplyAddMapper
    {
        public SupplyAddMapper()
        {
            Mapper.CreateMap<SupplyAddDto, Supply>()
                .ForMember(i => i.Firm, s => s.MapFrom(src => src.Firm))
                .ForMember(i => i.State, s => s.MapFrom(src => 1))
                .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime));
        }

        public Supply MapDtoToEntity(SupplyAddDto a_supplyAddDto)
        {
            var supply = Mapper.Map<SupplyAddDto, Supply>(a_supplyAddDto);
            if (supply != null)
            {
                supply.ProductItems = new List<ProductItem>();
            }
            return supply;
        }
    }
}
