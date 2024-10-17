namespace LMS.ViewModel
{
    public class BookUserViewModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime DueDate { get; set; }
        public string NationalID { get; set; }
    }
}
