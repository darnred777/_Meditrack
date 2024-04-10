using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Services;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class PurchaseRequisitionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
		private readonly PurchaseOrderService _purchaseOrderService;
		//private readonly PurchaseDetailService _purchaseDetailService;

		//public PurchaseRequisitionController(IUnitOfWork unitOfWork, PurchaseOrderService purchaseOrderService, PurchaseDetailService purchaseDetailService)
		public PurchaseRequisitionController(IUnitOfWork unitOfWork, PurchaseOrderService purchaseOrderService)
		{
            {
                _unitOfWork = unitOfWork;
                _purchaseOrderService = purchaseOrderService;
                //_purchaseDetailService = purchaseDetailService;
            }
        }

        /* Purchase Requisition Header*/

        public IActionResult ManagePurchaseRequisitionHeader()
        {
            List<PurchaseRequisitionHeader> objPurchaseRequisitionHeaderList = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status").ToList();

            return View(objPurchaseRequisitionHeaderList);
        }

        public IActionResult ManagePurchaseRequisitionDetail()
        {
            List<PurchaseRequisitionDetail> objPurchaseRequisitionDetailList = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "PurchaseRequisitionHeader,Product").ToList();

            return View(objPurchaseRequisitionDetailList);
        }



        ///////

        public IActionResult ManagePurchaseOrderHeader()
        {
            List<PurchaseOrderHeader> objPurchaseOrderHeaderList = _unitOfWork.PurchaseOrderHeader.GetAll(includeProperties: "Supplier,Location,Status").ToList();

            return View(objPurchaseOrderHeaderList);
        }

        public IActionResult ManagePurchaseOrderDetail()
        {
            List<PurchaseOrderDetail> objPurchaseOrderDetailList = _unitOfWork.PurchaseOrderDetail.GetAll(includeProperties: "Product").ToList();

            return View(objPurchaseOrderDetailList);
        }

        public IActionResult CreatePurchaseRequisitionDetail(int? PRHdrID)
        {
            //IEnumerable<SelectListItem> LocationList =

            //ViewBag.LocationList = LocationList;
            //ViewData["LocationList"] = LocationList;
            PurchaseRequisitionHeaderVM purchaseRequisitionHeaderVM = new()
            {
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                }),

                LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LocationAddress,
                    Value = u.LocationID.ToString()
                }),

                StatusList = _unitOfWork.Status.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StatusDescription,
                    Value = u.StatusID.ToString()
                }),

                PurchaseRequisitionHeader = new PurchaseRequisitionHeader()
            };

            if (PRHdrID == null || PRHdrID == 0)
            {
                //create
                return View(purchaseRequisitionHeaderVM);
            }
            else
            {
                purchaseRequisitionHeaderVM.PurchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == PRHdrID);

                //update
                return View(purchaseRequisitionHeaderVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePurchaseRequisitionDetail(PurchaseRequisitionHeaderVM purchaseRequisitionHeaderVM)
        {
            if (ModelState.IsValid)
            {
                if (purchaseRequisitionHeaderVM.PurchaseRequisitionHeader.PRHdrID == 0)
                {
                    // Adding a new user
                    _unitOfWork.PurchaseRequisitionHeader.Add(purchaseRequisitionHeaderVM.PurchaseRequisitionHeader);
                }
                else
                {
                    // Updating an existing user
                    _unitOfWork.PurchaseRequisitionHeader.Update(purchaseRequisitionHeaderVM.PurchaseRequisitionHeader);
                }

                _unitOfWork.Save();

                //_purchaseOrderService.PopulatePurchaseOrderFromRequisition(purchaseRequisitionHeaderVM.PurchaseRequisitionHeader.PRHdrID);

                return RedirectToAction("ManagePurchaseRequisitionHeader");
            }
            else
            {
                purchaseRequisitionHeaderVM.SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                });

                purchaseRequisitionHeaderVM.LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LocationAddress,
                    Value = u.LocationID.ToString()
                });

                purchaseRequisitionHeaderVM.StatusList = _unitOfWork.Status.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StatusDescription,
                    Value = u.StatusID.ToString()
                });

                return View(purchaseRequisitionHeaderVM);
            }

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult UpsertPurchaseRequisitionHeader(int? PRHdrID)
        {        
            PurchaseRequisitionHeaderVM purchaseRequisitionHeaderVM = new()
            {
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                }),

                LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LocationAddress,
                    Value = u.LocationID.ToString()
                }),

                StatusList = _unitOfWork.Status.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StatusDescription,
                    Value = u.StatusID.ToString()
                }),

                PurchaseRequisitionHeader = new PurchaseRequisitionHeader()
            };

            if (PRHdrID == null || PRHdrID == 0)
            {
                //create
                return View(purchaseRequisitionHeaderVM);
            }
            else
            {
                purchaseRequisitionHeaderVM.PurchaseRequisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == PRHdrID);

                //update
                return View(purchaseRequisitionHeaderVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPurchaseRequisitionHeader(PurchaseRequisitionHeaderVM purchaseRequisitionHeaderVM)
        {
            if (ModelState.IsValid)
            {
                if (purchaseRequisitionHeaderVM.PurchaseRequisitionHeader.PRHdrID == 0)
                {
                    // Adding a new user
                    _unitOfWork.PurchaseRequisitionHeader.Add(purchaseRequisitionHeaderVM.PurchaseRequisitionHeader);
                }
                else
                {
                    // Updating an existing user
                    _unitOfWork.PurchaseRequisitionHeader.Update(purchaseRequisitionHeaderVM.PurchaseRequisitionHeader);
                }

                _unitOfWork.Save();

                _purchaseOrderService.PopulatePurchaseOrderFromRequisition(purchaseRequisitionHeaderVM.PurchaseRequisitionHeader.PRHdrID);

                return RedirectToAction("ManagePurchaseRequisitionHeader");
            }
            else
            {
                purchaseRequisitionHeaderVM.SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                });

                purchaseRequisitionHeaderVM.LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LocationAddress,
                    Value = u.LocationID.ToString()
                });

                purchaseRequisitionHeaderVM.StatusList = _unitOfWork.Status.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StatusDescription,
                    Value = u.StatusID.ToString()
                });

                return View(purchaseRequisitionHeaderVM);
            }

        }

        public IActionResult DeletePurchaseRequisitionHeader(int? PRHdrID)
        {
            if (PRHdrID == null || PRHdrID == 0)
            {
                return NotFound();
            }
            PurchaseRequisitionHeader? purchaseRequisitionHeaderFromDb = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == PRHdrID);

            if (purchaseRequisitionHeaderFromDb == null)
            {
                return NotFound();
            }
            return View(purchaseRequisitionHeaderFromDb);
        }

        [HttpPost, ActionName("DeletePurchaseRequisitionHeader")]
        public IActionResult DeletePOSTPurchaseRequisitionHeader(int? PRHdrID)
        {
            PurchaseRequisitionHeader? obj = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == PRHdrID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.PurchaseRequisitionHeader.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManagePurchaseRequisitionHeader");
        }



		/* Purchase Requisition Detail */

		public IActionResult UpsertPurchaseRequisitionDetail(int? PRDtlID)
		{
			PurchaseRequisitionDetailVM purchaseRequisitionDetailVM = new()
			{
				HeaderIdList = _unitOfWork.PurchaseRequisitionHeader.GetAll()
								.Select(header => new SelectListItem
								{
									Value = header.PRHdrID.ToString(),
									Text = header.PRHdrID.ToString() // You can use other properties here like header description, etc.
								}),

				ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
				{
					Text = u.ProductName,
					Value = u.ProductID.ToString()
				}),

				PurchaseRequisitionDetail = new PurchaseRequisitionDetail()
			};

			if (PRDtlID == null || PRDtlID == 0)
			{
				//create
				return View(purchaseRequisitionDetailVM);
			}
			else
			{
				purchaseRequisitionDetailVM.PurchaseRequisitionDetail = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == PRDtlID);

				//update
				return View(purchaseRequisitionDetailVM);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpsertPurchaseRequisitionDetail(PurchaseRequisitionDetailVM purchaseRequisitionDetailVM)
		{
			if (ModelState.IsValid)
			{
				// Compute subtotal before saving
				purchaseRequisitionDetailVM.PurchaseRequisitionDetail.Subtotal =
				purchaseRequisitionDetailVM.PurchaseRequisitionDetail.UnitPrice *
				purchaseRequisitionDetailVM.PurchaseRequisitionDetail.QuantityInOrder;

				if (purchaseRequisitionDetailVM.PurchaseRequisitionDetail.PRDtlID == 0)
				{
					// Adding a new detail
					_unitOfWork.PurchaseRequisitionDetail.Add(purchaseRequisitionDetailVM.PurchaseRequisitionDetail);

				}
				else
				{
					// Updating an existing detail
					_unitOfWork.PurchaseRequisitionDetail.Update(purchaseRequisitionDetailVM.PurchaseRequisitionDetail);
				}

				// Update TotalAmount in PurchaseRequisitionHeader
				//var requisitionHeader = _unitOfWork.PurchaseRequisitionHeader.Get(header => header.PRHdrID == purchaseRequisitionDetailVM.PurchaseRequisitionDetail.PRHdrID);
				//if (requisitionHeader != null)
				//{
				//	requisitionHeader.TotalAmount += purchaseRequisitionDetailVM.PurchaseRequisitionDetail.Subtotal;
				//}

				//// Update TotalAmount in PurchaseOrderHeader
				//var orderHeader = _unitOfWork.PurchaseOrderHeader.Get(header => header.POHdrID == requisitionHeader.PRHdrID);
				//if (orderHeader != null)
				//{
				//	orderHeader.TotalAmount += purchaseRequisitionDetailVM.PurchaseRequisitionDetail.Subtotal;
				//}


				_unitOfWork.Save();
				//_purchaseDetailService.PopulatePurchaseOrderDetailFromRequisitionDetail(purchaseRequisitionDetailVM.PurchaseRequisitionDetail.PRDtlID);

				return RedirectToAction("ManagePurchaseRequisitionDetail");
			}
			else
			{
				purchaseRequisitionDetailVM.ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
				{
					Text = u.ProductName,
					Value = u.ProductID.ToString()
				});

				purchaseRequisitionDetailVM.HeaderIdList = _unitOfWork.PurchaseRequisitionHeader.GetAll().Select(u => new SelectListItem
				{
					Text = u.PRHdrID.ToString(),
					Value = u.PRHdrID.ToString()
				});

				return View(purchaseRequisitionDetailVM);
			}
		}

		public IActionResult DeletePurchaseRequisitionDetail(int? PRDtlID)
        {
            if (PRDtlID == null || PRDtlID == 0)
            {
                return NotFound();
            }
            PurchaseRequisitionDetail? purchaseRequisitionDetailFromDb = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == PRDtlID);

            if (purchaseRequisitionDetailFromDb == null)
            {
                return NotFound();
            }
            return View(purchaseRequisitionDetailFromDb);
        }


        [HttpPost, ActionName("DeletePurchaseRequisitionDetail")]
        public IActionResult DeletePOSTPurchaseRequisitionDetail(int? PRDtlID)
        {
            PurchaseRequisitionDetail? obj = _unitOfWork.PurchaseRequisitionDetail.Get(u => u.PRDtlID == PRDtlID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.PurchaseRequisitionDetail.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManagePurchaseRequisitionDetail");
        }


    }
}