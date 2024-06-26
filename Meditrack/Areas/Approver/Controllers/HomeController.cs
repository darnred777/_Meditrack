﻿using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meditrack.Areas.Approver.Controllers
{
    [Area("Approver")]
    [Authorize(Roles = StaticDetails.Role_Approver)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Product()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategory").ToList();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
