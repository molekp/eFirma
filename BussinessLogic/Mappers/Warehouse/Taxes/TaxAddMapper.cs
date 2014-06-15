using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Taxes
{
    public class TaxAddMapper
    {
        public TaxAddMapper()
        {
            Mapper.CreateMap<TaxAddDto, TaxEntity>()
                .ForMember(i => i.IdTax, s => s.MapFrom(src => src.IdTax))
                .ForMember(i => i.TaxName, s => s.MapFrom(src => src.TaxName))
                .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.TaxValue));
        }

        public TaxEntity MapDtoToEntity(TaxAddDto a_taxAddDto)
        {
            return Mapper.Map<TaxAddDto, TaxEntity>(a_taxAddDto);
        }
    }
}
