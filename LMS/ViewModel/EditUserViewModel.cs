namespace LMS.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string? Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalID {  get; set; }
    }
}
