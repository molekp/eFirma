using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class AddSafetyPointGroup
    {
         [Display(Name = "NameOfSafetyPointGroup", ResourceType = typeof(Resource))]
         [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string GroupName { get; set; }
    }
}
