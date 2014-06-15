using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using Database.Entities.Customers;
using Database.Entities.WarehouseEntities.Currencies;

namespace BussinessLogic.Mappers.Factures
{
    public class ExchangeMapper
    {
        public ExchangeMapper()
        {
            Mapper.CreateMap<Currency, ExchangeDto>()
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Code, s => s.MapFrom(src => src.Code))
                    ;

            Mapper.CreateMap<CurrencyExchange, ExchangeDto>()
                    .ForMember(i => i.Exchange, s => s.MapFrom(src => src.Exchange))
                    ;
        }

        public ExchangeDto MapEntitiesToDto(Currency a_currency, CurrencyExchange a_currencyExchange)
        {
            var a_currencyDto = Mapper.Map<Currency, ExchangeDto>(a_currency);
            var a_currencyExchangeDto = Mapper.Map<CurrencyExchange, ExchangeDto>(a_currencyExchange);
            return new ExchangeDto(){Code = a_currencyDto.Code, Name = a_currencyDto.Name, Exchange = a_currencyExchangeDto.Exchange}; 
        }   
    }
}
