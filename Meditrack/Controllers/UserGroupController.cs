using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
