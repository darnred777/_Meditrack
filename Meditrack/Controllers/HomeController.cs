using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Update_Profile()
        {
            return View();
        }

        public IActionResult ManageUserAccount()
        {
            return View();
        }

        public IActionResult ManageUserGroup()
        {
            return View();
        }

        public IActionResult AddNewUserAccount()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
