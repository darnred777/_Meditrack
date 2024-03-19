using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductCategoryController(IUnitOfWork unitOfWOrk)
        {
            _unitOfWork = unitOfWOrk;
        }

        public IActionResult ManageProductCategory()
        {
            List<ProductCategory> objProductCategoryList = _unitOfWork.ProductCategory.GetAll().ToList();
            return View(objProductCategoryList);
        }

        public IActionResult AddNewProductCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProductCategory(ProductCategory obj)
        {
            _unitOfWork.ProductCategory.Add(obj);
            _unitOfWork.Save();
            return RedirectToAction("ManageProductCategory");
        }

        public IActionResult EditProductCategory(int? CategoryID)
        {
            if (CategoryID == null || CategoryID == 0)
            {
                return NotFound();
            }
            ProductCategory? productCategoryFromDb = _unitOfWork.ProductCategory.Get(u => u.CategoryID == CategoryID);

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
                _unitOfWork.ProductCategory.Update(obj);
                _unitOfWork.Save();

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
            ProductCategory? productCategoryFromDb = _unitOfWork.ProductCategory.Get(u => u.CategoryID == CategoryID);

            if (productCategoryFromDb == null)
            {
                return NotFound();
            }
            return View(productCategoryFromDb);
        }

        [HttpPost, ActionName("DeleteProductCategory")]
        public IActionResult DeletePOSTProductCategory(int? CategoryID)
        {
            ProductCategory? obj = _unitOfWork.ProductCategory.Get(u => u.CategoryID == CategoryID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductCategory.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageProductCategory");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
