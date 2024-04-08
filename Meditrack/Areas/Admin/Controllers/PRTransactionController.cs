using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class PRTransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PRTransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //PRHeader List
        public IActionResult PRList()
        {
            return View();
        }

        //PRDetails List
        public IActionResult PRDList()
        {
            return View();
        }

        public IActionResult ViewPRDetails(int prdId)
        {
            // Fetch the PRDetail based on PRDtlID
            var purchaseRequisitionDetail = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == prdId, includeProperties: "Product");

            if (purchaseRequisitionDetail == null)
            {
                // Handle the case where PRDetail is not found
                return NotFound();
            }

            // Fetch the corresponding PRHeader based on PRHdrID in the PRDetail
            var purchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == purchaseRequisitionDetail.PRHdrID, includeProperties: "Supplier,Location,Status");

            if (purchaseRequisitionHeader == null)
            {
                // Handle the case where PRHeader is not found
                return NotFound();
            }

            // Create a view model to hold both PRHeader and PRDetail
            var prTransactionVM = new PRTransactionVM
            {
                PurchaseRequisitionHeader = purchaseRequisitionHeader,
                PurchaseRequisitionDetail = purchaseRequisitionDetail
            };

            return View(prTransactionVM);
        }


        // Pass the PRDetail to the view
        //return View(prDetail);      

        ////////////////////////////////////

        //Adding PR Details
        public IActionResult PRDetails(int prId)
        {
            // Fetch purchase requisition header
            var prHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == prId, includeProperties: "Supplier,Location,Status");

            // Fetch all products and convert them to SelectListItem objects
            var productList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(), // Assuming Id is the property you want to use as the value
                Text = p.ProductName // Assuming Name is the property you want to display as the text
            });

            // Create a new PRTransactionVM with the fetched PRHeader and product list
            PRTransactionVM prTransactionVM = new PRTransactionVM
            {
                PurchaseRequisitionHeader = prHeader,
                ProductList = productList,
                // Initialize an empty PRDetail
                PurchaseRequisitionDetail = new PurchaseRequisitionDetail { PRHdrID = prId } // Assuming PRHdrID is the property representing the foreign key
            };

            return View(prTransactionVM);
        }

        //Adding PR Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PRDetails(PRTransactionVM prTransactionVM)
        {
            if (ModelState.IsValid)
            {
                // Compute subtotal before saving
                prTransactionVM.PurchaseRequisitionDetail.Subtotal =
                    prTransactionVM.PurchaseRequisitionDetail.UnitPrice *
                    prTransactionVM.PurchaseRequisitionDetail.QuantityInOrder;

                // Adding a new detail
                _unitOfWork.PurchaseRequisitionDetail.Add(prTransactionVM.PurchaseRequisitionDetail);

                // Update TotalAmount in PurchaseRequisitionHeader
                // Retrieve the PR Header based on its ID
                var requisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(header => header.PRHdrID == prTransactionVM.PurchaseRequisitionDetail.PRHdrID);

                if (requisitionHeader != null)
                {                                       

                    // Add the new PR Detail's subtotal to the PR Header's total amount
                    requisitionHeader.TotalAmount = prTransactionVM.PurchaseRequisitionDetail.Subtotal;

                    // Update the PR Header in the database
                    _unitOfWork.PurchaseRequisitionHeader.Update(requisitionHeader);

                    // Save changes to the database
                    _unitOfWork.Save();
                }


                return RedirectToAction("PRDList");
            }          

                return View(prTransactionVM);
            
        }


        ///////


        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        //public IActionResult DisplayPRDetails(int prId)
        //{
        //    // Fetch all products and convert them to SelectListItem objects
        //    var productList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
        //    {
        //        Value = p.ProductID.ToString(), // Assuming Id is the property you want to use as the value
        //        Text = p.ProductName // Assuming Name is the property you want to display as the text
        //    });

        //    // Fetch purchase requisition header
        //    var prHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == prId, includeProperties: "Supplier,Location,Status");

        //    // Fetch purchase requisition details for the given PR ID
        //    var prDetails = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRHdrID == prId, includeProperties: "Product");

        //    PRTransactionVM prTransactionVM = new PRTransactionVM
        //    {
        //        PurchaseRequisitionHeader = prHeader,
        //        //PurchaseRequisitionDetail = prDetails,
        //        ProductList = productList
        //    };

        //    return View(prTransactionVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DisplayPRDetails(PRTransactionVM prTransactionVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Compute subtotal before saving
        //            prTransactionVM.PurchaseRequisitionDetail.Subtotal =
        //            prTransactionVM.PurchaseRequisitionDetail.UnitPrice *
        //            prTransactionVM.PurchaseRequisitionDetail.QuantityInOrder;

        //        if (prTransactionVM.PurchaseRequisitionDetail.PRDtlID == 0)
        //        {
        //            // Adding a new detail
        //            _unitOfWork.PurchaseRequisitionDetail.Add(prTransactionVM.PurchaseRequisitionDetail);

        //        }
        //        else
        //        {
        //            // Updating an existing detail
        //            _unitOfWork.PurchaseRequisitionDetail.Update(prTransactionVM.PurchaseRequisitionDetail);
        //        }

        //        // Update TotalAmount in PurchaseRequisitionHeader
        //        var requisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(header => header.PRHdrID == prTransactionVM.PurchaseRequisitionDetail.PRHdrID);
        //        if (requisitionHeader != null)
        //        {
        //            requisitionHeader.TotalAmount += prTransactionVM.PurchaseRequisitionDetail.Subtotal;
        //        }

        //        _unitOfWork.Save();

        //        return RedirectToAction("ManagePurchaseRequisitionDetail");
        //    }
        //    else
        //    {
        //        // Re-fetch product list and PR header for dropdowns
        //        prTransactionVM.ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.ProductName,
        //            Value = u.ProductID.ToString()
        //        });

        //        prTransactionVM.PurchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == prTransactionVM.PurchaseRequisitionDetail.PRHdrID, includeProperties: "Supplier,Location,Status");

        //        return View(prTransactionVM);
        //    }
        //}



        #region API CALLS

        //Retrieving the PR Headers
        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch all PurchaseRequisitionHeaders
            var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status")
                .Select(header => new
                {
                    header.PRHdrID,
                    header.Supplier.SupplierName,
                    header.Location.LocationAddress,
                    header.Status.StatusDescription,
                    header.PRDate
                    // Exclude totalAmount field
                });

            return Json(new { data = headers });
        }



        //Retrieving the PR Headers and PR Details
        [HttpGet]
        public IActionResult GetAllPRDetails()
        {
            // Fetch all PurchaseRequisitionHeaders
            var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status");
            // Fetch all PurchaseRequisitionDetails
            var details = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");
          
            return Json(new { data =  details });
        }

        #endregion
    }
}
