using LMS.Data;
using LMS.Interface;
using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Author author)
        {
            _context.Authors.Add(author);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();

        }


        public async Task<Author> FindByNameAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.name.ToLower() == name.ToLower());
        }
    }
}
