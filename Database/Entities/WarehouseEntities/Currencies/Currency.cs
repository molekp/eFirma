using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities.Currencies
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
