using LMS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Image {  get; set; }
        public int PublishDate {  get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool Status {  get; set; }
        public BookGenre Genre { get; set; }
        public BookLanguage Language { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author {  get; set; }
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<BookUser>? BookUsers { get; set; }

    }
}
