using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Meditrack.Areas.InventoryOfficer.Controllers
{
    [Area("InventoryOfficer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ManageProduct()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult AddNewProduct()
        {
            ProductVM productVM = new()
            {
                ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryID.ToString()
                }),

                Product = new Product()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult AddNewProduct(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();

                return RedirectToAction("ManageProduct");

            }
            else
            {
                //UserVM userVM = new()
                //{
                productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryID.ToString()
                });
                return View(productVM);
            }
        }

        public IActionResult EditProduct(int? ProductID)
        {
            if (ProductID == null || ProductID == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.ProductID == ProductID);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult EditProduct(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                return RedirectToAction("ManageProduct");
            }
            return View();
        }

        public IActionResult DeleteProduct(int? ProductID)
        {
            if (ProductID == null || ProductID == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.ProductID == ProductID);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public IActionResult DeletePOSTProduct(int? ProductID)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.ProductID == ProductID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageProduct");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}