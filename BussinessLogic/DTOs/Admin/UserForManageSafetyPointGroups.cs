using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class UserForManageSafetyPointGroups
    {
        public int IdUser { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "UserCurrentSafetyPointGroups", ResourceType = typeof(Resource))]
        public IEnumerable<SafetyPointGroupForUserManage> UserCurrentSafetyPointGroups { get; set; }

        [Display(Name = "SafetyPointGroupChoicesToAdd", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> SafetyPointGroupChoicesToAdd { get; set; }

        public int IdAddToGroup { get; set; }
    }
}
