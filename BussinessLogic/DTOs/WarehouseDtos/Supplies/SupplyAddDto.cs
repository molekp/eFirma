using System;
using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Supplies
{
    public class SupplyAddDto
    {
        [Display(Name = "firm", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "firmRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]   
        public string Firm { get; set; }

        [Display(Name = "realizationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [Required(ErrorMessageResourceName = "realizationTimeRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public DateTime RealizationTime { get; set; }
    }
}