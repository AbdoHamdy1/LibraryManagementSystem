using LMS.Data;
using LMS.IRepository;
using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class BookUserRepository : IBookUserRepository
    {

        private readonly ApplicationDbContext _context;

        public BookUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddBookUser(BookUser bookuser)
        {
            _context.bookUsers.Add(bookuser);
            return Save();
        }
        public async Task<IEnumerable<BookUser>> GetAllBookUsers()
        {
            return await _context.bookUsers.Include(bu => bu.Book).Include(bu => bu.AppUser).ToListAsync();
        }
        public async Task<BookUser?> GetBookUserById(int id)
        {
            return await _context.bookUsers.Include(bu => bu.Book).Include(bu => bu.AppUser).FirstOrDefaultAsync(bu => bu.Id == id);
        }
        public bool Return(BookUser bookuser)
        {
            _context.bookUsers.Remove(bookuser);
            return Save();
        }
        public bool Update(AppUser appUser)
        {
            _context.Users.Update(appUser);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Book>> GetBorrowedBooks(string id)
        {
            return await _context.bookUsers.Where(bu => bu.AppUserId == id).Select(bu => bu.Book).ToListAsync();
        }

        public async Task<bool> HasUserBorrowedBookAsync(string userId, int bookId)
        {
            return await _context.bookUsers.AnyAsync(b => b.AppUserId == userId && b.BookId == bookId);
        }


    }
}
