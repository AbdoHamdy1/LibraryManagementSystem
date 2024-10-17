using System.ComponentModel.DataAnnotations;
using LMS.Validation;
namespace LMS.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name  is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name  is required")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits")]
        [Display(Name = "Enter NationalID")]
        [Required(ErrorMessage = "NationalID is required")]
        [DataType(DataType.Password)]
        public string NationalID { get; set; }

        [Display(Name = "Enter Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Cofirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do Not Match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Confirm BirthDate")]
        [Required(ErrorMessage = "Cofirm BirthDate is required")]
        [DataType(DataType.DateTime)]
        [Compare("BirthDate", ErrorMessage = "BirthDate is required ")]
        [MinimumAge(16, ErrorMessage = "You must be at least 16 years old.")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Confirm Address")]
        [Required(ErrorMessage = "Cofirm Address is required")]
        [DataType(DataType.Text)]
        [Compare("Address", ErrorMessage = "Address is required ")]
        public string Address{ get; set; }

    }
}
