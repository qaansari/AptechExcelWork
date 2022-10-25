using ExcelFileUpload.Helpers;
using ExcelFileUpload.Models.User_Model;
using ExcelFileUpload.Services.Interfaces;
using ExcelFileUpload.ViewModels.User_ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExcelFileUpload.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger, IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserView userView)
        {
            try
            {
                if (string.IsNullOrEmpty(userView.Email) || string.IsNullOrEmpty(userView.Password))
                {
                    TempData["ErrorMessage"] = "Incorrect Email or Password";
                    return View(userView);
                }
                var hashedPassword = userView.Password.HashedWithSalt().Trim();
                
                var user = await _userService.Login(userView.Email.Trim(), hashedPassword);
                if (user != null)
                {
                    var claims = new List<Claim>{

                   new Claim("UserID",user.UserID.ToString()),
                   new Claim("FirstName",user.FirstName),
                   new Claim("FullName",user.FullName),
                   new Claim("Email",user.Email),
                   new Claim("Password",user.Password),
                   new Claim("UserImage",user.ImageName),
                   new Claim("RoleID",user.RoleID.ToString()),
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    var state = new AuthenticationState(claimsPrincipal);
                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Incorrect Email Or Password";
                }
            }
            catch (Exception exc)
            {
                TempData["ErrorMessage"] = exc.Message;
            }
            return View(userView);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserView userView)
        {
            try
            {
                userView.Password = userView.Password.HashedWithSalt();
                userView.RoleID = 2;
                string wwwrootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(userView.ImageFile.FileName);
                string fileExtension = Path.GetExtension(userView.ImageFile.FileName);
                userView.ImageName = fileName = fileName + "_" + DateTime.UtcNow.AddHours(5).ToString("dd-mm-yyyy") + fileExtension;
                string path = Path.Combine(wwwrootPath + "/assets/images/users/", fileName);
                using (var FileStream = new FileStream(path, FileMode.Create))
                {
                    await userView.ImageFile.CopyToAsync(FileStream);
                }

                await _userService.Add(userView);
                return RedirectToAction("Index", "User");
            }
            catch (Exception exc)
            {
                TempData["ErrorMessage"] = exc.Message;
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
