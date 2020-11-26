using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.UI.Web.Internet.Models;
using Northwind.Store.UI.Web.Internet.ViewModels;

namespace Northwind.Store.UI.Web.Internet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NWContext _context;

        public HomeController(ILogger<HomeController> logger, NWContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index( ProductViewModel pvm)
        {
            var filter = HttpContext.Session.GetString(nameof(pvm.Filter));

            pvm.Filter = (pvm.Filter ?? filter) ?? "";
            HttpContext.Session.SetString(nameof(pvm.Filter), pvm.Filter);

            //if (tvm.Filter != filter)
            //{
            //    tvm.Page = 1;
            //}

            var result = new List<Product>();

            result = _context.Products.Where(p => p.ProductName.ToUpper().Trim().Contains(pvm.Filter)).ToList();


            //var vm = new ProductIndexViewModel() { Products = result };
            pvm.Products = result;

            return View(pvm);
            //return View(_context.Products.ToList());
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
