using LMS.Data;
using LMS.Models;
using LMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) { return View(loginViewModel); }

            //check If user email exist
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //check the password of the user
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                //if password true login in 
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Welcome ";
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Password Incorrect
                TempData["Error"] = "Invalid Password Try again!";
                return View(loginViewModel);
            }

            //Password Incorrect
            TempData["Error"] = "Invalid Email";
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["success"] = "Logged out Successfully";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            //already user exist with the same email
            if (user != null)
            {
                TempData["Error"] = "This Email Address Already Exist";
                return View(registerViewModel);
            }

            if (!ModelState.IsValid) return View(registerViewModel);

            var newUser = new AppUser()
            {
                FirstName=registerViewModel.FirstName,
                LastName=registerViewModel.LastName,
                UserName = registerViewModel.FirstName,
                Email = registerViewModel.EmailAddress,
                NationalID = registerViewModel.NationalID,
                Address= registerViewModel.Address,
                BitrthDate=registerViewModel.BirthDate
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (result.Succeeded)
            {
               await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                TempData["success"] = "Account Created Successfully ";

                return RedirectToAction("Index", "Home");
            }

            else
            {
               
                return View(registerViewModel);
            }
        }

    }
}
