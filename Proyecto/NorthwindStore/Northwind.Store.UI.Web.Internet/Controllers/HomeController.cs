using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.UI.Web.Internet.Models;
using Northwind.Store.UI.Web.Internet.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace Northwind.Store.UI.Web.Internet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NWContext _context;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, NWContext context, IMemoryCache memoryCache)
        {
            _logger = logger;
            _context = context;
            _memoryCache = memoryCache;
        }

        
        public IActionResult Index( ProductViewModel pvm)
        {
            var filter = HttpContext.Session.GetString(nameof(pvm.Filter));

            pvm.Filter = (pvm.Filter ?? filter) ?? "";
            HttpContext.Session.SetString(nameof(pvm.Filter), pvm.Filter);

            var result = new List<Product>();

            result = _context.Products.Where(p => p.ProductName.ToUpper().Trim().Contains(pvm.Filter)).ToList();

            //var vm = new ProductIndexViewModel() { Products = result };
            pvm.Products = result;

            return View(pvm);
            //return View(_context.Products.ToList());
        }

        public JsonResult json(string pfilter)
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;

            var filter = HttpContext.Session.GetString("Filter");

            HttpContext.Session.SetString("Filter", pfilter ?? "");

            var result = new List<Product>();

            result= _memoryCache.GetOrCreate("Product", cacheEntry => { 
                    return _context.Products.Where(p => p.ProductName.ToUpper().Trim().Contains(pfilter)).ToList();
                    }
                    );
            //result = _context.Products.Where(p => p.ProductName.ToUpper().Trim().Contains(pfilter)).ToList();

            //var vm = new ProductIndexViewModel() { Products = result };

           // result = result.Where(p => p.ProductName.ToUpper().Trim().Contains(pfilter)).ToList();

            return Json(result);//, JsonRequestBehavior.Allowed);
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
