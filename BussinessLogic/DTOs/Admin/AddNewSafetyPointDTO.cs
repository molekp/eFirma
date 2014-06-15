using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class AddNewSafetyPointDto
    {
        [Display(Name = "NameOfSafetyPoint", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string NameOfsafetyPoint { get; set; }

        [Display(Name = "SelectTypeOfSafetyPoint", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "SelectErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public int IdTypeOfSafetyPoint { get; set; }

        [Display(Name = "SelectSafetyPointInTable", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "SelectErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public int IdOfPointInTable { get; set; }

        [Display(Name = "SafetyPointRead", ResourceType = typeof(Resource))]
        public bool Read { get; set; }
        [Display(Name = "SafetyPointWrite", ResourceType = typeof(Resource))]
        public bool Write { get; set; }

    }
}
