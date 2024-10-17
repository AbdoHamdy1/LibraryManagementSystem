using LMS.Data;
using LMS.Models;

namespace LMS.ViewModel
{
    public class EditBookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int PublishDate { get; set; }
        public bool Status { get; set; }
        public int Quantity { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public BookGenre bookGenre { get; set; }
        public BookLanguage bookLanguage { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
    }
}
