using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ManageProductCategory()
        {
            List<ProductCategory> objProductCategoryList = _db.ProductCategory.ToList();
            return View(objProductCategoryList);
        }

        public IActionResult AddNewProductCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProductCategory(ProductCategory obj)
        {
            _db.ProductCategory.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("ManageProductCategory");
        }

        public IActionResult EditProductCategory(int? CategoryID)
        {
            if (CategoryID == null || CategoryID == 0)
            {
                return NotFound();
            }
            ProductCategory? productCategoryFromDb = _db.ProductCategory.Find(CategoryID);

            if (productCategoryFromDb == null)
            {
                return NotFound();
            }
            return View(productCategoryFromDb);
        }

        [HttpPost]
        public IActionResult EditProductCategory(ProductCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProductCategory.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("ManageProductCategory");
            }
            return View();
        }

        public IActionResult DeleteProductCategory(int? CategoryID)
        {
            if (CategoryID == null || CategoryID == 0)
            {
                return NotFound();
            }
            ProductCategory? productCategoryFromDb = _db.ProductCategory.Find(CategoryID);

            if (productCategoryFromDb == null)
            {
                return NotFound();
            }
            return View(productCategoryFromDb);
        }

        [HttpPost, ActionName("DeleteProductCategory")]
        public IActionResult DeletePOSTProductCategory(int? CategoryID)
        {
            ProductCategory? obj = _db.ProductCategory.Find(CategoryID);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ProductCategory.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("ManageProductCategory");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
