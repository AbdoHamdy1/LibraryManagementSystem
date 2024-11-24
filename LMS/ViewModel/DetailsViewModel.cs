using LMS.Models; 
namespace LMS.ViewModel
{
    public class DetailsViewModel
    {
        public  Book book { get; set; }
        public IEnumerable<Book> AuthorBooks { get; set; }

    }
}
