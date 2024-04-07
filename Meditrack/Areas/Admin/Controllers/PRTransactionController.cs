using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Mvc;
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
