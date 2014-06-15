using System.Collections.Generic;
using BussinessLogic.DTOs.WarehouseDtos.Items;

namespace BussinessLogic.DTOs.EStore.EStoreDisplayDto
{
    public class EStoreDisplayItemsDto
    {
        public int IdEStore { get; set; }

        public int TypeOfItem { get; set; }

        public List<DisplayItemDto> Items { get; set; }
    }
}