using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.IRepository
{
    public interface IBookUserRepository
    {
        Task<IEnumerable<BookUser>> GetAllBookUsers();
        public bool AddBookUser(BookUser bookuser);

        Task<BookUser?> GetBookUserById(int id);
        bool Update(AppUser appUser);
        bool Return(BookUser bookuser);
        bool Save();
         Task<IEnumerable<Book>> GetBorrowedBooks(string id);
        Task<bool> HasUserBorrowedBookAsync(string userId, int bookId);




    }
}
