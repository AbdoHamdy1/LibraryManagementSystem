using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{  
    public class AppUser: IdentityUser
    {
        [Key]
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image {  get; set; }
        public string Address { get; set; }
        public DateTime BitrthDate { get; set; }
        public bool? Penalty { get; set; }
        public ICollection<BookUser>? BookUsers { get; set; }

    }
}
