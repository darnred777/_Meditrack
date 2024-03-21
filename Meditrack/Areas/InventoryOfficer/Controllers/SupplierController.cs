using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Meditrack.Areas.InventoryOfficer.Controllers
{
    [Area("InventoryOfficer")]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ManageVendor()
        {
            List<Supplier> objSupplierList = _unitOfWork.Supplier.GetAll().ToList();

            return View(objSupplierList);
        }

        public IActionResult EditVendor(int? SupplierID)
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

        [HttpPost]
        public IActionResult EditVendor(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Supplier.Update(obj);
                _unitOfWork.Save();

                return RedirectToAction("ManageVendor");
            }
            return View();
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
            Supplier? obj = _unitOfWork.Supplier.Get(u => u.SupplierID == SupplierID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Supplier.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageVendor");
        }

        public IActionResult AddVendor()
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

            return View(supplierVM);
        }

        [HttpPost]
        public IActionResult AddVendor(SupplierVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Supplier.Add(supplierVM.Supplier);
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
