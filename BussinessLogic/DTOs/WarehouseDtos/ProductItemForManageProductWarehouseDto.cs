using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class ProductItemForManageProductWarehouseDto
    {
        public int IdProduct { get; set; }

        [Display(Name = "itemName", ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "itemQuantity", ResourceType = typeof(Resource))]
        public double Quantity { get; set; }
    }
}