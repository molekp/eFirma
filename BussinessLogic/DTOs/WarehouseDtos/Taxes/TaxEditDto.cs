using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Taxes
{
    public class TaxEditDto
    {
        public int IdTax { get; set; }

        [Display(Name = "taxName", ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 1)]
        [Required(ErrorMessageResourceName = "taxNameRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string TaxName { get; set; }

        [Display(Name = "taxValue", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "taxValueRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public double TaxValue { get; set; }
        
    }
}
