using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meditrack.Controllers
{
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ManageUserAccount()
        {
            List<User> objUserList = _db.User.ToList();
            return View(objUserList);
        }

        public IActionResult AddNewUserAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewUserAccount(User obj, IFormFile profilePicture)
        {
            if (ModelState.IsValid)
            {
                // Check if a profile picture is uploaded
                if (profilePicture != null && profilePicture.Length > 0)
                {
                    // Convert the uploaded file to a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        profilePicture.CopyTo(memoryStream);
                        obj.ProfilePicture = memoryStream.ToArray();
                    }
                }


                _db.User.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("ManageUserAccount");
            }


            return View(obj);
        }

        public IActionResult EditUserAccount(int? UserID)
        {
            if (UserID == null || UserID == 0)
            {
                return NotFound();
            }
            User? userFromDb = _db.User.Find(UserID);

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
                _db.User.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("ManageUserAccount");
            }
            return View();
        }
    }
}