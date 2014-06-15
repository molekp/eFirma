using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class ExchangeDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Exchange { get; set; }
    }
}