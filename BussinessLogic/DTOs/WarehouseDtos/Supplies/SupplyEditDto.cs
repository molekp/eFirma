using System;
using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Supplies
{
    public class SupplyEditDto
    {
        public int IdSupply { get; set; }

        [Display(Name = "firm", ResourceType = typeof(Resource))]
        public string Firm { get; set; }

        [Display(Name = "realizationTime", ResourceType = typeof(Resource))]
        public DateTime RealizationTime { get; set; }

        [Display(Name = "creationTime", ResourceType = typeof(Resource))]
        public DateTime CreationTime { get; set; }

        [Display(Name = "deliveredTime", ResourceType = typeof(Resource))]
        public DateTime DeliveredTime { get; set; }

        [Display(Name = "state", ResourceType = typeof(Resource))]
        public int State { get; set; }
        
    }
}
