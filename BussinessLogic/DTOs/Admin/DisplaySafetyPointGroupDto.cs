using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class DisplaySafetyPointGroupDto
    {
        public int IdSafetyPointGroup { get; set; }

        [Display(Name = "NameOfSafetyPointGroup", ResourceType = typeof(Resource))]
        public string NameOfsafetyPointGroup { get; set; }

        [Display(Name = "NumberOfSafetyPointsInGroup", ResourceType = typeof(Resource))]
        public int NumberOfSafetyPointsInGroup { get; set; }

        [Display(Name = "NumberOfUsersInGroup", ResourceType = typeof(Resource))]
        public int NumberOfUsersInGroup { get; set; }

    }
}
