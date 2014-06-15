using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using Database.Entities.Factures;

namespace BussinessLogic.Mappers.Factures
{
    public class FactureViewMapper
    {
        public FactureViewMapper()
        {
            Mapper.CreateMap<Facture, FactureViewDto>()
                    .ForMember(i => i.IdFacture, s => s.MapFrom(src => src.IdFacture))
                    .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                    .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                    .ForMember(i => i.ExpirationTime, s => s.MapFrom(src => src.ExpirationTime))
                    .ForMember(i => i.ProviderName, s => s.MapFrom(src => src.ProviderName))
                    .ForMember(i => i.ClientName, s => s.MapFrom(src => src.ClientName));
        }

        public FactureViewDto MapEntityToDto(Facture a_FactureEntity)
        {
            var mapper = Mapper.Map<Facture, FactureViewDto>(a_FactureEntity);
            mapper.FactureNr = a_FactureEntity.IdFacture.ToString() + "/" + mapper.CreationTime.Year.ToString();
            return mapper;
        }
    }
}
