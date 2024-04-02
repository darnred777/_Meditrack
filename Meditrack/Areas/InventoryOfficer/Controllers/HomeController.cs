using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Areas.InventoryOfficer.Controllers
{
    [Area("InventoryOfficer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer + "," + StaticDetails.Role_Approver + "," + StaticDetails.Role_Viewer)]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer)]
        public IActionResult Transaction()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer + "," + StaticDetails.Role_Approver)]
        public IActionResult Notification()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer + "," + StaticDetails.Role_Approver)]
        public IActionResult Report()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer + "," + StaticDetails.Role_Approver)]
        public IActionResult Feedback()
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
