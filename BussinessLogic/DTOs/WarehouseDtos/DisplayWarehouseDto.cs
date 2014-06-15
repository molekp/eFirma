using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class DisplayWarehouseDto
    {
        public int IdWarehouse { get; set; }

        [Display(Name = "NameOfWarehouse", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "AddressOfWarehouse", ResourceType = typeof(Resource))]
        public string Address { get; set; }
    }
}