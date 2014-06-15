using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class DisplaySafetyPointDto
    {
        [Display(Name = "NameOfSafetyPoint", ResourceType = typeof(Resource))]
        public string NameOfsafetyPoint { get; set; }

         [Display(Name = "NameTypeOfSafetyPoint", ResourceType = typeof(Resource))]
        public string NameTypeOfSafetyPoint { get; set; }

         [Display(Name = "NameRecordInTable", ResourceType = typeof(Resource))]
        public string NameRecordInTable { get; set; }

        [Display(Name = "SafetyPointRead", ResourceType = typeof(Resource))]
        public bool Read { get; set; }
        [Display(Name = "SafetyPointWrite", ResourceType = typeof(Resource))]
        public bool Write { get; set; }

        public int IdSafetyPoint { get; set; }
    }
}
