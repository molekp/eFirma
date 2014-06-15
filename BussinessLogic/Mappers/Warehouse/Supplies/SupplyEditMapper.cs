using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Supplies
{
    public class SupplyEditMapper
    {
        public SupplyEditMapper()
        {
            Mapper.CreateMap<SupplyEditDto, Supply>()
                .ForMember(i => i.IdSupply, s => s.MapFrom(src => src.IdSupply))
                .ForMember(i => i.Firm, s => s.MapFrom(src => src.Firm))
                .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                .ForMember(i => i.DeliveredTime, s => s.MapFrom(src => src.DeliveredTime))
                .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                .ForMember(i => i.State, s => s.MapFrom(src => src.State));
            Mapper.CreateMap<Supply, SupplyEditDto>()
                    .ForMember(i => i.IdSupply, s => s.MapFrom(src => src.IdSupply))
                    .ForMember(i => i.Firm, s => s.MapFrom(src => src.Firm))
                    .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                    .ForMember(i => i.DeliveredTime, s => s.MapFrom(src => src.DeliveredTime))
                    .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                    .ForMember(i => i.State, s => s.MapFrom(src => src.State));
        }

        public Supply MapDtoToEntity(SupplyEditDto a_supplyDto)
        {
            return Mapper.Map<SupplyEditDto, Supply>(a_supplyDto);
        }

        public SupplyEditDto MapEntityToDto(Supply a_supplyEntity)
        {
            return Mapper.Map<Supply, SupplyEditDto>(a_supplyEntity);
        }
    }
}
