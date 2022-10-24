using ExcelFileUpload.Helpers;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
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
                   new Claim("FullName",user.FullName),
                   new Claim("Email",user.Email),
                   new Claim("Password",user.Password),
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
                //Save--Image-Starts
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imageFileName = Path.GetFileNameWithoutExtension(userView.ImageFile.FileName);
                string imageFileExt = Path.GetExtension(userView.ImageFile.FileName);
                userView.ImageName = imageFileName = imageFileName + "_" + userView.UserID.ToString();
                string filePath = Path.Combine(wwwRootPath + "/assets/images/users/", imageFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await userView.ImageFile.CopyToAsync(fileStream);
                }
                //Save--Image-Ends
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
