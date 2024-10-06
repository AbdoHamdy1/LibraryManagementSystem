using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
