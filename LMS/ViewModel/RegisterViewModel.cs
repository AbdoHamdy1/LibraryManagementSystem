using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }


        [Display(Name = "Enter Address")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Confirm Address")]
        [Required(ErrorMessage = "Cofirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
