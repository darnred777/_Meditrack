using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Meditrack.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult ManageUserAccount()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            string RoleID = _db.UserRoles.FirstOrDefault(u => u.UserId == userId).RoleId;

            RoleManagementVM RoleVM = new RoleManagementVM()
            {
                ApplicationUser = _db.ApplicationUser.Include(u => u.Location).FirstOrDefault(u => u.Id == userId),
                RoleList = _db.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                LocationList = _db.Location.Select(i => new SelectListItem
                {
                    Text = i.LocationAddress,
                    Value = i.LocationID.ToString()
                }),
            };

            RoleVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(u => u.Id == RoleID).Name;
            return View(RoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            // Retrieve the current role of the user
            var role = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagementVM.ApplicationUser.Id);
            var oldRole = role != null ? _db.Roles.FirstOrDefault(r => r.Id == role.RoleId)?.Name : null;
            var appUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id);
            
            if (appUser == null)
            {
                return NotFound();
            }
            appUser.Email = roleManagementVM.ApplicationUser.Email;
            appUser.PasswordHash = roleManagementVM.ApplicationUser.PasswordHash;
            appUser.FirstName = roleManagementVM.ApplicationUser.FirstName;
            appUser.LastName = roleManagementVM.ApplicationUser.LastName;
            appUser.BirthDate = roleManagementVM.ApplicationUser.BirthDate;
            appUser.LocationID = roleManagementVM.ApplicationUser.LocationID;


            // Check if there's a role change
            if (oldRole != roleManagementVM.ApplicationUser.Role)
            {
                // Retrieve the user
                var applicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id);

                // Remove the user from the old role
                if (!string.IsNullOrEmpty(oldRole))
                {
                    _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                }

                // Add the user to the new role
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }

            _db.SaveChanges();

            // Redirect back to the ManageUserAccount action
            return RedirectToAction("ManageUserAccount");
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _db.ApplicationUser.Include(u=>u.Location).ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach(var user in objUserList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            return Json(new { data = objUserList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);

            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deactivating/Activating" });
            }
            if (objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion 
    }
}