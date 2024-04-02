using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_InventoryOfficer)]

    public class LocationController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public LocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult ManageLocation()
        {
            //List<User> objUserList = _unitOfWork.User.GetAll().ToList();
            List<Location> objLocationList = _unitOfWork.Location.GetAll().ToList();
            //IEnumerable<SelectListItem> LocationList = _unitOfWork.Location.GetAll().Select();
            return View(objLocationList);
        }

        public IActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLocation(Location obj) 
        {
            
            _unitOfWork.Location.Add(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageLocation");
        }

        public IActionResult EditLocation(int? LocationID)
        {
            if (LocationID == null || LocationID == 0)
            {
                return NotFound();
            }
            Location? locationFromDb = _unitOfWork.Location.Get(u => u.LocationID == LocationID);
            //User? userFromDb2 = _db.User.Where(u => u.UserID == UserID).FirstOrDefault();

            if (locationFromDb == null)
            {
                return NotFound();
            }
            return View(locationFromDb);
        }

        [HttpPost]
        public IActionResult EditLocation(Location obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Location.Update(obj);
                _unitOfWork.Save();

                return RedirectToAction("ManageLocation");
            }
            return View();
        }

        public IActionResult DeleteLocation(int? LocationID)
        {
            if (LocationID == null || LocationID == 0)
            {
                return NotFound();
            }
            Location? locationFromDb = _unitOfWork.Location.Get(u => u.LocationID == LocationID);

            if (locationFromDb == null)
            {
                return NotFound();
            }
            return View(locationFromDb);
        }

        [HttpPost, ActionName("DeleteLocation")]
        public IActionResult DeletePOSTLocation(int? LocationID)
        {
            Location? obj = _unitOfWork.Location.Get(u => u.LocationID == LocationID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Location.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageLocation");
        }
    }
}
