using LMS.Models;

namespace LMS.ViewModel
{
    public class IndexViewModel
    {
        public string usrid {  get; set; }
        public ICollection<Book>Books { get; set; }
    }
}
