﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;
using WA4.Models;
using WA4.ViewModels;

namespace WA4.Controllers
{
    public class HomeController : Controller
    {
        NWContext dbContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, NWContext dbContext)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ///usamos el viewmodels
            ///luego de new indicamos el Viewmodel (clase) al que responde 
            ///el select para mostrar el view (index en este caso)
            var q1 = from p in this.dbContext.Products.Include(p => p.Category).ToList()
                     group p by p.Category?.CategoryName ?? "Sin Categoría" into CategoryProducts
                     select new CategoryProductsViewModel
                     {
                         CategoryName = CategoryProducts.Key,
                         Items = CategoryProducts.ToList()
                     };

            ///ViewData["products"] = q1;

            return View(q1.ToList());
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
