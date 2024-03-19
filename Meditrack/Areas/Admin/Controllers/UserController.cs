using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult ManageUserAccount()
        {
            List<User> objUserList = _unitOfWork.User.GetAll().ToList();
            return View(objUserList);
        }

        public IActionResult AddNewUserAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewUserAccount(User obj) //, IFormFile profilePicture
        {
            //if (ModelState.IsValid)
            //{
            //    // Check if a profile picture is uploaded
            //    if (profilePicture != null && profilePicture.Length > 0)
            //    {
            //        // Convert the uploaded file to a byte array
            //        using (var memoryStream = new MemoryStream())
            //        {
            //            profilePicture.CopyTo(memoryStream);
            //            obj.ProfilePicture = memoryStream.ToArray();
            //        }
            //    }


            _unitOfWork.User.Add(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageUserAccount");
            //}


            //return View(obj);
        }

        public IActionResult EditUserAccount(int? UserID)
        {
            if (UserID == null || UserID == 0)
            {
                return NotFound();
            }
            User? userFromDb = _unitOfWork.User.Get(u => u.UserID == UserID);
            //User? userFromDb2 = _db.User.Where(u => u.UserID == UserID).FirstOrDefault();

            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }

        [HttpPost]
        public IActionResult EditUserAccount(User obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.User.Update(obj);
                _unitOfWork.Save();

                return RedirectToAction("ManageUserAccount");
            }
            return View();
        }

        public IActionResult DeleteUserAccount(int? UserID)
        {
            if (UserID == null || UserID == 0)
            {
                return NotFound();
            }
            User? userFromDb = _unitOfWork.User.Get(u => u.UserID == UserID);

            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }

        [HttpPost, ActionName("DeleteUserAccount")]
        public IActionResult DeletePOSTUserAccount(int? UserID)
        {
            User? obj = _unitOfWork.User.Get(u => u.UserID == UserID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.User.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("ManageUserAccount");
        }
    }
}