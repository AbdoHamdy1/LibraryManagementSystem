using LMS.Data;
using LMS.Interface;
using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS.Repository
{
    public class UserDashboardRepository:IUserDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httcontextAccessor;

        public UserDashboardRepository(ApplicationDbContext context, IHttpContextAccessor httcontextAccessor)
        {
            _context = context;
            _httcontextAccessor = httcontextAccessor;
        }

        public async Task<List<BookUser>> GetAllUserBooks()
        {
            var CurrentUserId = _httcontextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var userBooks = await _context.bookUsers
                            .Where(bu => bu.AppUserId == CurrentUserId)
                            .Include(bu => bu.Book).Include(bu=>bu.Book.Author) // Eager loading the Book entity
                            .ToListAsync();
            return  userBooks;
           
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

       
    }
}
