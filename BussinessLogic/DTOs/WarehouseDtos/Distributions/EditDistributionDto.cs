using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Entities.WarehouseEntities;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Distributions
{
    public class EditDistributionDto
    {
        [Display(Name = "distributionId", ResourceType = typeof(Resource))]
        public int IdDistribution { get; set; }

        [Display(Name = "distributionTime", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        public DateTime DistributionTime { get; set; }

        [Display(Name = "distributionCreateTime", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        public DateTime DistributionCreateTime { get; set; }

        [Display(Name = "distributionCreatorName", ResourceType = typeof(Resource))]
        public string DistributionCreatorName { get; set; }

        [Display(Name = "distributionState", ResourceType = typeof(Resource))]
        public bool IsPerformed { get; set; }

        [Display(Name = "itemId", ResourceType = typeof(Resource))]
        public IEnumerable<DisplayItemDto> Items { get; set; }

        public int SelectedChoiceCustomer { get; set; }

        public DistributionState SelectedChoiceState { get; set; }


        [Display(Name = "customerName", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesCustomer { get; set; }

    }
}
