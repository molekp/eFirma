using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class DisplaySpecyficWarehouse
    {
        public int IdProductWarehouse { get; set; }

        [Display(Name = "NameOfSpecyficWarehouse", ResourceType = typeof(Resource))]
        public string Name {  get; set; }

        [Display(Name = "NumberOfItemsInWarehouse", ResourceType = typeof(Resource))]
        public int HowManyItems { get; set; }
    }
}