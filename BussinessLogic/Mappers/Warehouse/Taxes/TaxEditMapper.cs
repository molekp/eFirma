using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.Mappers.Warehouse.Taxes
{
    public class TaxEditMapper
    {
        public TaxEditMapper()
        {
            Mapper.CreateMap<TaxEntity, TaxEditDto>()
                .ForMember(i => i.IdTax, s => s.MapFrom(src => src.IdTax))
                .ForMember(i => i.TaxName, s => s.MapFrom(src => src.TaxName))
                .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.TaxValue));
            
            Mapper.CreateMap<TaxEditDto, TaxEntity>()
                    .ForMember(i => i.IdTax, s => s.MapFrom(src => src.IdTax))
                    .ForMember(i => i.TaxName, s => s.MapFrom(src => src.TaxName))
                    .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.TaxValue));
        }


        public TaxEntity MapDtoToEntity(TaxEditDto a_taxEditDto)
        {
            return Mapper.Map<TaxEditDto, TaxEntity>(a_taxEditDto);
        }

        public TaxEditDto MapEntityToDto(TaxEntity a_taxEntity)
        {
            return Mapper.Map<TaxEntity, TaxEditDto>(a_taxEntity);
        }
    }
}
