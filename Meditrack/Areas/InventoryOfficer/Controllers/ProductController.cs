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

namespace Meditrack.Areas.InventoryOfficer.Controllers
{
    [Area("InventoryOfficer")]
    [Authorize(Roles = StaticDetails.Role_InventoryOfficer)]
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
                            UpdatePurchaseRequisitionDetails(productVM, existingProduct);
                            UpdatePurchaseOrderDetails(productVM, existingProduct);
                        }

                        UpdateProductDetails(productVM, existingProduct);
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

        private void UpdateProductDetails(ProductVM productVM, Product existingProduct)
        {
            existingProduct.CategoryID = productVM.Product.CategoryID;
            existingProduct.SKU = productVM.Product.SKU;
            existingProduct.UnitPrice = productVM.Product.UnitPrice;
            existingProduct.UnitOfMeasurement = productVM.Product.UnitOfMeasurement;
            existingProduct.ProductName = productVM.Product.ProductName;
            existingProduct.Brand = productVM.Product.Brand;
            existingProduct.ProductDescription = productVM.Product.ProductDescription;
            existingProduct.QuantityInStock = productVM.Product.QuantityInStock;
            existingProduct.ExpirationDate = productVM.Product.ExpirationDate;
            existingProduct.isActive = productVM.Product.isActive;
            existingProduct.LastUnitPriceUpdated = DateTime.Now;
            existingProduct.LastQuantityInStockUpdated = DateTime.Now;

            _unitOfWork.Product.Update(existingProduct);
        }

        private void UpdatePurchaseRequisitionDetails(ProductVM productVM, Product existingProduct)
        {
            var requisitionDetails = _unitOfWork.PurchaseRequisitionDetail.GetAll(r => r.ProductID == existingProduct.ProductID);
            foreach (var detail in requisitionDetails)
            {
                detail.UnitPrice = productVM.Product.UnitPrice;
                detail.UnitOfMeasurement = productVM.Product.UnitOfMeasurement;
                detail.Subtotal = detail.UnitPrice * detail.QuantityInOrder;
                _unitOfWork.PurchaseRequisitionDetail.Update(detail);
            }

            //Update the total amounts in headers
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

        private void UpdatePurchaseOrderDetails(ProductVM productVM, Product existingProduct)
        {
            var orderDetails = _unitOfWork.PurchaseOrderDetail.GetAll(o => o.ProductID == existingProduct.ProductID);
            foreach (var detail in orderDetails)
            {
                detail.UnitPrice = productVM.Product.UnitPrice;
                detail.UnitOfMeasurement = productVM.Product.UnitOfMeasurement;
                detail.Subtotal = detail.UnitPrice * detail.QuantityInOrder;
                _unitOfWork.PurchaseOrderDetail.Update(detail);
            }

            // Optionally update the order headers' total amount if necessary
            var affectedOrderHeaders = orderDetails.Select(o => o.POHdrID).Distinct();
            foreach (var headerId in affectedOrderHeaders)
            {
                var header = _unitOfWork.PurchaseOrderHeader.GetFirstOrDefault(h => h.POHdrID == headerId);
                if (header != null)
                {
                    header.TotalAmount = _unitOfWork.PurchaseOrderDetail.GetAll(d => d.POHdrID == headerId).Sum(d => d.Subtotal);
                    _unitOfWork.PurchaseOrderHeader.Update(header);
                }
            }
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

        public IActionResult ExpiringProducts()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var tenDaysLater = today.AddDays(10);

            // Access products through the unit of work
            var expiringProducts = _unitOfWork.Product
                .GetAll(p => p.ExpirationDate >= today && p.ExpirationDate <= tenDaysLater)
                .ToList();

            return View(expiringProducts);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}