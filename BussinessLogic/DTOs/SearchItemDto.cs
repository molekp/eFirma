using System.ComponentModel.DataAnnotations;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using ResourceLibrary;

namespace BussinessLogic.DTOs
{
    public class SearchItemDto
    {
        public string WarehouseName { get; set; }

        public int IdSpecyficWarehouse { get; set; }

        public string TypeOfSpecyficWarehouse { get; set; }

        public string SpecyficWarehouseName { get; set; }
        
        public int IdItem { get; set; }

        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }

        public string ItemTypeName { get; set; }

        public string ItemState { get; set; }
        
    }
}
