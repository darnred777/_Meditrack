using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PRTransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PRTransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult PRList()
        {
            return View();
        }

        public IActionResult PRDetails(int prId)
        {
            // Fetch all products and convert them to SelectListItem objects
            var productList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(), // Assuming Id is the property you want to use as the value
                Text = p.ProductName // Assuming Name is the property you want to display as the text
            });

            PRTransactionVM prTransactionVM = new PRTransactionVM
            {
                PurchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == prId, includeProperties: "Supplier,Location,Status"),
                PurchaseRequisitionDetail = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == prId, includeProperties: "Product"),
                ProductList = productList
            };
            return View(prTransactionVM);
        }

        //[HttpPost]
        //public IActionResult PRDetails(PRTransactionVM prTransactionVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Add or update PurchaseRequisitionHeader
        //        if (prTransactionVM.PurchaseRequisitionHeader.PRHdrID == 0)
        //        {
        //            _unitOfWork.PurchaseRequisitionHeader.Add(prTransactionVM.PurchaseRequisitionHeader);
        //        }
        //        // Save changes to the PurchaseRequisitionHeader
        //        _unitOfWork.Save();

        //        // Add or update PurchaseRequisitionDetails
        //        foreach (var prDetail in prTransactionVM.PurchaseRequisitionDetailList)
        //        {
        //            if (prDetail.PRDtlID == 0)
        //            {
        //                prDetail.PRHdrID = prTransactionVM.PurchaseRequisitionHeader.PRHdrID;
        //                _unitOfWork.PurchaseRequisitionDetail.Add(prDetail);
        //            }
        //        }
        //        // Save changes to the PurchaseRequisitionDetails
        //        _unitOfWork.Save();

        //        return RedirectToAction("PRList"); // Redirect to desired action after saving
        //    }
        //    else
        //    {
        //        // Model validation failed, return the view with errors
        //        return View(prTransactionVM);
        //    }
        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch all PurchaseRequisitionHeaders
            var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status");

            // Determine status for each header
            //foreach (var header in headers)
            //{
            //    // Fetch associated details
            //    // Assuming header is of type PurchaseRequisitionHeader
            //    var details = _unitOfWork.PurchaseRequisitionDetail.GetAll().Where(d => d.PRHdrID == header.PRHdrID).ToList();


            //    // Check if any detail is pending or cancelled
            //    if (details.Any(d => d != null && d.PRHdrID == header.PRHdrID && d.Status != StaticDetails.Status_Approved && d.Status != StaticDetails.Status_Cancelled))
            //    {
            //        header.Status.StatusDescription = StaticDetails.Status_Pending;
            //    }
            //    else
            //    {
            //        header.Status.StatusDescription = StaticDetails.Status_Approved;
            //    }
            //}

            return Json(new { data = headers });
        }

        #endregion
    }
}
