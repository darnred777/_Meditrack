﻿using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Models.ViewModels;
using Meditrack.Repository;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Meditrack.Areas.Approver.Controllers
{
    [Area("Approver")]
    [Authorize(Roles = StaticDetails.Role_Approver)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ManageProduct()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategory").ToList();
            return View(objProductList);
        }

        public IActionResult ExpiringProducts()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var tenDaysLater = today.AddDays(10);

            // Access products through the unit of work
            var expiringProducts = _unitOfWork.Product
                .GetAll(p => p.ExpirationDate >= today && p.ExpirationDate <= tenDaysLater)
                .ToList();

            return View(expiringProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}