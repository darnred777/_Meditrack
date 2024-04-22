using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var transactionLogs = _unitOfWork.TransactionLogs.GetAll().ToList();

            return View(transactionLogs);
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