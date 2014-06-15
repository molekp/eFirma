using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.Factures;

namespace BussinessLogic.Helpers
{
    public class Exchange
    {
        public string name { get; private set; }
        public string code { get; private set; }
        public decimal value { get; private set; }

        public Exchange(ExchangeDto exchangeDto)
        {
            name = exchangeDto.Name;
            code = exchangeDto.Code;
            value = exchangeDto.Exchange;
        }

        public string calc(decimal d)
        {
            return (d * value).ToString("N2");
        }
    }
}
