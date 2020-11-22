using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using WA70.Models;
using WA70.Settings;

namespace WA70.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NWContext _context;
        private readonly SessionSettings _ss;

        public HomeController(ILogger<HomeController> logger, NWContext context, SessionSettings ss)
        {
            _logger = logger;
            _context = context;
            _ss = ss;
        }

        public IActionResult Index()
        {
            // ya no usaremos todo esto sino que trabajeremos con la clase SessionSettings 
            //if (HttpContext.Session.GetString("welcome") == null)
            //{
            //    HttpContext.Session.SetString("welcome", $"Welcome {DateTime.Now.ToString()}");
            //}
            _ss.Welcome = $"Welcome {DateTime.Now.ToString()}";
            ViewBag.welcome = _ss.Welcome;

            

            return View(_context.Products.ToList());
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
