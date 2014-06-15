using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Taxes
{
    public class TaxMapper
    {
        public TaxMapper()
        {
            Mapper.CreateMap<TaxEntity, TaxDto>()
                .ForMember(i => i.IdTax, s => s.MapFrom(src => src.IdTax))
                .ForMember(i => i.TaxName, s => s.MapFrom(src => src.TaxName))
                .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.TaxValue));
        }

        public TaxDto MapEntityToDto(TaxEntity a_taxEntity)
        {
            return Mapper.Map<TaxEntity, TaxDto>(a_taxEntity);
        }
    }
}
