using LMS.IRepository;
using LMS.Models;

namespace LMS.Repository
{
    public class BookRepository : IBookRepository
    {
        public bool Add(Book book)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBookByGenre(string city)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
