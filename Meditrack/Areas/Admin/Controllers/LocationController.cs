﻿using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]

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

        public IActionResult UpsertLocation(int? locationID)
        {
            LocationVM locationVM = new()
            {
                Location = new Location()
                {
                    LocationType = "",
                    LocationAddress = ""
                }
            };

            if (locationID == null || locationID == 0)
            {
                // Create
                return View(locationVM);
            }
            else
            {
                locationVM.Location = _unitOfWork.Location.Get(u => u.LocationID == locationID);

                // Update
                return View(locationVM);
            }
        }

        [HttpPost]
        public IActionResult UpsertLocation(LocationVM locationVM)
        {
            if (ModelState.IsValid)
            {
                if (locationVM.Location.LocationID == 0)
                {
                    // Adding a new location
                    _unitOfWork.Location.Add(locationVM.Location);
                }
                else
                {
                    // Updating an existing location
                    // Get the existing location entity from the database
                    var existingLocation = _unitOfWork.Location.Get(u => u.LocationID == locationVM.Location.LocationID);

                    if (existingLocation == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing location entity with the new values
                    existingLocation.LocationType = locationVM.Location.LocationType;
                    existingLocation.LocationAddress = locationVM.Location.LocationAddress;

                    // Update the location entity in the database
                    _unitOfWork.Location.Update(existingLocation);
                }

                _unitOfWork.Save();

                return RedirectToAction("ManageLocation");
            }
            else
            {
                return View(locationVM);
            }
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
            if (LocationID == null)
            {
                return NotFound();
            }

            if (_unitOfWork.Location.ExistsWithRelation(LocationID.Value))
            {
                TempData["Error"] = "You can't delete this Location because it has associated Data Existed";
                return RedirectToAction("DeleteLocation", new { LocationID });
            }

            Location location = _unitOfWork.Location.Get(u => u.LocationID == LocationID);

            if (location == null)
            {
                return NotFound();
            }

            _unitOfWork.Location.Remove(location);
            _unitOfWork.Save();

            TempData["LocationSuccess"] = "Location deleted successfully.";
            return RedirectToAction("ManageLocation");
        }


        //[HttpPost, ActionName("DeleteLocation")]
        //public IActionResult DeletePOSTLocation(int? LocationID)
        //{
        //    Location? obj = _unitOfWork.Location.Get(u => u.LocationID == LocationID);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Location.Remove(obj);
        //    _unitOfWork.Save();

        //    return RedirectToAction("ManageLocation");
        //}
    }
}
