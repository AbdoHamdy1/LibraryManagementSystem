using LMS.Data;
using LMS.Models;

namespace LMS.ViewModel
{
    public class BookIndexViewModel
    {
        public string appuserid {  get; set; }
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }

        public BookGenre? SelectedGenre { get; set; }
        public BookLanguage? SelectedLanguage { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool isborrowed {  get; set; }
        public Dictionary<int, bool> userbookStatus { get; set; }

    }
}
