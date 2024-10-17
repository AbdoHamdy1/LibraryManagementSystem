using LMS.Models;

namespace LMS.IRepository
{
    public interface IBookUserRepository
    {
        Task<IEnumerable<BookUser>> GetAllBookUsers();
        Task<BookUser?> GetBookUserById(int id);
        bool Update(AppUser appUser);
        bool Return(BookUser bookuser);
        bool Save();
    }
}
