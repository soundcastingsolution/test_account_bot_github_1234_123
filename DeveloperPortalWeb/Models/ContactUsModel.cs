using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Models
{
    public class ContactUsModel
    {

        [Required]
        public string Company { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email Address Failed. Check your Email Address.")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"(\+?\(?\d{0,4}\)?[ -\/]{0,3}\(?\d{2,5}\)?[ -]?)?[\d -]{6,12}\d", ErrorMessage = "The Phone Number field is not valid.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }
        [Required(ErrorMessage = "Captcha field is required")]
        [Display(Name = "Enter the code from the image here")]
        public string Captcha { get; set; }

    }
}