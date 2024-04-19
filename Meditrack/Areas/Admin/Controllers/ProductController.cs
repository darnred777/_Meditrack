using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ManageProduct()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategory").ToList();
            return View(objProductList);
        }

        public IActionResult UpsertProduct(int? productID)
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

            if (productID == null || productID == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.ProductID == productID);

                //update
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult UpsertProduct(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                bool isNewProduct = productVM.Product.ProductID == 0;
                if (isNewProduct)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    var existingProduct = _unitOfWork.Product.Get(u => u.ProductID == productVM.Product.ProductID);
                    if (existingProduct != null)
                    {
                        bool updateDetails = existingProduct.UnitPrice != productVM.Product.UnitPrice || existingProduct.UnitOfMeasurement != productVM.Product.UnitOfMeasurement;

                        if (updateDetails)
                        {
                            var requisitionDetails = _unitOfWork.PurchaseRequisitionDetail.GetAll(r => r.ProductID == productVM.Product.ProductID);
                            foreach (var detail in requisitionDetails)
                            {
                                detail.UnitPrice = productVM.Product.UnitPrice;
                                detail.UnitOfMeasurement = productVM.Product.UnitOfMeasurement;
                                detail.Subtotal = detail.UnitPrice * detail.QuantityInOrder;
                                _unitOfWork.PurchaseRequisitionDetail.Update(detail);
                            }

                            // Update the total amounts in headers
                            var affectedHeaders = requisitionDetails.Select(r => r.PRHdrID).Distinct();
                            foreach (var headerId in affectedHeaders)
                            {
                                var header = _unitOfWork.PurchaseRequisitionHeader.GetFirstOrDefault(h => h.PRHdrID == headerId);
                                if (header != null)
                                {
                                    header.TotalAmount = _unitOfWork.PurchaseRequisitionDetail.GetAll(d => d.PRHdrID == headerId).Sum(d => d.Subtotal);
                                    _unitOfWork.PurchaseRequisitionHeader.Update(header);
                                }
                            }
                        }

                        existingProduct.UnitPrice = productVM.Product.UnitPrice;
                        existingProduct.UnitOfMeasurement = productVM.Product.UnitOfMeasurement;
                        existingProduct.ProductName = productVM.Product.ProductName;
                        existingProduct.Brand = productVM.Product.Brand;
                        existingProduct.ProductDescription = productVM.Product.ProductDescription;
                        existingProduct.QuantityInStock = productVM.Product.QuantityInStock;
                        existingProduct.ExpirationDate = productVM.Product.ExpirationDate;
                        existingProduct.isActive = productVM.Product.isActive;

                        _unitOfWork.Product.Update(existingProduct);
                    }
                }

                _unitOfWork.Save();
                return RedirectToAction("ManageProduct");
            }
            else
            {
                productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryID.ToString()
                });
                return View(productVM);
            }
        }





        //[HttpPost]
        //public IActionResult UpsertProduct(ProductVM productVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (productVM.Product.ProductID == 0)
        //        {
        //            // Adding a new user
        //            _unitOfWork.Product.Add(productVM.Product);
        //        }
        //        else
        //        {
        //            // Updating an existing user
        //            _unitOfWork.Product.Update(productVM.Product);
        //        }

        //        _unitOfWork.Save();

        //        return RedirectToAction("ManageProduct");
        //    }
        //    else
        //    {
        //        //UserVM userVM = new()
        //        //{
        //        productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.CategoryName,
        //            Value = u.CategoryID.ToString()
        //        });
        //        return View(productVM);
        //    }
        //}

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
            if (ProductID == null || ProductID == 0)
            {
                return NotFound();
            }

            Product? product = _unitOfWork.Product.Get(u => u.ProductID == ProductID);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the product has dependencies
            if (_unitOfWork.Product.HasDependencies(product.ProductID))
            {
                // If dependencies exist, do not delete and show a message
                TempData["Error"] = "You can't delete this Product because it has associated Data Existed";
                return RedirectToAction("DeleteProduct", new { ProductID });
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully.";

            return RedirectToAction("ManageProduct");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}