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
    public class ReturnDto
    {
        public int  DistributionId { get; set; }

        public int ItemId { get; set; }

        public ItemTypeEnum ItemType { get; set; }

        [Display(Name = "choicesWarehouses", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesWarehouses { get; set; }

        public int SelectedWarehouse { get; set; }

        public int StoreId { get; set; }
    }
}
