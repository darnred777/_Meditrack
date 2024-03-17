using Meditrack.Data;
using Meditrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Update_Profile()
        {
            return View();
        }

        //public IActionResult ManageUserAccount()
        //{
        //    List<User> objUserList = _db.User.ToList();
        //    return View(objUserList);
        //}

        //public IActionResult ManageUserGroup()
        //{
        //    List<UserGroup> objUserGroupList = _db.UserGroup.ToList();
        //    return View(objUserGroupList);
        //}

        //public IActionResult EditUserGroup(int? UserGroupID)
        //{
        //    if (UserGroupID == null || UserGroupID == 0)
        //    {
        //        return NotFound();
        //    }
        //    UserGroup? userGroupFromDb = _db.UserGroup.Find(UserGroupID);

        //    if (userGroupFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userGroupFromDb);
        //}

        //[HttpPost]
        //public IActionResult EditUserGroup(UserGroup obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.UserGroup.Update(obj);
        //        _db.SaveChanges();

        //        return RedirectToAction("ManageUserGroup");
        //    }
        //    return View();
        //}

        //public IActionResult AddNewUserAccount()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddNewUserAccount(User obj, IFormFile profilePicture)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Check if a profile picture is uploaded
        //        if (profilePicture != null && profilePicture.Length > 0)
        //        {
        //            // Convert the uploaded file to a byte array
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                profilePicture.CopyTo(memoryStream);
        //                obj.ProfilePicture = memoryStream.ToArray();
        //            }
        //        }


        //        _db.User.Add(obj);
        //        _db.SaveChanges();

        //        return RedirectToAction("ManageUserAccount");
        //    }


        //    return View(obj);
        //}
        //public IActionResult AddNewUserAccount(User obj)
        //{
        //_db.User.Add(obj);
        //_db.SaveChanges();
        //return RedirectToAction("ManageUserAccount");
        //}

        //public IActionResult EditUserAccount(int? UserID)
        //{
        //    if (UserID == null || UserID == 0)
        //    {
        //        return NotFound();
        //    }
        //    User? userFromDb = _db.User.Find(UserID);

        //    if(userFromDb == null)
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
        //        _db.User.Update(obj);
        //        _db.SaveChanges();

        //        return RedirectToAction("ManageUserAccount");
        //    }
        //    return View();
        //}

        //public IActionResult ManageVendor()
        //{
        //    List<Supplier> objSupplierList = _db.Supplier.ToList();
        //    return View(objSupplierList);
        //}

        //public IActionResult EditVendor(int? SupplierID)
        //{
        //    if (SupplierID == null || SupplierID == 0)
        //    {
        //        return NotFound();
        //    }
        //    Supplier? supplierFromDb = _db.Supplier.Find(SupplierID);

        //    if (supplierFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(supplierFromDb);
        //}

        //[HttpPost]
        //public IActionResult EditVendor(Supplier obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Supplier.Update(obj);
        //        _db.SaveChanges();

        //        return RedirectToAction("ManageVendor");
        //    }
        //    return View();
        //}

        //public IActionResult AddVendor()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddVendor(Supplier obj)
        //{
        //    _db.Supplier.Add(obj);
        //    _db.SaveChanges();

        //    return RedirectToAction("ManageVendor");
        //}

        public IActionResult ManageProductCategory()
        {
            List<ProductCategory> objProductCategoryList = _db.ProductCategory.ToList();
            return View(objProductCategoryList);
        }
        public IActionResult ManageProduct()
        {
            List<Product> objProductList = _db.Product.ToList();
            return View(objProductList);
        }

        public IActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(Product obj)
        {
            _db.Product.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("ManageProduct");
        }

        [HttpPost]
        public IActionResult AddNewProductCategory(ProductCategory obj)
        {
            _db.ProductCategory.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("ManageProductCategory");
        }

        public IActionResult EditProductCategory(int? CategoryID)
        {
            if (CategoryID == null || CategoryID == 0)
            {
                return NotFound();
            }
            ProductCategory? productCategoryFromDb = _db.ProductCategory.Find(CategoryID);

            if (productCategoryFromDb == null)
            {
                return NotFound();
            }
            return View(productCategoryFromDb);
        }

        [HttpPost]
        public IActionResult EditProductCategory(ProductCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProductCategory.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("ManageProductCategory");
            }
            return View();
        }

        public IActionResult AddNewProductCategory()
        {
            return View();
        }

        public IActionResult Transaction()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            List<Product> objProductList = _db.Product.ToList();
            return View(objProductList);
        }

        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult CreatePurchaseRequisition()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
