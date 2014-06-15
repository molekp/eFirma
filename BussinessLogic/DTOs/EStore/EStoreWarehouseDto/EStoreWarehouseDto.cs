using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.EStore.EStoreWarehouseDto
{
    public class EStoreWarehouseDto
    {
        public int IdWarehouse { get; set; }

        [Display(Name = "NameOfWarehouse", ResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}