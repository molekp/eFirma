using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class EditWarehouseDto
    {
        public int IdWarehouse { get; set; }

        [Display(Name = "NameOfWarehouse", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "AddressOfWarehouse", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Address { get; set; }

        [Display(Name = "ProductWarehouses", ResourceType = typeof(Resource))]
        public IEnumerable<DisplaySpecyficWarehouse> ProductWarehouses { get; set; }

        [Display(Name = "AddProductWarehouse", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesProductWarehouses { get; set; }

        public int ChoiceIdProductWarehouse { get; set; }

        [Display(Name = "CreateProductWarehouseName", ResourceType = typeof(Resource))]
        public string CreateProductWarehouseName { get; set; }

        [Display(Name = "ServiceWarehouses", ResourceType = typeof(Resource))]
        public IEnumerable<DisplaySpecyficWarehouse> ServiceWarehouses { get; set; }

        [Display(Name = "AddServiceWarehouse", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesServicesWarehouses { get; set; }

        public int ChoiceIdServiceWarehouse { get; set; }

        [Display(Name = "CreateServiceWarehouseName", ResourceType = typeof(Resource))]
        public string CreateServiceWarehouseName { get; set; }

    }
}