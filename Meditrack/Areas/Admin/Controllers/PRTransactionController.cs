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

        // GET: /Admin/AddPurchase/CreatePR
        public IActionResult CreatePR()
        {
            // Fetch all products and convert them to SelectListItem objects
            var productList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(), // Assuming Id is the property you want to use as the value
                Text = p.ProductName // Assuming Name is the property you want to display as the text
            });

            // Fetch all suppliers and convert them to SelectListItem objects
            var supplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            });

            // Fetch all locations and convert them to SelectListItem objects
            var locationList = _unitOfWork.Location.GetAll().Select(l => new SelectListItem
            {
                Value = l.LocationID.ToString(),
                Text = l.LocationAddress
            });

            // Fetch all statuses and convert them to SelectListItem objects
            var statusList = _unitOfWork.Status.GetAll().Select(s => new SelectListItem
            {
                Value = s.StatusID.ToString(),
                Text = s.StatusDescription
            });

            // Create a new instance of the view model with empty lists
            var viewModel = new PRTransactionVM
            {
                ProductList = productList,
                SupplierList = supplierList,
                LocationList = locationList,
                StatusList = statusList,
                PurchaseRequisitionHeader = new PurchaseRequisitionHeader(),
                PurchaseRequisitionDetail = new PurchaseRequisitionDetail()
            };

            return View(viewModel);
        }


        // POST: /Admin/AddPurchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePR(PRTransactionVM viewModel)
        {
            if (ModelState.IsValid)
            {
                // Compute subtotal before saving
                viewModel.PurchaseRequisitionDetail.Subtotal =
                    viewModel.PurchaseRequisitionDetail.UnitPrice *
                    viewModel.PurchaseRequisitionDetail.QuantityInOrder;

                // Add the new PurchaseRequisitionHeader to the database
                _unitOfWork.PurchaseRequisitionHeader.Add(viewModel.PurchaseRequisitionHeader);
                _unitOfWork.Save();

                // Assign the newly generated header ID to the detail
                viewModel.PurchaseRequisitionDetail.PRHdrID = viewModel.PurchaseRequisitionHeader.PRHdrID;

                // Add the new PurchaseRequisitionDetail to the database
                _unitOfWork.PurchaseRequisitionDetail.Add(viewModel.PurchaseRequisitionDetail);
                _unitOfWork.Save();

                return RedirectToAction("PRDList", "PRTransaction");  
            }

            // If model state is not valid, return the view with validation errors
            return View("PRDList", viewModel);
        }

        //View the Purchase Requisition Details for Approval
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
                    header.PRDate
                    // Exclude totalAmount field
                });

            return Json(new { data = headers });
        }



        //Retrieving the PR Headers and PR Details
        //[HttpGet]
        //public IActionResult GetAllPRDetails()
        //{
        //    // Fetch all PurchaseRequisitionHeaders
        //    var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status");
        //    // Fetch all PurchaseRequisitionDetails
        //    var details = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");

        //    return Json(new { data =  details });
        //}

        //Retrieving the PR Headers and PR Details
        [HttpGet]
        public IActionResult GetAllPRDetails()
        {
            // Fetch all PurchaseRequisitionHeaders including related entities
            var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status");

            // Fetch all PurchaseRequisitionDetails including related entities
            var details = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");

            // Project the details and headers into an anonymous type
            var detailsWithStatus = details.Select(detail => new
            {
                detail.PRDtlID,
                detail.PRHdrID,
                PurchaseRequisitionHeader = new
                {
                    detail.PurchaseRequisitionHeader.Supplier.SupplierName,
                    detail.PurchaseRequisitionHeader.Location.LocationAddress,
                    detail.PurchaseRequisitionHeader.Status.StatusDescription,
                    detail.PurchaseRequisitionHeader.PRDate
                },
                detail.Product.ProductName,
                detail.UnitPrice,
                detail.UnitOfMeasurement,
                detail.QuantityInOrder,
                detail.Subtotal,
            });

            // Return the result as JSON
            return Json(new { data = detailsWithStatus });
        }


        #endregion
    }
}
