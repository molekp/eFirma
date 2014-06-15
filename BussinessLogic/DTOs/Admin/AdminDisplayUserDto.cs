using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Admin
{
    public class AdminDisplayUserDto
    {
        public int IdUser { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "UserEmail", ResourceType = typeof(Resource))]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Display(Name = "RoleName", ResourceType = typeof(Resource))]
        public string RoleName { get; set; }
    }
}