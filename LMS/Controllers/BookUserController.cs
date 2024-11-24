using LMS.IRepository;
using LMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class BookUserController : Controller
    {
        private readonly IBookUserRepository _bookUserRepository;

        public BookUserController(IBookUserRepository bookUserRepository)
        {
            _bookUserRepository = bookUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            var bookusers = await _bookUserRepository.GetAllBookUsers();
            List<BookUserViewModel> BookUserVM = new List<BookUserViewModel>();
            foreach(var bookuser in bookusers)
            {
                BookUserVM.Add(new BookUserViewModel()
                {
                    Id=bookuser.Id,
                    UserFirst=bookuser.AppUser.FirstName,
                    UserLast=bookuser.AppUser.LastName,
                    UserImage = bookuser.AppUser.Image,
                    BookName = bookuser.Book.Name,
                    ReserveDate=bookuser.ReserveDate,
                    DueDate=bookuser.DueDate,
                    NationalID=bookuser.AppUser.NationalID,         
                });
            }
            
            return View(BookUserVM);
        }
        [HttpPost]
        public async Task<IActionResult> ReturnAsync(int id)
        {
            var bookuser = await _bookUserRepository.GetBookUserById(id);
            bookuser.Book.Quantity++;
            if (bookuser.ReturnDate <= DateTime.Now)
            {
                bookuser.AppUser.Penalty = true;
                _bookUserRepository.Update(bookuser.AppUser);
            }
            _bookUserRepository.Return(bookuser);
            TempData["success"] = "Book Returned";
            return RedirectToAction("Index");
        }
    }
}
