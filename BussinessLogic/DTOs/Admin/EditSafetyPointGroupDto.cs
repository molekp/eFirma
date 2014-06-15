using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class EditSafetyPointGroupDto
    {
        public int IdSafetyPointGroup { get; set; }

        [Display(Name = "NameOfSafetyPointGroup", ResourceType = typeof(Resource))]
        public string NameOfsafetyPointGroup { get; set; }

        [Display(Name = "NameOfSafetyPointsInSafetyPointGroup", ResourceType = typeof(Resource))]
        public List<DisplaySafetyPointDto> SafetyPoints { get; set; }

         [Display(Name = "AddSafetyPointToSafetyPointGroup", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesToAddSafetyPointToGroup { get; set; } 

        public int IdNewAddSafetyPoint { get; set; }
    }
}
