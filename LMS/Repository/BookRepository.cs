using LMS.Data;
using LMS.IRepository;
using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Book book)
        {
            _context.Books.Add(book);
            return Save();
        }
        public bool Update(Book book)
        {
            _context.Update(book);
            return Save();
        }
        public bool Delete(Book book)
        {
            _context.Books.Remove(book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).Include(b=>b.Publisher).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByGenre(BookGenre bookgenre)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).Where(b => b.Genre==bookgenre).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefaultAsync(b=>b.Id==id);

        }
        public async Task<Book?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public bool AddBookUser(BookUser bookuser)
        {
            _context.bookUsers.Add(bookuser);
            return Save();
        }
    }
}
