using LMS.Data;
using LMS.Models;

namespace LMS.ViewModel
{
    public class CreateBookViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public int PublishDate { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; } = true;
        public int Quantity { get; set; }
        public BookGenre Genre { get; set; }
        public BookLanguage Language { get; set; }

        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
    }
}
