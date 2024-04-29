using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
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


        [Authorize(Roles = StaticDetails.Role_Admin)]
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

        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Transaction()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult ExpiringProducts()
        {
            return View();
        }

        [Authorize(Roles = StaticDetails.Role_Admin)]
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

            return View();
            //try
            //{
            //    // Retrieve all purchase requisition headers with related details and status
            //    var purchaseRequisitionHeaders = _unitOfWork.PurchaseRequisitionHeader
            //        .GetAll(includeProperties: "PurchaseRequisitionDetail,Status");

            //    foreach (var prHeader in purchaseRequisitionHeaders)
            //    {
            //        foreach (var prDetail in prHeader.PurchaseRequisitionDetail)
            //        {
            //            // Check if a transaction log already exists for the current prDetail
            //            var existingTransactionLog = _unitOfWork.TransactionLogs.GetFirstOrDefault(
            //                t => t.PRHdrID == prHeader.PRHdrID &&
            //                     t.ProductID == prDetail.ProductID);

            //            if (existingTransactionLog == null)
            //            {
            //                // Ensure that the Status property is accessible from prHeader
            //                var productStatus = prHeader.Status;

            //                // Create a new TransactionLogs entry based on the PR detail
            //                var transactionLog = new TransactionLogs
            //                {
            //                    TransType = "test", // Set the transaction type accordingly
            //                    ApplicationUserId = prHeader.ApplicationUserId,
            //                    StatusID = productStatus?.StatusID, // Use the correct property from Status
            //                    POHdrID = null, // Set to null if there's no related PO header
            //                    PRHdrID = prHeader.PRHdrID,
            //                    ProductID = prDetail.ProductID,
            //                    Quantity = prDetail.QuantityInOrder,
            //                    TransDate = DateTime.Now // Set the transaction date
            //                };

            //                // Add the new transaction log to the database
            //                _unitOfWork.TransactionLogs.Add(transactionLog);
            //            }
            //        }
            //    }

            //    // Save changes to persist the transaction logs in the database
            //    _unitOfWork.Save();

            //    // Redirect to the TransactionLogs view upon successful creation of transaction logs
            //    return RedirectToAction("TransactionLogs"); // Adjust the controller name as needed
            //}
            //catch (Exception ex)
            //{
            //    // Log the detailed exception message
            //    Console.WriteLine($"Exception: {ex}");

            //    return Json(new { success = false, message = $"Failed to create transaction logs: {ex.Message}" });
            //}
        }
  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//try
//{
//    string fromMail = "darnred7@gmail.com";
//    string fromPassword = "gdrugqgfnryhgnnu";

//    MailMessage message = new MailMessage();
//    message.From = new MailAddress(fromMail);
//    message.Subject = "Test";
//    message.To.Add(new MailAddress("flores.klarke@gmail.com"));
//    message.Body = "<html><body> Test </html> </body>";
//    message.IsBodyHtml = true;

//    var smtpClient = new SmtpClient("smtp.gmail.com")
//    {
//        Port = 587,
//        Credentials = new NetworkCredential(fromMail, fromPassword),
//        EnableSsl = true,
//    };

//    smtpClient.Send(message);

//    return Json(new { success = true, message = "Email sent successfully." });
//}
//catch (Exception ex)
//{
//    return Json(new { success = false, message = $"Failed to send email: {ex.Message}" });
//}