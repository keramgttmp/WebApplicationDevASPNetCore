using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empresa.Conta.Data;
using Empresa.Conta.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WA22.Pages
{
    public class ProductModel : PageModel
    {
        public List<Product> Products { get; set; }
        public IEnumerable<int> Nums { get; set; } = Enumerable.Range(1, 10);

        /// <summary>
        /// HTTP GET
        /// </summary>
        public void OnGet()
        {
            ProductD pD = new ProductD();

            Products = pD.GetList();
        }
    }
}
