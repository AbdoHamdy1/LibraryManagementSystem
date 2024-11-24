using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Author
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
