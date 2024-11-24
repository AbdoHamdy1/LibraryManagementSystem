namespace LMS.ViewModel
{
    public class BookUserViewModel
    {
        public int Id { get; set; }
        public string UserFirst { get; set; }
        public string UserLast { get; set; }
        public string UserImage {  get; set; }
        public string BookName { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime DueDate { get; set; }
        public string NationalID { get; set; }
    }
}
