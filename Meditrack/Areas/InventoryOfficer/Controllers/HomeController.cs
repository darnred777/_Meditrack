using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
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
            try
            {
                var approvedPRs = _unitOfWork.PurchaseRequisitionHeader
                    .GetAll(includeProperties: "Status")
                    .Where(pr => pr.Status != null && pr.Status.StatusDescription == StaticDetails.Status_Approved);

                DateTime currentDate = DateTime.Today;
                DateTime firstDayOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                DateTime firstDayOfNextMonth = firstDayOfCurrentMonth.AddMonths(1);

                var approvedPRsCurrentMonth = approvedPRs
                    .Where(pr => pr.PRDate >= firstDayOfCurrentMonth && pr.PRDate < firstDayOfNextMonth)
                    .ToList(); // Execute query and fetch results to debug

                var approvedPRsNextMonth = approvedPRs
                    .Where(pr => pr.PRDate >= firstDayOfNextMonth)
                    .ToList(); // Execute query and fetch results to debug

                int totalApprovedPRs = approvedPRs?.Count() ?? 0;
                int totalApprovedPRsCurrentMonth = approvedPRsCurrentMonth?.Count() ?? 0;
                int totalApprovedPRsNextMonth = approvedPRsNextMonth?.Count() ?? 0;

                // Log the counts for debugging
                Console.WriteLine($"Total Approved PRs: {totalApprovedPRs}");
                Console.WriteLine($"Total Approved PRs Current Month: {totalApprovedPRsCurrentMonth}");
                Console.WriteLine($"Total Approved PRs Next Month: {totalApprovedPRsNextMonth}");

                ViewBag.TotalApprovedPRs = totalApprovedPRs;
                ViewBag.TotalApprovedPRsCurrentMonth = totalApprovedPRsCurrentMonth;
                ViewBag.TotalApprovedPRsNextMonth = totalApprovedPRsNextMonth;

                return View();
            }
            catch (Exception ex)
            {
                // Handle any exceptions gracefully
                Console.WriteLine($"Error in Report action: {ex.Message}");
                ViewBag.ErrorMessage = "An error occurred while generating the report.";
                return View();
            }
        }

        public IActionResult TransactionLogs()
        {
            // Retrieve transaction logs data from the database
            var transactionLogs = _unitOfWork.TransactionLogs.GetAll(includeProperties: "ApplicationUser,Status,Product");

            // Map transaction logs data to view model
            var viewModelList = transactionLogs.Select(t => new TransactionLogsVM
            {
                TransactionID = t.TransactionID,
                TransType = "Purchase Requisition",
                ApplicationUserId = t.ApplicationUserId,
                ApplicationUserEmail = t.ApplicationUser.Email,
                StatusDescription = t.Status.StatusDescription,
                ProductName = t.Product.ProductName,
                Quantity = (int)t.Quantity,
                TransDate = t.TransDate
            }).ToList();

            return View(viewModelList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
