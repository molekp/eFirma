using AutoMapper;
using BussinessLogic.DTOs.Customers;
using Database.Entities;
using Database.Entities.Customers;

namespace BussinessLogic.Mappers.Customers
{
    class DisplayCustomerDtoMapper
    {
        public DisplayCustomerDtoMapper()
        {
            Mapper.CreateMap<Customer, DisplayCustomerDto>()
                  .ForMember(desc => desc.IdCustomer, s => s.MapFrom(src => src.IdCustomer))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.IsFirm, s => s.MapFrom(src => src is Firm))
                  .ForMember(desc => desc.Address, s => s.MapFrom(src => src.Address));

        }

        public DisplayCustomerDto MapEntityToDto(Customer a_simpleCustomer)
        {
            return Mapper.Map<Customer, DisplayCustomerDto>(a_simpleCustomer);
        }
    }
}
