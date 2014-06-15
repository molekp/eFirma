using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class WarehouseDto
    {
        public int IdWarehouse { get; set; }

        [Display(Name = "NameOfWarehouse", ResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}