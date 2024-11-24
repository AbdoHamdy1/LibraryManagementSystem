using LMS.Data;
using LMS.Models;

namespace LMS.IRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetBookByGenre(BookGenre bookgenre);
         Task<IEnumerable<Book>> GetBookByAuthor(int id);

        bool Add(Book book);

        bool Update(Book book);
        bool Delete(Book book);
        bool Save();
        Task<List<Book>> GetFilteredBooksAsync(
      string? search,
      int? authorId,
      int? publisherId,
      BookGenre? genre,
      BookLanguage? language,
      decimal? minPrice,
      decimal? maxPrice);
        Task<Book?> GetByIdAsyncNoTracking(int id);
        

    }
}
