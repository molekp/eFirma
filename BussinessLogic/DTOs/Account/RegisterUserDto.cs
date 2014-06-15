using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Account
{
    public class RegisterUserDto
    {

        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "passwordLength", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "passwordName", ResourceType = typeof(Resource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword", ResourceType = typeof(Resource))]
        [Compare("Password", ErrorMessageResourceName = "comparePasswords", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "emailAddress", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = "rememberMe", ResourceType = typeof(Resource))]
        public bool RememberMe { get; set; }
    }
}