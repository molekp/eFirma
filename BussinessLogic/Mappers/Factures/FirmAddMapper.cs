using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using Database.Entities.Customers;

namespace BussinessLogic.Mappers.Factures
{
    public class FirmAddMapper
    {
        public FirmAddMapper()
        {
            Mapper.CreateMap<FirmAddDto, Firm>()
                    .ForMember(i => i.IdCustomer, s => s.MapFrom(src => src.IdFirm))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Address, s => s.MapFrom(src => src.Address))
                    .ForMember(i => i.Info, s => s.MapFrom(src => src.Info))
                    .ForMember(i => i.Phone, s => s.MapFrom(src => src.Phone))
                    .ForMember(i => i.NIP, s => s.MapFrom(src => src.NIP))
                    .ForMember(i => i.BankAccountNumber, s => s.MapFrom(src => src.BankAccountNumber))
                    .ForMember(i => i.BankInfo, s => s.MapFrom(src => src.BankInfo))
                    ;

            Mapper.CreateMap<Firm, FirmAddDto>()
                    .ForMember(i => i.IdFirm, s => s.MapFrom(src => src.IdCustomer))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Address, s => s.MapFrom(src => src.Address))
                    .ForMember(i => i.Info, s => s.MapFrom(src => src.Info))
                    .ForMember(i => i.Phone, s => s.MapFrom(src => src.Phone))
                    .ForMember(i => i.NIP, s => s.MapFrom(src => src.NIP))
                    .ForMember(i => i.BankAccountNumber, s => s.MapFrom(src => src.BankAccountNumber))
                    .ForMember(i => i.BankInfo, s => s.MapFrom(src => src.BankInfo))
                    ;
        }

        public Firm MapDtoToEntity(FirmAddDto a_FirmDto)
        {
            return Mapper.Map<FirmAddDto, Firm>(a_FirmDto);
        }

        public FirmAddDto MapEntityToDto(Firm a_FirmEntity)
        {
            return Mapper.Map<Firm, FirmAddDto>(a_FirmEntity);
        }   
    }
}
