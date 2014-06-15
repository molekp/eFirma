using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class SupplyViewMapper
    {
        public ProductViewMapper ProductViewMapper { get; set; }
        public SupplyViewMapper()
        {
            Mapper.CreateMap<Supply, SupplyViewDto>()
                    .ForMember(i => i.IdSupply, s => s.MapFrom(src => src.IdSupply))
                    .ForMember(i => i.Firm, s => s.MapFrom(src => src.Firm))
                    .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                    .ForMember(i => i.DeliveredTime, s => s.MapFrom(src => src.DeliveredTime))
                    .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                    .ForMember(i => i.State, s => s.MapFrom(src => src.State));
        }

        public SupplyViewDto MapEntityToDto(Supply a_supplyEntity)
        {

            var supplyDto = Mapper.Map<Supply, SupplyViewDto>(a_supplyEntity);
            supplyDto.Products = new List<ProductViewDto>();
            foreach(var product in a_supplyEntity.ProductItems)
            {
                supplyDto.Products.Add(ProductViewMapper.MapEntityToDto(product));
            }
            return supplyDto;
        }
    }
}
