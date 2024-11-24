using LMS.Models;

namespace LMS.Interface
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllPublishersAsync();
    }
}
