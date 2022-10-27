using Microsoft.AspNetCore.Mvc;

namespace ExcelFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly int UserID;
        public HomeController(IHttpContextAccessor httpContextAccessor,IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment= webHostEnvironment;
            UserID = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("UserID").Value);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult uploadFile(IFormFile excelFile)
        {
            if (excelFile != null)
            {
                var rootPath = _webHostEnvironment.WebRootPath;
                var fileName = Path.GetFileName(excelFile.FileName);
                var uploadPath = Path.Combine(rootPath, "ExcelFiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var folderName = DateTime.UtcNow.AddHours(5).ToString("ddMMyyyyHHmmss");
                var folderPath=Path.Combine(uploadPath, folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    excelFile.CopyTo(stream);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
