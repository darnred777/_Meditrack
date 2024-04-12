using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Meditrack.Approver.Controllers
{
    [Area("Approver")]
    [Authorize(Roles = StaticDetails.Role_Approver)]

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