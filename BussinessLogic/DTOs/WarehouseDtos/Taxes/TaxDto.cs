using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Taxes
{
    public class TaxDto
    {
        public int IdTax { get; set; }

        [Display(Name = "taxName", ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 2)]
        public string TaxName { get; set; }

        [Display(Name = "taxValue", ResourceType = typeof(Resource))]
        public double TaxValue { get; set; }
        
    }
}
