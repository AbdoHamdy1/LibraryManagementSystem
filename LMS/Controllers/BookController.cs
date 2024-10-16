using LMS.Interface;
using LMS.IRepository;
using LMS.Models;
using LMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IBookRepository _bookRepository;

        public BookController(IPhotoService photoService, IBookRepository bookRepository)
        {
            _photoService = photoService;
            _bookRepository = bookRepository;
        }

        public async  Task<IActionResult> Index()
        {
            var Books = await _bookRepository.GetAllAsync();
            return View(Books);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Book? book = await _bookRepository.GetByIdAsync(id);
            return View(book);
        }
        public async Task<IActionResult>Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel BookVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(BookVM.Image);
                Book book = new Book
                {
                    Author = BookVM.Author,
                    Genre = BookVM.Genre,
                    Image = result.Url.ToString(),
                    Language = BookVM.Language,
                    Name = BookVM.Name,
                    Price = BookVM.Price,
                    Publisher = BookVM.Publisher,
                    Status = BookVM.Status,
                    PublishDate = BookVM.PublishDate,
                };
                _bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Log the error for debugging
                }
                //ModelState.AddModelError("", "Photo Upload failed");
            }
            return View(BookVM);
        
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Book = await _bookRepository.GetByIdAsync(id);

            var BookVM = new EditBookViewModel
            {
                Name = Book.Name,
                Author = Book.Author,
                bookGenre = Book.Genre,
                bookLanguage = Book.Language,
                Price = Book.Price,
                Publisher = Book.Publisher,
                Status = Book.Status,
                PublishDate = Book.PublishDate,
                URL = Book.Image,
            };
            return View(BookVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookViewModel BookVM)
        {

            if (!ModelState.IsValid)
            {
                return View(BookVM);
            }
            //Get Old Book
            var userBook = await _bookRepository.GetByIdAsyncNoTracking(id);
            if (userBook != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userBook.Image);
                }
                catch
                {
                    ModelState.AddModelError("", "Could not delete old photo");
                    return View(BookVM);
                }
                //upload new Image
                var photoResult = await _photoService.AddPhotoAsync(BookVM.Image);

                //mapping to EditClub
                var book = new Book
                {
                    Id = id,
                    Name = BookVM.Name,
                    Author = BookVM.Author,
                    Publisher = BookVM.Publisher,
                    Genre = BookVM.bookGenre,
                    Language = BookVM.bookLanguage,
                    Price = BookVM.Price,
                    Status = BookVM.Status,
                    PublishDate = BookVM.PublishDate,
                    Image = photoResult.Url.ToString(),
                };
                _bookRepository.Update(book);
                return RedirectToAction("Index");
            }
            else
            {
                return View(BookVM);
            }

        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Book? book=await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                await _photoService.DeletePhotoAsync(book.Image);
                _bookRepository.Delete(book);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
