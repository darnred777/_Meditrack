//using Meditrack.Data;
//using Meditrack.Models;
//using Meditrack.Models.ViewModels;
//using Meditrack.Repository.IRepository;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace Meditrack.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class UserController : Controller
//    {

//        private readonly IUnitOfWork _unitOfWork;
//        public UserController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }
//        public IActionResult ManageUserAccount()
//        {
//            List<User> objUserList = _unitOfWork.User.GetAll(includeProperties: "Location").ToList();
            
//            return View(objUserList);
//        }

//        //ViewBag
//        public IActionResult UpsertUserAccount(int? userID)
//        {
//            //IEnumerable<SelectListItem> LocationList =

//            //ViewBag.LocationList = LocationList;
//            //ViewData["LocationList"] = LocationList;
//            UserVM userVM = new()
//            {
//                LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
//                {
//                    Text = u.LocationAddress,
//                    Value = u.LocationID.ToString()
//                }),

//                User = new User()
//            };

//            if(userID == null || userID == 0)
//            {
//                //create
//                return View(userVM);
//            }
//            else
//            {
//                userVM.User = _unitOfWork.User.Get(u=>u.UserID == userID);

//                //update
//                return View(userVM);
//            }
            
//        }

//        [HttpPost]
//        public IActionResult UpsertUserAccount(UserVM userVM)
//        {
//            if (ModelState.IsValid)
//            {
//                if (userVM.User.UserID == 0)
//                {
//                    // Adding a new user
//                    _unitOfWork.User.Add(userVM.User);
//                }
//                else
//                {
//                    // Updating an existing user
//                    _unitOfWork.User.Update(userVM.User);
//                }

//                _unitOfWork.Save();

//                return RedirectToAction("ManageUserAccount");
//            }
//            else
//            {
//                userVM.LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
//                {
//                    Text = u.LocationAddress,
//                    Value = u.LocationID.ToString()
//                });
//                return View(userVM);
//            }
//        }


//        //[HttpPost]
//        //public IActionResult UpsertUserAccount(UserVM userVM) 
//        //{   

//        //    if (ModelState.IsValid)
//        //    {
//        //        _unitOfWork.User.Add(userVM.User);
//        //        _unitOfWork.Save();

//        //        return RedirectToAction("ManageUserAccount");

//        //    } else
//        //    {
//        //        //UserVM userVM = new()
//        //        //{
//        //        userVM.LocationList = _unitOfWork.Location.GetAll().Select(u => new SelectListItem
//        //        {
//        //            Text = u.LocationAddress,
//        //            Value = u.LocationID.ToString()
//        //        });
//        //        return View(userVM);
//        //    }           
//        //}

//        //public IActionResult EditUserAccount(int? UserID)
//        //{
//        //    if (UserID == null || UserID == 0)
//        //    {
//        //        return NotFound();
//        //    }
//        //    User? userFromDb = _unitOfWork.User.Get(u => u.UserID == UserID);
//        //    //User? userFromDb2 = _db.User.Where(u => u.UserID == UserID).FirstOrDefault();

//        //    if (userFromDb == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return View(userFromDb);
//        //}

//        //[HttpPost]
//        //public IActionResult EditUserAccount(User obj)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        _unitOfWork.User.Update(obj);
//        //        _unitOfWork.Save();

//        //        return RedirectToAction("ManageUserAccount");
//        //    }
//        //    return View();
//        //}

//        public IActionResult DeleteUserAccount(int? UserID)
//        {
//            if (UserID == null || UserID == 0)
//            {
//                return NotFound();
//            }
//            User? userFromDb = _unitOfWork.User.Get(u => u.UserID == UserID);

//            if (userFromDb == null)
//            {
//                return NotFound();
//            }
//            return View(userFromDb);
//        }

//        [HttpPost, ActionName("DeleteUserAccount")]
//        public IActionResult DeletePOSTUserAccount(int? UserID)
//        {
//            User? obj = _unitOfWork.User.Get(u => u.UserID == UserID);
//            if (obj == null)
//            {
//                return NotFound();
//            }
//            _unitOfWork.User.Remove(obj);
//            _unitOfWork.Save();

//            return RedirectToAction("ManageUserAccount");
//        }
//    }
//}