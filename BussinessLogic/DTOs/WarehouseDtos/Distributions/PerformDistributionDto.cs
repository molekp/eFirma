using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Distributions
{
    public class PerformDistributionDto
    {
        public bool IsPerformed { get; set; }

        [Display(Name = "distributionId", ResourceType = typeof(Resource))]
        public int IdDistribution { get; set; }

        [Display(Name = "distributionTime", ResourceType = typeof(Resource))]
        public DateTime DistributionTime { get; set; }

        [Display(Name = "distributionCreateTime", ResourceType = typeof(Resource))]
        public DateTime DistributionCreateTime { get; set; }

        [Display(Name = "distributionCreatorName", ResourceType = typeof(Resource))]
        public string DistributionCreatorName { get; set; }

        [Display(Name = "distributionState", ResourceType = typeof(Resource))]
        public int State { get; set; }

        public DisplayCustomerDto Customer { get; set; }

        [Display(Name = "itemId", ResourceType = typeof(Resource))]
        public IEnumerable<DisplayItemDto> Items { get; set; }

         [Display(Name = "distributionTotalCost", ResourceType = typeof(Resource))]
        public decimal TotalCost { get; set; }
    }
}
