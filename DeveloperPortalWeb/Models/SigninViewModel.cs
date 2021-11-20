using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Models
{
    public class SigninViewModel
    {
        
        [ConditionalValidationAttribute(ErrorMessage = "Application name is required")]
        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }

        
        [ConditionalValidationAttribute(ErrorMessage = "Vendor Name is required")]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }

        
        [ConditionalValidationAttribute(ErrorMessage = "Business Unit is required")]
        [Display(Name = "Business Unit")]
        public int? BusinessUnitNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool isTrue
        { get { return true; } }

        [Required]
        [Display(Name = "I understand that I will deal with real Business Unit data")]
        [System.ComponentModel.DataAnnotations.Compare("isTrue", ErrorMessage = "You must agree that you are responsible for all Business Unit data changes")]
        public bool Consent { get; set; }


        [Display(Name = "Remember Business Unit information")]
        public bool RememberBusinessUnitInfo { get; set; }
    }

    public class ConditionalValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.SigninViewModel)validationContext.ObjectInstance;

            if (value == null)
            {
                if (model.BusinessUnitNumber != null || model.VendorName != null)
                {
                    return new ValidationResult("" + validationContext.DisplayName + " is required");
                }
                else if (model.ApplicationName != null || model.BusinessUnitNumber != null)
                {
                    return new ValidationResult("" + validationContext.DisplayName + " is required");
                }
                else if (model.ApplicationName != null || model.VendorName != null)
                {
                    return new ValidationResult("" + validationContext.DisplayName + " is required");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return ValidationResult.Success;

        }
    }
}








