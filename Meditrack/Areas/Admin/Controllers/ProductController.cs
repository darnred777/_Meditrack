using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ManageProduct()
        {
            List<Product> objProductList = _db.Product.ToList();
            return View(objProductList);
        }

        public IActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(Product obj)
        {
            _db.Product.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("ManageProduct");
        }

        public IActionResult DeleteProduct(int? ProductID)
        {
            if (ProductID == null || ProductID == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _db.Product.Find(ProductID);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public IActionResult DeletePOSTProduct(int? ProductID)
        {
            Product? obj = _db.Product.Find(ProductID);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Product.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("ManageProduct");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}