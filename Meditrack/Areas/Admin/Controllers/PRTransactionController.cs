using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]

    public class PRTransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public PRTransactionController(IUnitOfWork unitOfWork , IPurchaseOrderService purchaseOrderService, IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, ISupplierRepository supplierRepository,
            ILocationRepository locationRepository)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderService = purchaseOrderService;
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _supplierRepository = supplierRepository;
            _locationRepository = locationRepository;
        }
      
        //PODetails List
        public IActionResult PODList()
        {
            return View();
        }

        //PRDetails List
        public IActionResult PRDList()
        {
            return View();
        }       

        [HttpPost]
        public IActionResult SendPurchaseOrderEmail(int poHdrID)
        {
            try
            {
                // Fetch the specific Purchase Order Header based on poHdrID
                var purchaseOrderHeader = _unitOfWork.PurchaseOrderHeader.Get(
                    p => p.POHdrID == poHdrID && p.Status.StatusDescription == StaticDetails.Status_Approved,
                    includeProperties: "Supplier,Location,Status,ApplicationUser"
                );

                if (purchaseOrderHeader == null)
                {
                    // Handle the case where Purchase Order Header is not found or not approved
                    return NotFound();
                }                

                // Fetch the corresponding Purchase Order Details based on poHdrID
                var purchaseOrderDetails = _unitOfWork.PurchaseOrderDetail.GetAll(
                    d => d.POHdrID == poHdrID,
                    includeProperties: "Product"
                );

                if (purchaseOrderDetails == null || !purchaseOrderDetails.Any())
                {
                    // Handle the case where no Purchase Order Details are found
                    return NotFound();
                }

                // Email content
                string fromMail = purchaseOrderHeader.ApplicationUser.Email;
                string fromPassword = "gdrugqgfnryhgnnu";
                string toMail = purchaseOrderHeader.Supplier.Email;
                string subject = "Purchase Order Details";
                string body = ConstructEmailBody(purchaseOrderHeader, purchaseOrderDetails);

                // Send email
                using (var message = new MailMessage(fromMail, toMail))
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient("smtp.gmail.com")) // Update with SMTP server address
                    {
                        smtpClient.Port = 587; // Update with SMTP port
                        smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                        smtpClient.EnableSsl = true;

                        smtpClient.Send(message);
                    }
                }

                return Json(new { success = true, message = "Email sent successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Failed to send email: {ex.Message}" });
            }
        }

        // Method to construct the email body using purchase order data
        private string ConstructEmailBody(PurchaseOrderHeader purchaseOrderHeader, IEnumerable<PurchaseOrderDetail> purchaseOrderDetails)
        {
            
            string body = $@"
        <h2>Purchase Order Details</h2>
        <p><strong>Sender:</strong> {purchaseOrderHeader.ApplicationUser.Email}</p>
        <p><strong>Sender Location:</strong> {purchaseOrderHeader.ApplicationUser.Location.LocationAddress}</p>
        <p><strong>Supplier:</strong> {purchaseOrderHeader.Supplier.SupplierName}</p>
        <p><strong>Supplier Location:</strong> {purchaseOrderHeader.Location.LocationAddress}</p>
        <p><strong>Status:</strong> {purchaseOrderHeader.Status.StatusDescription}</p>
        <p><strong>PO Date:</strong> {purchaseOrderHeader.PODate}</p>
        <p><strong>TotalAmount:</strong> {purchaseOrderHeader.TotalAmount}</p>
        <h3>Products:</h3>
        <ul>";

            foreach (var detail in purchaseOrderDetails)
            {
                body += $@"
            <li>
                <strong>Product Name:</strong> {detail.Product.ProductName}<br>
                <strong>Unit Price:</strong> {detail.UnitPrice}<br>
                <strong>Quantity:</strong> {detail.QuantityInOrder}<br>
                <strong>Unit Of Measurement:</strong> {detail.UnitOfMeasurement}<br>
                <strong>Is VAT Exclusive:</strong> {(detail.IsVATExclusive ? "Yes" : "No" )}<br>
                <strong>Subtotal:</strong> {detail.Subtotal}<br>
            </li>";
            }

            body += "</ul>";

            return body;
        }   

        [HttpPost]
        public IActionResult CancelPO(int poId)
        {
            try
            {
                _purchaseOrderService.CancelPurchaseOrder(poId);

                return Json(new { success = true, message = "Purchase order cancelled successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetSupplierLocation(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierLocationById(supplierId);
            if (supplier == null || supplier.LocationID == null)
            {
                return NotFound("Supplier location not found.");
            }

            return Ok(supplier.LocationID);
        }

        [HttpGet]
        public IActionResult GetLocationAddress(int locationId)
        {
            var location = _locationRepository.GetLocationById(locationId);
            if (location == null)
            {
                return NotFound("Location not found.");
            }

            return Ok(location.LocationAddress);
        }



        [HttpGet]
        public IActionResult GetSupplierDetails(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierLocationById(supplierId);
            if (supplier == null)
            {
                return NotFound("Supplier not found.");
            }

            return Ok(new
            {
                LocationAddress = supplier.Location.LocationAddress,
            });
        }


        [HttpGet]
        public IActionResult GetUnitPrice(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product.UnitPrice);
        }

        [HttpGet]
        public IActionResult GetUnitOfMeasurement(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product.UnitOfMeasurement);
        }


        [HttpGet]
        public IActionResult GetProductDetails(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(new
            {
                UnitPrice = product.UnitPrice,
                UnitOfMeasurement = product.UnitOfMeasurement,
            });
        }         

        private string GetCurrentUserId()
        {
            // Get the current HttpContext
            var httpContext = _httpContextAccessor.HttpContext;

            // Get the user's identity
            var userIdentity = httpContext.User.Identity;

            // Check if the user is authenticated
            if (userIdentity.IsAuthenticated)
            {
                // Get the user's ID
                var user = _userManager.GetUserAsync(httpContext.User).Result; // Ensure synchronous execution
                return user?.Id;
            }

            // Return null if user is not authenticated
            return null;
        }

        // GET: /Admin/AddPurchase/CreatePR
        public IActionResult CreatePR()
        {
            // Fetch all products and convert them to SelectListItem objects
            var productList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            });

            // Fetch all suppliers and convert them to SelectListItem objects
            var supplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            });

            // Fetch all locations and convert them to SelectListItem objects
            var locationList = _unitOfWork.Location.GetAll().Select(l => new SelectListItem
            {
                Value = l.LocationID.ToString(),
                Text = l.LocationAddress
            });

            // Fetch all statuses and convert them to SelectListItem objects
            var statusList = _unitOfWork.Status.GetAll().Select(s => new SelectListItem
            {
                Value = s.StatusID.ToString(),
                Text = s.StatusDescription
            });

            // Get the current user's ID
            string currentUserId = GetCurrentUserId();

            // Create a new instance of PRTransactionVM with dropdown lists and default values
            var viewModel = new PRTransactionVM
            {
                ProductList = productList.ToList(),
                SupplierList = supplierList.ToList(),
                LocationList = locationList.ToList(),
                StatusList = statusList.ToList(),
                PurchaseRequisitionHeader = new PurchaseRequisitionHeader
                {
                    ApplicationUserId = currentUserId // Assign the current user's ID to ApplicationUserId
                },
                PurchaseRequisitionDetail = new PurchaseRequisitionDetail()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePR(PRTransactionVM viewModel)
        {
            if (ModelState.IsValid)
            {
                // Compute subtotal before saving
                viewModel.PurchaseRequisitionDetail.Subtotal =
                    viewModel.PurchaseRequisitionDetail.UnitPrice *
                    viewModel.PurchaseRequisitionDetail.QuantityInOrder;

                // Set the status of the new Purchase Requisition to "Pending"
                var pendingStatus = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Pending);
                if (pendingStatus == null)
                {
                    ModelState.AddModelError("", "Pending status not found in database.");
                    PopulateDropdowns(viewModel);
                    return View(viewModel); // Return to the view with validation error
                }

                viewModel.PurchaseRequisitionHeader.Status = pendingStatus;

                // Set the ApplicationUserId to the current user's ID
                viewModel.PurchaseRequisitionHeader.ApplicationUserId = GetCurrentUserId();

                // Add the new PurchaseRequisitionHeader to the database
                _unitOfWork.PurchaseRequisitionHeader.Add(viewModel.PurchaseRequisitionHeader);
                _unitOfWork.Save();

                // Assign the newly generated header ID to the detail
                viewModel.PurchaseRequisitionDetail.PRHdrID = viewModel.PurchaseRequisitionHeader.PRHdrID;

                // Add the new PurchaseRequisitionDetail to the database
                _unitOfWork.PurchaseRequisitionDetail.Add(viewModel.PurchaseRequisitionDetail);

                // Update the header's TotalAmount with the subtotal of the detail
                viewModel.PurchaseRequisitionHeader.TotalAmount = viewModel.PurchaseRequisitionDetail.Subtotal;

                // Save the updated TotalAmount in PurchaseRequisitionHeader
                _unitOfWork.PurchaseRequisitionHeader.Update(viewModel.PurchaseRequisitionHeader);
                _unitOfWork.Save();

                // Create transaction log entries for each product in the PR
                foreach (var detail in viewModel.PurchaseRequisitionHeader.PurchaseRequisitionDetail)
                {
                    var transactionLog = new TransactionLogs
                    {
                        ApplicationUserId = viewModel.PurchaseRequisitionHeader.ApplicationUserId,
                        StatusID = viewModel.PurchaseRequisitionHeader.StatusID,
                        PRHdrID = viewModel.PurchaseRequisitionHeader.PRHdrID,
                        ProductID = detail.ProductID,
                        Quantity = detail.QuantityInOrder,
                        TransDate = viewModel.PurchaseRequisitionHeader.PRDate
                    };

                    _unitOfWork.TransactionLogs.Add(transactionLog);
                }

                _unitOfWork.Save();

                TempData["CreatePRSuccess"] = "Purchase Requisition created successfully!";
                return RedirectToAction("PRDList", "PRTransaction");
            }

            // If model state is invalid, repopulate dropdown lists and return to the view with validation errors
            PopulateDropdowns(viewModel);
            return View(viewModel);
        }


        private void PopulateDropdowns(PRTransactionVM viewModel)
        {
            viewModel.ProductList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            }).ToList();

            viewModel.SupplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            }).ToList();

            viewModel.LocationList = _unitOfWork.Location.GetAll().Select(l => new SelectListItem
            {
                Value = l.LocationID.ToString(),
                Text = l.LocationAddress
            }).ToList();

            viewModel.StatusList = _unitOfWork.Status.GetAll().Select(s => new SelectListItem
            {
                Value = s.StatusID.ToString(),
                Text = s.StatusDescription
            }).ToList();
        }

        public IActionResult ViewPR(int prdId)
        {
            // Fetch the PRDetail based on PRDtlID
            var purchaseRequisitionDetail = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == prdId, includeProperties: "Product");

            if (purchaseRequisitionDetail == null)
            {
                // Handle the case where PRDetail is not found
                return NotFound();
            }

            // Fetch the corresponding PRHeader based on PRHdrID in the PRDetail
            var purchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == purchaseRequisitionDetail.PRHdrID, includeProperties: "Supplier,Location,Status");

            if (purchaseRequisitionHeader == null)
            {
                // Handle the case where PRHeader is not found
                return NotFound();
            }

            string currentUserId = GetCurrentUserId();

            // Create a view model to hold PRHeader, PRDetail, and user who created them
            var prTransactionVM = new PRTransactionVM
            {
                PurchaseRequisitionHeader = purchaseRequisitionHeader,
                PurchaseRequisitionDetail = purchaseRequisitionDetail,
                CreatedByUserId = currentUserId
            };

            return View(prTransactionVM);
        }

        public IActionResult EditPR(int prdId)
        {
            // Fetch the PRDetail based on PRDtlID
            var purchaseRequisitionDetail = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == prdId, includeProperties: "Product");

            if (purchaseRequisitionDetail == null)
            {
                // Handle the case where PRDetail is not found
                return NotFound();
            }

            // Fetch the corresponding PRHeader based on PRHdrID in the PRDetail
            var purchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == purchaseRequisitionDetail.PRHdrID, includeProperties: "Supplier,Location,Status");

            if (purchaseRequisitionHeader == null)
            {
                // Handle the case where PRHeader is not found
                return NotFound();
            }

            // Populate Supplier dropdown list
            var suppliers = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            }).ToList();

            // Populate Location dropdown list
            var locations = _unitOfWork.Location.GetAll().Select(l => new SelectListItem
            {
                Value = l.LocationID.ToString(),
                Text = l.LocationAddress
            }).ToList();

            // Populate Product dropdown list
            var products = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            }).ToList();

            // Create a view model to hold both PRHeader, PRDetail, and dropdown lists
            var prTransactionVM = new PRTransactionVM
            {
                PurchaseRequisitionHeader = purchaseRequisitionHeader,
                PurchaseRequisitionDetail = purchaseRequisitionDetail,
                SupplierList = suppliers,
                LocationList = locations,
                ProductList = products
            };

            return View(prTransactionVM);
        }

        // Update the Purchase Requisition details
        [HttpPost]
        public IActionResult EditPR(PRTransactionVM model)
        {
            // Fetch the existing Purchase Requisition Header from the database
            var existingHeader = _unitOfWork.PurchaseRequisitionHeader.Get(h => h.PRHdrID == model.PurchaseRequisitionHeader.PRHdrID, includeProperties: "Status");

            if (existingHeader == null)
            {
                return NotFound();
            }

            // Check if the Purchase Requisition has been cancelled
            if (existingHeader.Status.StatusDescription == StaticDetails.Status_Cancelled)
            {
                // If the Purchase Requisition is already cancelled, prevent the edit operation
                TempData["ErrorMessage"] = "Cannot edit a Purchase Requisition that has been cancelled.";
                PopulateDropdownLists(model);
                return View(model); // Redirect to PRDList page or any other appropriate action
            }

            // Check if the Purchase Requisition has been approved
            if (existingHeader.Status.StatusDescription == StaticDetails.Status_Approved)
            {
                // If the Purchase Requisition is already approved, set a TempData notification message
                TempData["ErrorMessage"] = "Cannot edit a Purchase Requisition that has already been approved.";
                PopulateDropdownLists(model);
                return View(model); // Redirect to PRDList page
            }                       

            if (ModelState.IsValid)
            {
                // Fetch the existing Purchase Requisition Detail from the database
                var existingDetail = _unitOfWork.PurchaseRequisitionDetail.Get(d => d.PRDtlID == model.PurchaseRequisitionDetail.PRDtlID);

                if (existingDetail == null)
                {
                    return NotFound();
                }

                // Update the fields with the new values from the form
                existingHeader.SupplierID = model.PurchaseRequisitionHeader.SupplierID;
                existingHeader.LocationID = model.PurchaseRequisitionHeader.LocationID;
                // Update other header fields as needed...

                existingDetail.ProductID = model.PurchaseRequisitionDetail.ProductID;
                existingDetail.UnitPrice = model.PurchaseRequisitionDetail.UnitPrice;
                existingDetail.UnitOfMeasurement = model.PurchaseRequisitionDetail.UnitOfMeasurement;
                existingDetail.QuantityInOrder = model.PurchaseRequisitionDetail.QuantityInOrder;

                // Update the subtotal based on the new UnitPrice and QuantityInOrder
                existingDetail.Subtotal = existingDetail.UnitPrice * existingDetail.QuantityInOrder;

                // Update the entities in the database
                _unitOfWork.PurchaseRequisitionHeader.Update(existingHeader);
                _unitOfWork.PurchaseRequisitionDetail.Update(existingDetail);
                _unitOfWork.Save();

                // Update the TotalAmount in the header by summing up the subtotals of all details
                var totalAmount = _unitOfWork.PurchaseRequisitionDetail
                    .GetAll(d => d.PRHdrID == existingHeader.PRHdrID)
                    .Sum(d => d.Subtotal);

                existingHeader.TotalAmount = totalAmount;
                _unitOfWork.PurchaseRequisitionHeader.Update(existingHeader);
                _unitOfWork.Save();

                TempData["PRSuccessMessage"] = "Purchase Requisition updated successfully.";
                return RedirectToAction("PRDList", "PRTransaction");

            }
            else
            {
                // If the model state is not valid, return the edit view with validation errors
                // Repopulate dropdown lists with data
                PopulateDropdownLists(model);
                return View(model);
            }
        }

        private void PopulateDropdownLists(PRTransactionVM model)
        {
            model.SupplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            }).ToList();

            model.LocationList = _unitOfWork.Location.GetAll().Select(l => new SelectListItem
            {
                Value = l.LocationID.ToString(),
                Text = l.LocationAddress
            }).ToList();

            model.ProductList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            }).ToList();
        }

        //Adding PR Details
        public IActionResult PRDetails(int prId)
        {
            // Fetch purchase requisition header
            var prHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == prId, includeProperties: "Supplier,Location,Status");

            // Fetch all products and convert them to SelectListItem objects
            var productList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(), // Assuming Id is the property you want to use as the value
                Text = p.ProductName // Assuming Name is the property you want to display as the text
            });

            // Create a new PRTransactionVM with the fetched PRHeader and product list
            PRTransactionVM prTransactionVM = new PRTransactionVM
            {
                PurchaseRequisitionHeader = prHeader,
                ProductList = productList,
                // Initialize an empty PRDetail
                PurchaseRequisitionDetail = new PurchaseRequisitionDetail { PRHdrID = prId } // Assuming PRHdrID is the property representing the foreign key
            };

            return View(prTransactionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PRDetails(PRTransactionVM prTransactionVM)
        {
            if (ModelState.IsValid)
            {
                // Compute subtotal before saving
                prTransactionVM.PurchaseRequisitionDetail.Subtotal =
                    prTransactionVM.PurchaseRequisitionDetail.UnitPrice *
                    prTransactionVM.PurchaseRequisitionDetail.QuantityInOrder;

                // Adding a new detail
                _unitOfWork.PurchaseRequisitionDetail.Add(prTransactionVM.PurchaseRequisitionDetail);

                // Update TotalAmount in PurchaseRequisitionHeader
                // Retrieve the PR Header based on its ID
                var requisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(header => header.PRHdrID == prTransactionVM.PurchaseRequisitionDetail.PRHdrID);

                if (requisitionHeader != null)
                {
                    // Fetch all related PurchaseRequisitionDetails based on PRHdrID
                    var requisitionDetails = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");

                    // Calculate the total amount by summing up the subtotals of all details
                    decimal totalAmount = requisitionDetails.Sum(detail => detail.Subtotal);

                    // Update the TotalAmount property in the PurchaseRequisitionHeader
                    requisitionHeader.TotalAmount = totalAmount;

                    // Update the PR Header in the database
                    _unitOfWork.PurchaseRequisitionHeader.Update(requisitionHeader);

                    // Save changes to the database
                    _unitOfWork.Save();
                }

                return RedirectToAction("PRDList");
            }

            return View(prTransactionVM);
        }

        #region API CALLS             

        //Retrieving the PR Headers and PR Details
        [HttpGet]
        public IActionResult GetAllPRDetails()
        {
            // Fetch all PurchaseRequisitionHeaders including related entities
            var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status,ApplicationUser");

            // Fetch all PurchaseRequisitionDetails including related entities
            var details = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");

            // Project the details and headers into an anonymous type
            var detailsWithStatus = details.Select(detail => new
            {
                detail.PRDtlID,
                detail.PRHdrID,
                PurchaseRequisitionHeader = new
                {
                    detail.PurchaseRequisitionHeader.Supplier.SupplierName,
                    detail.PurchaseRequisitionHeader.Location.LocationAddress,
                    detail.PurchaseRequisitionHeader.Status.StatusDescription,
                    detail.PurchaseRequisitionHeader.TotalAmount,
                    detail.PurchaseRequisitionHeader.PRDate,
                    ApplicationUserEmail = detail.PurchaseRequisitionHeader.ApplicationUser.Email
                },
                detail.Product.ProductName,
                detail.UnitPrice,
                detail.UnitOfMeasurement,
                detail.QuantityInOrder,
                detail.Subtotal,
            }); ;

            // Return the result as JSON
            return Json(new { data = detailsWithStatus });
        }


        //Retrieving the PO Headers and PO Details
        [HttpGet]
        public IActionResult GetAllPODetails()
        {
            // Fetch all PurchaseOrderHeaders including related entities
            var headers = _unitOfWork.PurchaseOrderHeader.GetAll(includeProperties: "Supplier,Location,Status,ApplicationUser");

            // Fetch all PurchaseOrderDetails including related entities
            var details = _unitOfWork.PurchaseOrderDetail.GetAll(includeProperties: "Product,PurchaseOrderHeader");

            // Project the details and headers into an anonymous type
            var detailsWithStatus = details.Select(detail => new
            {
                detail.PODtlID,
                detail.POHdrID,
                PurchaseOrderHeader = new
                {
                    detail.PurchaseOrderHeader.Supplier.SupplierName,
                    detail.PurchaseOrderHeader.Location.LocationAddress,
                    detail.PurchaseOrderHeader.Status.StatusDescription,
                    detail.PurchaseOrderHeader.TotalAmount,
                    detail.PurchaseOrderHeader.PODate,
                    ApplicationUserEmail = detail.PurchaseOrderHeader.ApplicationUser.Email
                },
                detail.Product.ProductName,
                detail.UnitPrice,
                detail.UnitOfMeasurement,
                detail.QuantityInOrder,
                detail.Subtotal,
            });

            // Return the result as JSON
            return Json(new { data = detailsWithStatus });
        }

        //[HttpGet]
        //public IActionResult GetAllTransactionLogs()
        //{         
        //    // Fetch all PurchaseRequisitionHeaders including related entities
        //    var headers = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Status,ApplicationUser");

        //    // Fetch all PurchaseRequisitionDetails including related entities
        //    var details = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product,PurchaseRequisitionHeader");

        //    // Project the details and headers into an anonymous type
        //    var detailsWithStatus = details.Select(detail => new
        //    {
        //        detail.PRDtlID,
        //        detail.PRHdrID,
        //        PurchaseRequisitionHeader = new
        //        {
        //            detail.PurchaseRequisitionHeader.Supplier.SupplierName,
        //            detail.PurchaseRequisitionHeader.Location.LocationAddress,
        //            detail.PurchaseRequisitionHeader.Status.StatusDescription,
        //            detail.PurchaseRequisitionHeader.TotalAmount,
        //            detail.PurchaseRequisitionHeader.PRDate,
        //            ApplicationUserEmail = detail.PurchaseRequisitionHeader.ApplicationUser?.Email // Use ?. to handle null ApplicationUser
        //        },
        //        detail.Product.ProductName,
        //        detail.UnitPrice,
        //        detail.UnitOfMeasurement,
        //        detail.QuantityInOrder,
        //        detail.Subtotal,
        //    });

        //    // Return the result as JSON
        //    return Json(new { data = detailsWithStatus });
        //}


        #endregion
    }
}


//[HttpPost]
//public IActionResult SendPurchaseOrderEmail()
//{
//    try
//    {
//        // Your code to send the email
//        string fromMail = "darnred7@gmail.com";
//        string fromPassword = "gdrugqgfnryhgnnu";

//        MailMessage message = new MailMessage();
//        message.From = new MailAddress(fromMail);
//        message.Subject = "Test";
//        message.To.Add(new MailAddress("flores.klarke@gmail.com"));
//        message.Body = "<html><body> Test </html></body>";
//        message.IsBodyHtml = true;

//        var smtpClient = new SmtpClient("smtp.gmail.com")
//        {
//            Port = 587,
//            Credentials = new NetworkCredential(fromMail, fromPassword),
//            EnableSsl = true,
//        };

//        smtpClient.Send(message);

//        return Json(new { success = true, message = "Email sent successfully." });
//    }
//    catch (Exception ex)
//    {
//        return Json(new { success = false, message = $"Failed to send email: {ex.Message}" });
//    }
//}