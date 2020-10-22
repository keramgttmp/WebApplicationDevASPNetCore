﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;
using WA50.Models;

namespace WA50.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// controlado / acción 
        /// /Home/Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string filter = "")
        {
            /// returna una vista que se llama como la acción, en este caso Index
            /// 
            var result = new List<Product>();
            using (var db = new NWContext()) 
            {
                result = db.Products.Where(p => p.ProductName.Contains(filter)).ToList();
            }
            ViewData["products"] = result;
            return View();
        }

        public IActionResult Privacy()
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
