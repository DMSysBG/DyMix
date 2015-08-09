using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DyMix.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Label_LoginName", ResourceType = typeof(Resources.Account))]
        // [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataType(DataType.Password)]
        [Display(Name = "Label_Password", ResourceType = typeof(Resources.Account))]
        public string Password { get; set; }

        [Display(Name = "Label_RememberMe", ResourceType = typeof(Resources.Account))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Label_FirstName", ResourceType = typeof(Resources.Account))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Label_LastName", ResourceType = typeof(Resources.Account))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Label_Email", ResourceType = typeof(Resources.Account))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataType(DataType.Password)]
        [Display(Name = "Label_Password", ResourceType = typeof(Resources.Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Label_ConfirmPassword", ResourceType = typeof(Resources.Account))]
        [Compare("Password", ErrorMessageResourceName = "CompareNewPassword", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string ConfirmPassword { get; set; }
    }

    public class AccountModel
    {
        public int ActionId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LanguageCode { get; set; }
    }
}