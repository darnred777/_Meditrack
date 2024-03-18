using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SupplierController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ManageVendor()
        {
            List<Supplier> objSupplierList = _db.Supplier.ToList();
            return View(objSupplierList);
        }

        public IActionResult EditVendor(int? SupplierID)
        {
            if (SupplierID == null || SupplierID == 0)
            {
                return NotFound();
            }
            Supplier? supplierFromDb = _db.Supplier.Find(SupplierID);

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
                _db.Supplier.Update(obj);
                _db.SaveChanges();

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
            Supplier? supplierFromDb = _db.Supplier.Find(SupplierID);

            if (supplierFromDb == null)
            {
                return NotFound();
            }
            return View(supplierFromDb);
        }

        [HttpPost, ActionName("DeleteVendor")]
        public IActionResult DeletePOSTVendor(int? SupplierID)
        {
            Supplier? obj = _db.Supplier.Find(SupplierID);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Supplier.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("ManageVendor");
        }

        public IActionResult AddVendor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddVendor(Supplier obj)
        {
            _db.Supplier.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("ManageVendor");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
