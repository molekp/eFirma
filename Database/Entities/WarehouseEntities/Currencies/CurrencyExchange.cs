using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities.Currencies
{
    public class CurrencyExchange
    {
        [Key]
        public int CurrencyExchangeId { get; set; }

        public DateTime ExchangeCourseDate { get; set; }

        public virtual Currency Currency { get; set; }

        public decimal Exchange { get; set; }
    }
}
