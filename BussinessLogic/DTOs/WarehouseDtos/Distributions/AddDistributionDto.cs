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
    public class AddDistributionDto
    {
        [Display(Name = "distributionTime", ResourceType = typeof (Resource))]
        [DataType(DataType.Time)]
        public DateTime DistributionTime { get; set; }

        [Display(Name = "distributionDate", ResourceType = typeof (Resource))]
        [DataType(DataType.Date)]
        public DateTime DistributionDate { get; set; }

        [Display(Name = "distributionCustomerAdd", ResourceType = typeof (Resource))]
        public List<SelectListItem> ChoicesCustomer { get; set; }

        [Display(Name = "distributionProviderAdd", ResourceType = typeof(Resource))]
        public List<SelectListItem> ChoicesProvider { get; set; }

        [Display(Name = "distributionItemsAdd", ResourceType = typeof (Resource))]
        public IEnumerable<SimpleItemDto> Items { get; set; }

        public int SelectedCustomer { get; set; }

        public int SelectedProvider { get; set; }

        public SelectedItem[] SelectedItems { get; set; }
    }

    public class SelectedItem
    {
        public int ItemId { get; set; }

        public double ItemQuantity { get; set; }

        public ItemTypeEnum ItemTypeEnum { get; set; }
    }
}
