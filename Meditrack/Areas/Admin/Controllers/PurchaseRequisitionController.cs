using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseRequisitionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public PurchaseRequisitionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult ManagePurchaseRequisitionHeader()
        {
            List<PurchaseRequisitionHeader> objPurchaseRequisitionHeaderList = _unitOfWork.PurchaseRequisitionHeader.GetAll(includeProperties: "Supplier,Location,Status").ToList();

            return View(objPurchaseRequisitionHeaderList);
        }

        public IActionResult ManagePurchaseRequisitionDetail()
        {
            List<PurchaseRequisitionDetail> objPurchaseRequisitionDetailList = _unitOfWork.PurchaseRequisitionDetail.GetAll(includeProperties: "Product").ToList();

            return View(objPurchaseRequisitionDetailList);
        }

        //ViewBag
        public IActionResult UpsertPurchaseRequisitionHeader(int? PRHdrID)
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
        public IActionResult UpsertPurchaseRequisition(PurchaseRequisitionHeaderVM purchaseRequisitionHeaderVM)
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

                return RedirectToAction("ManagePurchaseRequisition");
            }
            else
            {
                purchaseRequisitionHeaderVM.SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                });
                return View(purchaseRequisitionHeaderVM);
            }
        }


        //[HttpPost]
        //public IActionResult UpsertUserAccount(UserVM userVM) 
        //{   

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.User.Add(userVM.User);
        //        _unitOfWork.Save();

        //        return RedirectToAction("ManageUserAccount");

        //    } else
        //    {
        //        //UserVM userVM = new()
        //        //{
        //        userVM.LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.LocationAddress,
        //            Value = u.LocationID.ToString()
        //        });
        //        return View(userVM);
        //    }           
        //}

        //public IActionResult EditUserAccount(int? UserID)
        //{
        //    if (UserID == null || UserID == 0)
        //    {
        //        return NotFound();
        //    }
        //    User? userFromDb = _unitOfWork.User.Get(u => u.UserID == UserID);
        //    //User? userFromDb2 = _db.User.Where(u => u.UserID == UserID).FirstOrDefault();

        //    if (userFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userFromDb);
        //}

        //[HttpPost]
        //public IActionResult EditUserAccount(User obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.User.Update(obj);
        //        _unitOfWork.Save();

        //        return RedirectToAction("ManageUserAccount");
        //    }
        //    return View();
        //}

        public IActionResult DeletePurchaseRequisition(int? PRHdrID)
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

        [HttpPost, ActionName("DeletePurchaseRequisition")]
        public IActionResult DeletePOSTPurchaseRequisition(int? PRHdrID)
        {
            PurchaseRequisitionHeader? obj = _unitOfWork.PurchaseRequisitionHeader.Get(u => u.PRHdrID == PRHdrID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.PurchaseRequisitionHeader.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManagePurchaseRequisition");
        }
    }
}