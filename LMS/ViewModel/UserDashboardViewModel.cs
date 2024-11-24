using LMS.Models;

namespace LMS.ViewModel
{
    public class UserDashboardViewModel
    {
        public string Id {  get; set; }
        public List<BookUser> Books { get; set; }
        public string Image {  get; set; }
        public string firstname {  get; set; }
        public string lastname { get; set; }

    }
}
