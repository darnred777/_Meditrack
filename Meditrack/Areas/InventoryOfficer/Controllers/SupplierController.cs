using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Meditrack.Areas.InventoryOfficer.Controllers
{
    [Area("InventoryOfficer")]
    [Authorize(Roles = StaticDetails.Role_InventoryOfficer)]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ManageVendor()
        {
            List<Supplier> objSupplierList = _unitOfWork.Supplier.GetAll(includeProperties: "Location").ToList();

            return View(objSupplierList);
        }
      
        public IActionResult DeleteVendor(int? SupplierID)
        {
            if (SupplierID == null || SupplierID == 0)
            {
                return NotFound();
            }
            Supplier? supplierFromDb = _unitOfWork.Supplier.Get(u => u.SupplierID == SupplierID);

            if (supplierFromDb == null)
            {
                return NotFound();
            }
            return View(supplierFromDb);
        }

        [HttpPost, ActionName("DeleteVendor")]
        public IActionResult DeletePOSTVendor(int? SupplierID)
        {
            if (SupplierID == null)
            {
                return NotFound();
            }

            // Check if there are any Purchase Requisitions related to this Supplier
            if (_unitOfWork.PurchaseRequisitionHeader.Any(pr => pr.SupplierID == SupplierID))
            {
                TempData["error"] = "You can't delete this Supplier because it has associated Data Existed.";
                return RedirectToAction("DeleteVendor", new { SupplierID });
            }

            var supplier = _unitOfWork.Supplier.Get(u => u.SupplierID == SupplierID);
            if (supplier == null)
            {
                return NotFound();
            }

            _unitOfWork.Supplier.Remove(supplier);
            _unitOfWork.Save();
            TempData["success"] = "Supplier deleted successfully.";

            return RedirectToAction("ManageVendor");
        }

        public IActionResult UpsertVendor(int? supplierID)
        {
            SupplierVM supplierVM = new()
            {
                LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LocationAddress,
                    Value = u.LocationID.ToString()
                }),

                Supplier = new Supplier()
            };

            if (supplierID == null || supplierID == 0)
            {
                //create
                return View(supplierVM);
            }
            else
            {
                supplierVM.Supplier = _unitOfWork.Supplier.Get(u => u.SupplierID == supplierID);

                //update
                return View(supplierVM);
            }
        }

        [HttpPost]
        public IActionResult UpsertVendor(SupplierVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                if (supplierVM.Supplier.SupplierID == 0)
                {
                    // Adding a new user
                    _unitOfWork.Supplier.Add(supplierVM.Supplier);
                }
                else
                {
                    // Updating an existing user
                    _unitOfWork.Supplier.Update(supplierVM.Supplier);
                }

                _unitOfWork.Save();

                return RedirectToAction("ManageVendor");
            }
            else
            {

                supplierVM.LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LocationAddress,
                    Value = u.LocationID.ToString()
                });
                return View(supplierVM);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
