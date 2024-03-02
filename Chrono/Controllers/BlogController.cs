using Microsoft.AspNetCore.Mvc;

namespace Chrono.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
