using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class SafetyPointGroupForUserManage
    {
        public int IdSafetyPointGroup { get; set; }

        [Display(Name = "NameOfSafetyPointGroup", ResourceType = typeof(Resource))]
        public string NameOfsafetyPointGroup { get; set; }
    }
}
