using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Areas.InventoryOfficer.Controllers
{
    [Area("InventoryOfficer")]
    [Authorize(Roles = StaticDetails.Role_InventoryOfficer)]
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

        public IActionResult UpsertProductCategory(int? categoryID)
        {
            ProductCategoryVM categoryVM = new()
            {
                ProductCategory = new ProductCategory()
                {
                    CategoryName = "",
                    CategoryDescription = ""
                }
            };

            if (categoryID == null || categoryID == 0)
            {
                // Create
                return View(categoryVM);
            }
            else
            {
                categoryVM.ProductCategory = _unitOfWork.ProductCategory.Get(u => u.CategoryID == categoryID);

                // Update
                return View(categoryVM);
            }
        }

        [HttpPost]
        public IActionResult UpsertProductCategory(ProductCategoryVM productCategoryVM)
        {
            if (ModelState.IsValid)
            {
                if (productCategoryVM.ProductCategory.CategoryID == 0)
                {
                    // Adding a new location
                    _unitOfWork.ProductCategory.Add(productCategoryVM.ProductCategory);
                }
                else
                {
                    // Updating an existing location
                    // Get the existing location entity from the database
                    var existingCategory = _unitOfWork.ProductCategory.Get(u => u.CategoryID == productCategoryVM.ProductCategory.CategoryID);

                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing location entity with the new values
                    existingCategory.CategoryName = productCategoryVM.ProductCategory.CategoryName;
                    existingCategory.CategoryDescription = productCategoryVM.ProductCategory.CategoryDescription;

                    // Update the location entity in the database
                    _unitOfWork.ProductCategory.Update(existingCategory);
                }

                _unitOfWork.Save();

                return RedirectToAction("ManageProductCategory");
            }
            else
            {
                return View(productCategoryVM);
            }
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
            if (CategoryID == null)
            {
                return NotFound();
            }

            // Check if the category is linked to any products
            if (_unitOfWork.ProductCategory.HasProducts(CategoryID.Value))
            {
                TempData["Error"] = "Cannot delete this category because it has associated Data Existed.";
                return RedirectToAction("DeleteProductCategory", new { CategoryID });
            }

            ProductCategory obj = _unitOfWork.ProductCategory.Get(u => u.CategoryID == CategoryID);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductCategory.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully.";

            return RedirectToAction("ManageProductCategory");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
