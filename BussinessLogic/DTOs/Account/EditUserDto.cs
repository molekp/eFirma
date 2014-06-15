using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Account
{
    public class EditUserDto
    {
        [Display(Name = "oldPasswordName", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "passwordRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [Display(Name = "newPasswordName", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "passwordRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword", ResourceType = typeof(Resource))]
        [Compare("NewPassword", ErrorMessageResourceName = "comparePasswords", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "emailAddress", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        public int IdUser { get; set; }
    }

}
