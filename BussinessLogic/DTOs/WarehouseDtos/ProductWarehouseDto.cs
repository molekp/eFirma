using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class ProductWarehouseDto
    {
        public int IdProductWarehouse { get; set; }

        [Display(Name = "NameOfSpecyficWarehouse", ResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}