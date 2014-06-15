using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class SupplyMapper
    {
        public SupplyMapper()
        {
            Mapper.CreateMap<Supply, SupplyDto>()
                .ForMember(i => i.IdSupply, s => s.MapFrom(src => src.IdSupply))
                .ForMember(i => i.Firm, s => s.MapFrom(src => src.Firm))
                .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                .ForMember(i => i.DeliveredTime, s => s.MapFrom(src => src.DeliveredTime))
                .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                .ForMember(i => i.State, s => s.MapFrom(src => src.State));
        }

        public SupplyDto MapEntityToDto(Supply a_supplyEntity)
        {
            return Mapper.Map<Supply, SupplyDto>(a_supplyEntity);
        }
    }
}
