using LMS.Models;

namespace LMS.Interface
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> FindByNameAsync(string name);
        public bool Add(Author author);
        public bool Save();

    }
}
