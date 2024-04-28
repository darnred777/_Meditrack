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
    [Authorize(Roles = StaticDetails.Role_InventoryOfficer)]
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
            const int reorderPoint = 10;
            var lowStockProducts = _unitOfWork.Product.GetAll(p => p.QuantityInStock <= reorderPoint).ToList();

            // Pass these products to the view, perhaps within a ViewModel or ViewBag/ViewData
            ViewBag.LowStockProducts = lowStockProducts;

            var pendingPRs = _unitOfWork.PurchaseRequisitionHeader
                        .GetAll(includeProperties: "Status")
                        .Where(pr => pr.Status != null && pr.Status.StatusDescription == StaticDetails.Status_Pending);

            int totalPendingPRs = pendingPRs?.Count() ?? 0;  // Set default value to 0 if pendingPRs is null

            ViewBag.TotalPendingPRs = totalPendingPRs;

            var approvedPRs = _unitOfWork.PurchaseRequisitionHeader
                        .GetAll(includeProperties: "Status")
                        .Where(pr => pr.Status != null && pr.Status.StatusDescription == StaticDetails.Status_Approved);

            int totalApprovedPRs = approvedPRs?.Count() ?? 0;  // Set default value to 0 if pendingPRs is null

            ViewBag.TotalApprovedPRs = totalApprovedPRs;

            var cancelledPRs = _unitOfWork.PurchaseRequisitionHeader
                        .GetAll(includeProperties: "Status")
                        .Where(pr => pr.Status != null && pr.Status.StatusDescription == StaticDetails.Status_Cancelled);

            int totalCancelledPRs = cancelledPRs?.Count() ?? 0;  // Set default value to 0 if pendingPRs is null

            ViewBag.TotalCancelledPRs = totalCancelledPRs;

            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Transaction()
        {
            return View();
        }

        public IActionResult ExpiringProducts()
        {
            return View();
        }

        public IActionResult Report()
        {
            // Your code snippet here to calculate total approved PRs and monthly counts
            var approvedPRs = _unitOfWork.PurchaseRequisitionHeader
                .GetAll(includeProperties: "Status")
                .Where(pr => pr.Status != null && pr.Status.StatusDescription == StaticDetails.Status_Approved);

            DateTime currentDate = DateTime.Today;
            DateTime firstDayOfNextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);

            var approvedPRsCurrentMonth = approvedPRs
                .Where(pr => pr.PRDate >= currentDate && pr.PRDate < firstDayOfNextMonth);

            var approvedPRsNextMonth = approvedPRs
                .Where(pr => pr.PRDate >= firstDayOfNextMonth);

            int totalApprovedPRs = approvedPRs?.Count() ?? 0;
            int totalApprovedPRsCurrentMonth = approvedPRsCurrentMonth?.Count() ?? 0;
            int totalApprovedPRsNextMonth = approvedPRsNextMonth?.Count() ?? 0;

            ViewBag.TotalApprovedPRs = totalApprovedPRs;
            ViewBag.TotalApprovedPRsCurrentMonth = totalApprovedPRsCurrentMonth;
            ViewBag.TotalApprovedPRsNextMonth = totalApprovedPRsNextMonth;

            // Render the view and pass the data to the view
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
