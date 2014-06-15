using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs
{
    public class LogOnDto
    {
        public int IdUser { get; set; }

        [Display(Name = "loginName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "loginRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(15, MinimumLength = 2, ErrorMessageResourceName = "loginLengthErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "passwordName", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "passwordRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}