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
            return await _context.Books.Include(b => b.Author).Include(b=>b.Publisher).Include(b=>b.BookUsers).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByGenre(BookGenre bookgenre)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).Where(b => b.Genre==bookgenre).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByAuthor(int id)
        {
            return await _context.Books.Where(b => b.AuthorId ==id ).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefaultAsync(b=>b.Id==id);

        }
        public async Task<Book?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
       
        public async Task<List<Book>> GetFilteredBooksAsync(
      string? search,
      int? authorId,
      int? publisherId,
      BookGenre? genre,
      BookLanguage? language,
      decimal? minPrice,
      decimal? maxPrice)
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Publisher).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Name.Contains(search) || b.Author.name.Contains(search) || b.Publisher.Name.Contains(search));
            }

            if (authorId.HasValue)
            {
                books = books.Where(b => b.AuthorId == authorId.Value);
            }

            if (publisherId.HasValue)
            {
                books = books.Where(b => b.PublisherId == publisherId.Value);
            }

            if (genre.HasValue)
            {
                books = books.Where(b => b.Genre == genre.Value);
            }

            if (language.HasValue)
            {
                books = books.Where(b => b.Language == language.Value);
            }



            if (minPrice.HasValue && maxPrice.HasValue)
            {
                books = books.Where(b => b.Price >= minPrice.Value && b.Price <= maxPrice.Value);
            }

            return await books.ToListAsync();
        }
       

    }
}
