using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities
{
    public class TaxEntity
    {
        [Key]
        public int IdTax { get; set; }

        public string TaxName { get; set; }

        public double TaxValue { get; set; }

    }
}
