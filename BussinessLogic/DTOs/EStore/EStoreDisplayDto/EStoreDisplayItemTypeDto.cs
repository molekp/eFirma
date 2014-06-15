using System.Collections.Generic;

namespace BussinessLogic.DTOs.EStore.EStoreDisplayDto
{
    public class EStoreDisplayItemTypeDto
    {
        public int IdItemType { get; set; }
        public string Name { get; set; }
        public virtual List<EStoreDisplayItemTypeDto> ItemTypeNodes { get; set; }
    }
}