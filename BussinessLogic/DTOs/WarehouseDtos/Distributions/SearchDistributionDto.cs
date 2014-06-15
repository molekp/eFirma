using System;
using System.Collections.Generic;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos.Items;

namespace BussinessLogic.DTOs.WarehouseDtos.Distributions
{
    public class SearchDistributionDto
    {
        public int IdDistribution { get; set; }

        public int ItemsCount { get; set; }

        public DateTime DistributionTime { get; set; }

        public DateTime DistributionCreateTime { get; set; }

        public string DistributionCreatorName { get; set; }

        public int State { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }
    }
}
