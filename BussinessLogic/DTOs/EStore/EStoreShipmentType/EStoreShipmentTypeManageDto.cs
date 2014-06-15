using System.Collections.Generic;

namespace BussinessLogic.DTOs.EStore.EStoreShipmentType
{
    public class EStoreShipmentTypeManageDto
    {
        public int IdEStore { get; set; }

        public IEnumerable<EStoreShipmentTypeDto> EStoreShipmentTypeDto { get; set; }
    }
}