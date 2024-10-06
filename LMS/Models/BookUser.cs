using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class BookUser
    {
        [Key]
        public int Id { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
