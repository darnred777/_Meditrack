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

namespace Meditrack.Areas.Viewer.Controllers
{
    [Area("Viewer")]
    [Authorize(Roles = StaticDetails.Role_Viewer)]

    public class PRTransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
   
       

        public PRTransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;          
        }

        //PODetails List
        public IActionResult PODList()
        {
            return View();
        }
             
        #region API CALLS             
     
        //Retrieving the PO Headers and PO Details
        [HttpGet]
        public IActionResult GetAllPODetails()
        {
            // Fetch all PurchaseOrderHeaders including related entities
            var headers = _unitOfWork.PurchaseOrderHeader.GetAll(includeProperties: "Supplier,Location,Status,ApplicationUser");

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
                    detail.PurchaseOrderHeader.PODate,
                    ApplicationUserEmail = detail.PurchaseOrderHeader.ApplicationUser.Email
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