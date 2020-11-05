﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Store.UI.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")] //se vincula el controller al área
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
