using LMS.Data;
using LMS.Interface;
using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class PublisherRepository:IPublisherRepository
    {
        private readonly ApplicationDbContext _context;

        public PublisherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllPublishersAsync()
        {
            return await _context.publishers.ToListAsync();
        }
    }
}
