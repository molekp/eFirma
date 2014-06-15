using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DTOs.Store
{
    public class SimpleDisplayStoreDto
    {
        public int IdStore { get; set; }

        public string Name { get; set; }

        public List<SellDto> Sells { get; set; }
    }

    public class SellDto
    {
        public int DistributionId { get; set; }

        public DateTime ExecuteTime { get; set; }

        public DistributionState DistributionState { get; set; }
    }
}
