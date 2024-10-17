using LMS.Models;

namespace LMS.Interface
{
    public interface IUserDashboardRepository
    {
        Task<List<BookUser>> GetAllUserBooks();
        Task<AppUser> GetUserById(string id);

        bool Update(AppUser user);

        bool Save();
    }
}
