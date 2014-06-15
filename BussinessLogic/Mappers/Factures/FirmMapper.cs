using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using Database.Entities.Customers;

namespace BussinessLogic.Mappers.Factures
{
    public class FirmMapper
    {
        public FirmMapper()
        {
            Mapper.CreateMap<Customer, FirmDto>()
                    .ForMember(i => i.IdFirm, s => s.MapFrom(src => src.IdCustomer))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Address, s => s.MapFrom(src => src.Address))
                    .ForMember(i => i.Info, s => s.MapFrom(src =>  ((Firm)src).Info))
                    .ForMember(i => i.Phone, s => s.MapFrom(src => ((Firm)src).Phone))
                    .ForMember(i => i.NIP, s => s.MapFrom(src => ((Firm)src).NIP))
                    .ForMember(i => i.BankAccountNumber, s => s.MapFrom(src => ((Firm)src).BankAccountNumber))
                    .ForMember(i => i.BankInfo, s => s.MapFrom(src => ((Firm)src).BankInfo))
                    ;
        }

        public FirmDto MapEntityToDto(Customer a_FirmEntity)
        {
            return Mapper.Map<Customer, FirmDto>(a_FirmEntity);
        }   
    }
}
