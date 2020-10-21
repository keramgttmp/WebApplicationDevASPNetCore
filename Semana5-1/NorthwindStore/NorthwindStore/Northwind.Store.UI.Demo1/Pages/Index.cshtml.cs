using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;

namespace Northwind.Store.UI.Demo1.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Customer> Customers { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            //vpy por el punto 9
            using (var db = new NWContext())
            {

                var vQuery = from c in db.Customers
                             orderby c.CompanyName
                             select c; // new { c.CompanyName, c.ContactName, c.Country };
                var vData = vQuery.ToList();

                Customers = vData;
                //foreach (var p in vQuery)
                //{
                //    Console.WriteLine($"{p.CompanyName.PadRight(50, ' ')}{p.ContactName.PadRight(30, ' ')}{p.Country}");
                //}

            }
        }
    }
}
