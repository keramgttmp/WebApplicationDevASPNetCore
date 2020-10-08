using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace WA30.Pages
{
    public class serversideModel : PageModel
    {
        public List <Product> Products { get; set; }

        [BindProperty]
        public string Filter { get; set; }
        public void OnGet()
        {
            var pD = new ProductD();
            var pM = pD.List();

            Products = pM;
        }

        public void OnPost()
        {
            var pD = new ProductD();
            var pM = pD.List();
            pM = pM.Where(p => p.ProductName.Contains(Filter)).ToList();
            Products = pM;
        }
    }
}
