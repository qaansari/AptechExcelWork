using Microsoft.AspNetCore.Mvc;

namespace ExcelFileUpload.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
    }
}
