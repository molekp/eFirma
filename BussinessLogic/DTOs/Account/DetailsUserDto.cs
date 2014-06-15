using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Account
{
    public class DetailsUserDto
    {
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "emailAddress", ResourceType = typeof(Resource))]
        public string Email { get; set; }
    }
}
