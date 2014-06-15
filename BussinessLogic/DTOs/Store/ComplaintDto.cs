using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Store
{
    public class ComplaintDto
    {
        public int DistributionId { get; set; }

        public int ItemId { get; set; }

        public ItemTypeEnum ItemType { get; set; }
        
        public string CustomerName { get; set; }

        [Display(Name = "choiceCustomer", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesCustomer { get; set; }

        public int SelectedCustomer { get; set; }

        public int StoreId { get; set; }


        [Display(Name = "complaintDescription", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string ComplaintDescription { get; set; }
    }
}
