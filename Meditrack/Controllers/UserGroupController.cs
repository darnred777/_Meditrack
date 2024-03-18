using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Controllers
{
    public class UserGroupController : Controller
    {

        private readonly ApplicationDbContext _db;
        public UserGroupController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ManageUserGroup()
        {
            List<UserGroup> objUserGroupList = _db.UserGroup.ToList();
            return View(objUserGroupList);
        }

        public IActionResult EditUserGroup(int? UserGroupID)
        {
            if (UserGroupID == null || UserGroupID == 0)
            {
                return NotFound();
            }
            UserGroup? userGroupFromDb = _db.UserGroup.Find(UserGroupID);

            if (userGroupFromDb == null)
            {
                return NotFound();
            }
            return View(userGroupFromDb);
        }

        [HttpPost]
        public IActionResult EditUserGroup(UserGroup obj)
        {
            if (ModelState.IsValid)
            {
                _db.UserGroup.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("ManageUserGroup");
            }
            return View();
        }

        public IActionResult DeleteUserGroup(int? UserGroupID)
        {
            if (UserGroupID == null || UserGroupID == 0)
            {
                return NotFound();
            }
            UserGroup? userGroupFromDb = _db.UserGroup.Find(UserGroupID);

            if (userGroupFromDb == null)
            {
                return NotFound();
            }
            return View(userGroupFromDb);
        }

        [HttpPost, ActionName("DeleteUserGroup")]
        public IActionResult DeletePOSTUserGroup(int? UserGroupID)
        {
            UserGroup? obj = _db.UserGroup.Find(UserGroupID);
            if (obj == null)
            {
                return NotFound();
            }
            _db.UserGroup.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("ManageUserGroup");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
