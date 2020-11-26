using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Web.Internet.ViewModels
{
    public class ProductViewModel
    {
        public string Filter { get; set; }
        public List<Product> Products { get; set; }
    }
}
