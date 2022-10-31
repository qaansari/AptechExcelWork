using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.OleDb;

namespace ExcelFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly int UserID;

        public HomeController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            UserID = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserID").Value);

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RenderedExcel(DataTable dt)
        {
            return View("Index", dt);
        }
        [HttpPost]
        public IActionResult uploadFile(IFormFile excelFile)
        {
            if (excelFile != null)
            {
                //Save the uploaded file in a folder
                var rootPath = _webHostEnvironment.WebRootPath;
                var fileName = Path.GetFileName(excelFile.FileName);
                var uploadPath = Path.Combine(rootPath, "ExcelFiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var folderName = DateTime.UtcNow.AddHours(5).ToString("ddMMyyyyHHmmss");
                var folderPath = Path.Combine(uploadPath, folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    excelFile.CopyTo(stream);
                }
                var dt = readData(filePath);
                return RenderedExcel(dt);
            }
            else
            {
                TempData["Message"] = "Please Select a File First";
            }
            return RedirectToAction("Index");
            
        }

        public DataTable readData(string FilePath)
        {
            string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'", FilePath);
            OleDbConnection con = new OleDbConnection(excelCS);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from [Sheet1$]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
