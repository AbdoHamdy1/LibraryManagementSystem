using LMS.Models;

namespace LMS.IRepository
{
    public interface IBookRepository
    {
        //CRUD
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetBookByGenre(string city);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        bool Save();
    }
}
