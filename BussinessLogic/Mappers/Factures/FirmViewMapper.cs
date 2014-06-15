using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using Database.Entities.Customers;

namespace BussinessLogic.Mappers.Factures
{
    public class FirmViewMapper
    {
        public FirmViewMapper()
        {
            Mapper.CreateMap<Customer, FirmViewDto>()
                    .ForMember(i => i.IdFirm, s => s.MapFrom(src => src.IdCustomer))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Address, s => s.MapFrom(src => src.Address))
                    .ForMember(i => i.Info, s => s.MapFrom(src => ((Firm)src).Info))
                    .ForMember(i => i.Phone, s => s.MapFrom(src => ((Firm)src).Phone))
                    ;
        }

        public FirmViewDto MapEntityToDto(Customer a_FirmEntity)
        {
            return Mapper.Map<Customer, FirmViewDto>(a_FirmEntity);
        }
    }
}
