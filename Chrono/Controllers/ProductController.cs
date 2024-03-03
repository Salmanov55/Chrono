using Microsoft.AspNetCore.Mvc;

namespace Chrono.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Model()
        {
            return View();
        }
    }
}
