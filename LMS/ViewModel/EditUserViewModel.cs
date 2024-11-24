using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Address  is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "First Name  is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name  is required")]

        public string LastName { get; set; }
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits")]
        [Display(Name = "Enter NationalID")]
        [Required(ErrorMessage = "NationalID is required")]
        public string NationalID {  get; set; }
    }
}
