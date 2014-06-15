using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities
{
    public class SaleType
    {
        [Key]
        public int IdSaleType { get; set; }

        public string Name { get; set; }

    }
}