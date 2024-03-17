using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
