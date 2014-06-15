using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Employee
{
    public class AddEmployeeDto
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
        public string EMail { get; set; }
        
        public int SelectedRole { get; set; }

        [Display(Name = "choicesRoles", ResourceType = typeof(Resource))]
        public IEnumerable<SelectListItem> ChoicesRoles { get; set; }

        [Display(Name = "firstName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }


        [Display(Name = "lastName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "phoneNumber", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Phone { get; set; }


        [Display(Name = "address", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "salary", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public decimal Salary { get; set; }

  
        [Display(Name = "bankAccountNumber", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string BankAccountNumber { get; set; }

    }
}
