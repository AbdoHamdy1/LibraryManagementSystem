using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address Is required")]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Password Is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
