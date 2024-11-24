using LMS.Data;
using LMS.Interface;
using LMS.IRepository;
using LMS.Models;
using LMS.Repository;
using LMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookUserRepository _bookUserRepository;




        public BookController(IPhotoService photoService, IBookRepository bookRepository,IHttpContextAccessor httpContextAccessor,IAuthorRepository authorRepository,IPublisherRepository publisherRepository,IBookUserRepository bookUserRepository)
        {
            _photoService = photoService;
            _bookRepository = bookRepository;
            _contextAccessor= httpContextAccessor;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _bookUserRepository= bookUserRepository;
        }
        public async Task<IActionResult> Index(string? search, int? authorId, int? publisherId, BookGenre? genre, BookLanguage? language, decimal? minPrice, decimal? maxPrice)
        {    
            var books = await _bookRepository.GetFilteredBooksAsync(search, authorId, publisherId, genre, language, minPrice, maxPrice);

            var authors = await _authorRepository.GetAllAuthorsAsync();
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var publishers = await _publisherRepository.GetAllPublishersAsync();

            var bookuserStatus = new Dictionary<int, bool>();
            foreach (var book in books)
            {
                var hasBorrowed = await _bookUserRepository.HasUserBorrowedBookAsync(currentUserId, book.Id);
            }
            var viewModel = new BookIndexViewModel
            {
                appuserid=currentUserId,
                Books = books,
                Authors = authors,
                Publishers = publishers,
                SelectedGenre = genre,
                SelectedLanguage = language,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                userbookStatus=bookuserStatus
            };

            return View(viewModel);
        }
        //public async  Task<IActionResult> Index()
        //{

        //    var Books = await _bookRepository.GetAllAsync();

        //    return View(Books);
        //}
        public async Task<IActionResult> Detail(int id)
        {
            Book? book = await _bookRepository.GetByIdAsync(id);
            int AuthorID = book.AuthorId;
           var Authorbooks = await _bookRepository.GetBookByAuthor(AuthorID);
            var DetailVM = new DetailsViewModel()
            {
                book = book,
                AuthorBooks = Authorbooks,
            };
            return View(DetailVM);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult>Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateBookViewModel BookVM)
        {
            if (ModelState.IsValid)
            {
                var existingAuthor = await _authorRepository.FindByNameAsync(BookVM.Author.name);
                var result = await _photoService.AddPhotoAsync(BookVM.Image);
                Author author;
                if (existingAuthor != null)
                {
                    author = existingAuthor;
                }
                else
                { 
                    author = new Author()
                    {
                        name = BookVM.Author.name,
                    };
                _authorRepository.Add(author);
                }
                Book book = new Book
                {
                    Author =author,
                    Genre = BookVM.Genre,
                    Image = result.Url.ToString(),
                    Language = BookVM.Language,
                    Name = BookVM.Name,
                    Quantity= BookVM.Quantity,
                    Price = BookVM.Price,
                    Publisher = BookVM.Publisher,
                    Status = BookVM.Status,
                    PublishDate = BookVM.PublishDate,
                };
                _bookRepository.Add(book);
                TempData["success"] = "Book Created Successfully";
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
        [Authorize(Roles = "admin")]
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
                Quantity= Book.Quantity,
                Publisher = Book.Publisher,
                Status = Book.Status,
                PublishDate = Book.PublishDate,
                URL = Book.Image,
            };
            return View(BookVM);
        }
        [Authorize(Roles = "admin")]
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
                    Quantity = BookVM.Quantity,
                    PublishDate = BookVM.PublishDate,
                    Image = photoResult.Url.ToString(),
                };
                _bookRepository.Update(book);
                TempData["success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(BookVM);
            }

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Book? book=await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                await _photoService.DeletePhotoAsync(book.Image);
                _bookRepository.Delete(book);
                TempData["success"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]

        public async Task<IActionResult> Borrow(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _bookUserRepository.HasUserBorrowedBookAsync(currentUserId, id))
            {
                TempData["error"] = "You have already borrowed this book.";
                return RedirectToAction("Index");
            }


            if (book.Quantity > 0)
            {
                BookUser bookUser = new BookUser
                {
                    BookId = book.Id,
                    AppUserId = currentUserId,
                    ReserveDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14),
                    ReturnDate = null,
                };
                _bookUserRepository.AddBookUser(bookUser);
                book.Quantity--;
                _bookRepository.Update(book);
                TempData["success"] = "Book reserved";
                return RedirectToAction("Index");
            }
            return NotFound();            
        }
    }
}
