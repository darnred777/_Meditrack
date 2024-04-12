using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]

    public class PRTransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PRTransactionController(IUnitOfWork unitOfWork , IPurchaseOrderService purchaseOrderService)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderService = purchaseOrderService;
        }

        //PODetails List
        public IActionResult PODList()
        {
            return View();
        }

        //PRDetails List
        public IActionResult PRDList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApprovePR(int prdId)
        {
            try
            {
                // Logic to approve the purchase requisition with the specified prdId
                // You can use the prdId parameter to identify the purchase requisition to approve
                // Call the appropriate service method to handle the approval process

                // Assuming you have a service instance available
                _purchaseOrderService.CreatePurchaseOrderFromRequisition(prdId);

                // Return a success response
                return Ok("Purchase requisition approved successfully!");
            }
            catch (Exception ex)
            {
                // Return an error response if there's an exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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
                // Initialize the TotalAmount with 0
                viewModel.PurchaseRequisitionHeader.TotalAmount = 0;

                // Set the status of the new Purchase Requisition to "Pending"
                viewModel.PurchaseRequisitionHeader.Status = new Status
                {
                    StatusDescription = StaticDetails.Status_Pending
                };


                _unitOfWork.PurchaseRequisitionHeader.Add(viewModel.PurchaseRequisitionHeader);
                _unitOfWork.Save();

                // Assign the newly generated header ID to the detail
                viewModel.PurchaseRequisitionDetail.PRHdrID = viewModel.PurchaseRequisitionHeader.PRHdrID;

                // Add the new PurchaseRequisitionDetail to the database
                _unitOfWork.PurchaseRequisitionDetail.Add(viewModel.PurchaseRequisitionDetail);

                // Update the header's TotalAmount with the subtotal of the detail
                viewModel.PurchaseRequisitionHeader.TotalAmount += viewModel.PurchaseRequisitionDetail.Subtotal;

                // Save the updated TotalAmount in PurchaseRequisitionHeader
                _unitOfWork.PurchaseRequisitionHeader.Update(viewModel.PurchaseRequisitionHeader);
                _unitOfWork.Save();

                return RedirectToAction("PRDList", "PRTransaction");
            }

            // Re-populate the dropdown lists if the ModelState is not valid
            viewModel.ProductList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            }).ToList();

            viewModel.SupplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            }).ToList();

            viewModel.LocationList = _unitOfWork.Location.GetAll().Select(l => new SelectListItem
            {
                Value = l.LocationID.ToString(),
                Text = l.LocationAddress
            }).ToList();

            viewModel.StatusList = _unitOfWork.Status.GetAll().Select(s => new SelectListItem
            {
                Value = s.StatusID.ToString(),
                Text = s.StatusDescription
            }).ToList();

            return View(viewModel);
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
                    // Fetch all related PurchaseRequisitionDetails based on PRHdrID
                    var requisitionDetails = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");

                    // Calculate the total amount by summing up the subtotals of all details
                    decimal totalAmount = requisitionDetails.Sum(detail => detail.Subtotal);

                    // Update the TotalAmount property in the PurchaseRequisitionHeader
                    requisitionHeader.TotalAmount = totalAmount;

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
                    detail.PurchaseRequisitionHeader.TotalAmount,
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


        //Retrieving the PO Headers and PO Details
        [HttpGet]
        public IActionResult GetAllPODetails()
        {
            // Fetch all PurchaseOrderHeaders including related entities
            var headers = _unitOfWork.PurchaseOrderHeader.GetAll(includeProperties: "Supplier,Location,Status");

            // Fetch all PurchaseOrderDetails including related entities
            var details = _unitOfWork.PurchaseOrderDetail.GetAll(includeProperties: "Product,PurchaseOrderHeader");

            // Project the details and headers into an anonymous type
            var detailsWithStatus = details.Select(detail => new
            {
                detail.PODtlID,
                detail.POHdrID,
                PurchaseOrderHeader = new
                {
                    detail.PurchaseOrderHeader.Supplier.SupplierName,
                    detail.PurchaseOrderHeader.Location.LocationAddress,
                    detail.PurchaseOrderHeader.Status.StatusDescription,
                    detail.PurchaseOrderHeader.TotalAmount,
                    detail.PurchaseOrderHeader.PODate
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

// trash section

// POST: /Admin/AddPurchase/Create
//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult CreatePR(PRTransactionVM viewModel)
//{
//    if (ModelState.IsValid)
//    {
//        // Compute subtotal before saving
//        viewModel.PurchaseRequisitionDetail.Subtotal =
//            viewModel.PurchaseRequisitionDetail.UnitPrice *
//            viewModel.PurchaseRequisitionDetail.QuantityInOrder;

//        // Add the new PurchaseRequisitionHeader to the database
//        _unitOfWork.PurchaseRequisitionHeader.Add(viewModel.PurchaseRequisitionHeader);
//        _unitOfWork.Save();

//        // Assign the newly generated header ID to the detail
//        viewModel.PurchaseRequisitionDetail.PRHdrID = viewModel.PurchaseRequisitionHeader.PRHdrID;

//        // Add the new PurchaseRequisitionDetail to the database
//        _unitOfWork.PurchaseRequisitionDetail.Add(viewModel.PurchaseRequisitionDetail);
//        _unitOfWork.Save();

//        return RedirectToAction("PRDList", "PRTransaction");  
//    }

//    // If model state is not valid, return the view with validation errors
//    return View("PRDList", viewModel);
//}


//public IActionResult ViewPRDetails(int prviewId)
//{
//    PRTransactionVM prTransactionVM= new()
//    {
//        PurchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == prviewId, includeProperties: "Supplier,Location,Status"),
//        PurchaseRequisitionDetail = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRHdrID == prviewId, includeProperties: "Product")
//    };

//    return View(prTransactionVM);
//}


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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