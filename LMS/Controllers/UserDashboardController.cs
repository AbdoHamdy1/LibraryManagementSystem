using LMS.Interface;
using LMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS.Controllers
{
    [Authorize(Roles ="user")]
    public class UserDashboardController:Controller
    {
        private readonly IUserDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IPhotoService _photoService;

        public UserDashboardController(IUserDashboardRepository dashboardRepository, IHttpContextAccessor httpcontextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpcontextAccessor = httpcontextAccessor;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var CurrentUserId = _httpcontextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var appuser = await _dashboardRepository.GetUserById(CurrentUserId);

            var UserBooks = await _dashboardRepository.GetAllUserBooks();

            var UserdashboardVM = new UserDashboardViewModel()
            {
                Books = UserBooks,
                firstname=appuser.FirstName,
                lastname=appuser.LastName,
                Image=appuser.Image,
               Id=appuser.Id
               
            };
            return View(UserdashboardVM);
        }

        public async Task<IActionResult> EditUserProfile()
        {
           
            var CurrentUserId = _httpcontextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _dashboardRepository.GetUserById(CurrentUserId);

            if (user == null)
            {
                return NotFound();
            }
            var editUserViewModel = new EditUserViewModel()
            {
                Id = user.Id,
               Address = user.Address,
               FirstName   = user.FirstName,
               LastName = user.LastName,    
                ProfileImageUrl = user.Image,
                NationalID=user.NationalID
            };
            return View(editUserViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Faild to edit Profile");
                return View("EditUserProfile", editVM);
            }
            //editview model has 5 properties, AppUser 15 propertey

            var CurrentUserId = _httpcontextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _dashboardRepository.GetUserById(CurrentUserId);

            //user doesn't have profile image yet
            if (!string.IsNullOrEmpty(user.Image))
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.Image);
                }
                catch
                {
                    ModelState.AddModelError("", "Couldt delete the preivous image");
                    return View(editVM);
                }
            }
            var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
           
            user.Image = photoResult.Url.ToString();
            user.Address = editVM.Address;
            user.FirstName=editVM.FirstName;
            user.LastName=editVM.LastName;
            user.NationalID=editVM.NationalID;

            _dashboardRepository.Update(user);

            return RedirectToAction("Index");

        }
    }
}
    

