using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer)]

    public class MonitoringController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MonitoringController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ManageMonitoring()
        {
            List<Monitoring> objMonitoringList = _unitOfWork.Monitoring.GetAll(includeProperties: "Product,Supplier").ToList();

            return View(objMonitoringList);
        }

        public IActionResult UpsertMonitoring(int? monitoringID)
        {
            MonitoringVM monitoringVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ProductName,
                    Value = u.ProductID.ToString()
                }),

                SupplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
                {
                    Value = s.SupplierID.ToString(),
                    Text = s.SupplierName
                    //Text = $"{s.SupplierName} - {s.Location.LocationAddress}"
                }),

                Monitoring = new Monitoring()
            };

            if (monitoringID == null || monitoringID == 0)
            {
                //create
                return View(monitoringVM);
            }
            else
            {
                monitoringVM.Monitoring = _unitOfWork.Monitoring.Get(u => u.MonitoringID == monitoringID);

                //update
                return View(monitoringVM);
            }
        }

        [HttpPost]
        public IActionResult UpsertMonitoring(MonitoringVM monitoringVM)
        {
            if (ModelState.IsValid)
            {

                // Calculate QuantityLacking
                monitoringVM.Monitoring.QuantityLacking = monitoringVM.Monitoring.QuantityExpected - monitoringVM.Monitoring.QuantityReceived;

                if (monitoringVM.Monitoring.MonitoringID == 0)
                {
                    // Adding a new user
                    _unitOfWork.Monitoring.Add(monitoringVM.Monitoring);
                }
                else
                {
                    // Updating an existing user
                    _unitOfWork.Monitoring.Update(monitoringVM.Monitoring);
                }

                _unitOfWork.Save();

                return RedirectToAction("ManageMonitoring");
            }
            else
            {

                monitoringVM.ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ProductName,
                    Value = u.ProductID.ToString()
                });

                monitoringVM.SupplierList = _unitOfWork.Supplier.GetAll().Select(s => new SelectListItem
                {                 
                    Value = s.SupplierID.ToString(),
                    Text = s.SupplierName
                    //Text = $"{s.SupplierName} - {s.Location.LocationAddress}"
                });
                return View(monitoringVM);
            }
        }

        public IActionResult DeleteMonitoring(int? MonitoringID)
        {
            if (MonitoringID == null || MonitoringID == 0)
            {
                return NotFound();
            }
            Monitoring? monitoringFromDb = _unitOfWork.Monitoring.Get(u => u.MonitoringID == MonitoringID);

            if (monitoringFromDb == null)
            {
                return NotFound();
            }
            return View(monitoringFromDb);
        }

        [HttpPost, ActionName("DeleteMonitoring")]
        public IActionResult DeletePOSTMonitoring(int? MonitoringID)
        {
            if (MonitoringID == null)
            {
                return NotFound();
            }

            var monitoring = _unitOfWork.Monitoring.Get(u => u.MonitoringID == MonitoringID);
            if (monitoring == null)
            {
                return NotFound();
            }

            _unitOfWork.Monitoring.Remove(monitoring);
            _unitOfWork.Save();
            TempData["monitorsuccess"] = "Supply deleted successfully.";

            return RedirectToAction("ManageMonitoring");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetMonitoring()
        {
            List<Monitoring> objMonitoringList = _unitOfWork.Monitoring.GetAll(includeProperties: "Product,Supplier").ToList();

            return Json(new { data = objMonitoringList });
        }

        #endregion
    }
}
