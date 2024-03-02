using Chrono.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chrono.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}