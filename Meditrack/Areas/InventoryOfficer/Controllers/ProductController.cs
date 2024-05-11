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
        public IActionResult UpsertProduct(ProductVM productVM, string operation)
        {
            if (ModelState.IsValid)
            {
                bool isNewProduct = productVM.Product.ProductID == 0;

                // Set UnitPrice based on UnitOfMeasurement selection
                switch (productVM.Product.UnitOfMeasurement)
                {
                    case "PC":
                        productVM.Product.UnitPrice = 700;
                        break;
                    case "BOX":
                        productVM.Product.UnitPrice = 600;
                        break;
                    case "PACKAGE":
                        productVM.Product.UnitPrice = 500;
                        break;
                    case "VIAL":
                        productVM.Product.UnitPrice = 400;
                        break;
                    case "TUBE":
                        productVM.Product.UnitPrice = 300;
                        break;
                    case "MILILITER":
                        productVM.Product.UnitPrice = 200;
                        break;
                    default:
                        // Handle default case (if needed)
                        break;
                }

                if (isNewProduct)
                {
                    // Adding a new product
                    if (operation != "Deposit")
                    {
                        TempData["AddingProdErrorMessage"] = "Invalid operation for adding a new product.";
                        // Return the view with validation errors
                        productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
                        {
                            Text = u.CategoryName,
                            Value = u.CategoryID.ToString()
                        });
                        return View(productVM);
                    }

                    // Map selected value from dropdown to UnitOfMeasurement field
                    productVM.Product.UnitOfMeasurement = productVM.Product.UnitOfMeasurement.ToString();

                    var newProduct = new Product
                    {
                        CategoryID = productVM.Product.CategoryID,
                        ProductName = productVM.Product.ProductName,
                        SKU = productVM.Product.SKU,
                        Brand = productVM.Product.Brand,
                        ProductDescription = productVM.Product.ProductDescription,
                        UnitPrice = productVM.Product.UnitPrice,
                        UnitOfMeasurement = productVM.Product.UnitOfMeasurement,
                        QuantityInStock = productVM.QuantityChange, // Set initial stock quantity based on deposit
                        ExpirationDate = productVM.Product.ExpirationDate,
                        LastUnitPriceUpdated = productVM.Product.LastUnitPriceUpdated,
                        LastQuantityInStockUpdated = productVM.Product.LastQuantityInStockUpdated
                        // Set other properties as needed
                    };

                    _unitOfWork.Product.Add(newProduct);
                    _unitOfWork.Save();

                    return RedirectToAction("ManageProduct");
                }
                else
                {
                    // Editing an existing product
                    var existingProduct = _unitOfWork.Product.Get(u => u.ProductID == productVM.Product.ProductID);

                    if (existingProduct != null)
                    {
                        if (operation == "Withdraw")
                        {
                            if (existingProduct.QuantityInStock < productVM.QuantityChange)
                            {
                                TempData["UpdateProdErrorMessage"] = "Insufficient quantity available for withdrawal.";
                                // Return the current view with validation errors
                                productVM.Product = existingProduct; // Preserve the original product data
                                productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
                                {
                                    Text = u.CategoryName,
                                    Value = u.CategoryID.ToString()
                                });
                                return View(productVM);
                            }

                            existingProduct.QuantityInStock -= productVM.QuantityChange;
                        }
                        else if (operation == "Deposit")
                        {
                            existingProduct.QuantityInStock += productVM.QuantityChange;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid operation.");
                            // Return the view with validation errors
                            productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
                            {
                                Text = u.CategoryName,
                                Value = u.CategoryID.ToString()
                            });
                            return View(productVM);
                        }

                        // Update existing product details
                        existingProduct.CategoryID = productVM.Product.CategoryID;
                        existingProduct.ProductName = productVM.Product.ProductName;
                        existingProduct.SKU = productVM.Product.SKU;
                        existingProduct.Brand = productVM.Product.Brand;
                        existingProduct.ProductDescription = productVM.Product.ProductDescription;
                        existingProduct.UnitPrice = productVM.Product.UnitPrice;
                        // Map selected value from dropdown to UnitOfMeasurement field
                        existingProduct.UnitOfMeasurement = productVM.Product.UnitOfMeasurement.ToString();
                        //existingProduct.UnitOfMeasurement = productVM.Product.UnitOfMeasurement;
                        existingProduct.ExpirationDate = productVM.Product.ExpirationDate;
                        existingProduct.LastUnitPriceUpdated = productVM.Product.LastUnitPriceUpdated;
                        existingProduct.LastQuantityInStockUpdated = productVM.Product.LastQuantityInStockUpdated;

                        _unitOfWork.Product.Update(existingProduct);
                        _unitOfWork.Save();

                        // Update associated PRDetails and PODetails with new UnitPrice
                        UpdatePRDetailsAndPODetails(existingProduct);

                        return RedirectToAction("ManageProduct");
                    }
                }
            }

            // If model state is invalid or product not found, return to the current view with validation errors
            productVM.ProductCategoryList = _unitOfWork.ProductCategory.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.CategoryID.ToString()
            });
            return View(productVM);
        }   

        private void UpdatePRDetailsAndPODetails(Product existingProduct)
        {
            // Update PRDetails associated with the existing product
            var prDetails = _unitOfWork.PurchaseRequisitionDetail.GetAll(prd => prd.ProductID == existingProduct.ProductID);
            foreach (var prDetail in prDetails)
            {
                prDetail.UnitPrice = existingProduct.UnitPrice;
                prDetail.Subtotal = prDetail.QuantityInOrder * existingProduct.UnitPrice;
                _unitOfWork.PurchaseRequisitionDetail.Update(prDetail);

                // Update associated PRHeader with updated TotalAmount
                var prHeader = _unitOfWork.PurchaseRequisitionHeader.GetFirstOrDefault(prh => prh.PRHdrID == prDetail.PRHdrID);
                if (prHeader != null)
                {
                    prHeader.TotalAmount = _unitOfWork.PurchaseRequisitionDetail.GetAll(pd => pd.PRHdrID == prHeader.PRHdrID).Sum(pd => pd.Subtotal);
                    _unitOfWork.PurchaseRequisitionHeader.Update(prHeader);
                }
            }

            // Update PODetails associated with the existing product
            var poDetails = _unitOfWork.PurchaseOrderDetail.GetAll(pod => pod.ProductID == existingProduct.ProductID);
            foreach (var poDetail in poDetails)
            {
                poDetail.UnitPrice = existingProduct.UnitPrice;
                poDetail.Subtotal = poDetail.QuantityInOrder * existingProduct.UnitPrice;
                _unitOfWork.PurchaseOrderDetail.Update(poDetail);

                // Update associated POHeader with updated TotalAmount
                var poHeader = _unitOfWork.PurchaseOrderHeader.GetFirstOrDefault(poh => poh.POHdrID == poDetail.POHdrID);
                if (poHeader != null)
                {
                    poHeader.TotalAmount = _unitOfWork.PurchaseOrderDetail.GetAll(pd => pd.POHdrID == poHeader.POHdrID).Sum(pd => pd.Subtotal);
                    _unitOfWork.PurchaseOrderHeader.Update(poHeader);
                }
            }

            // Save changes to the database
            _unitOfWork.Save();
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
            TempData["ProductSuccess"] = "Product deleted successfully.";

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

        #region API CALLS

        [HttpGet]
        public IActionResult GetProduct()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategory").ToList();

            return Json(new { data = objProductList });
        }

        #endregion
    }

}