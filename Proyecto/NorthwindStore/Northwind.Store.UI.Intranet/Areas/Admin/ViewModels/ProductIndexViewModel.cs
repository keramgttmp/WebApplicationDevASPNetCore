using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class ProductIndexViewModel
    {
        ProductRepository _pR;

        public string Command { get; set; } = "list";
        public Product Filter { get; set; } = new Product();
        public IEnumerable<Product> Items { get; set; } = new List<Product>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "ProductName" };

        public async Task HandleRequest(ProductRepository pR)
        {
            _pR = pR;
            
            switch (Command)
            {
                case "list":
                    await list();
                    break;
                case "search":
                case "paging":
                case "order":
                    await search();
                    break;
                default:
                    break;
            }
        }

        async Task list()
        {
            Items = await _pR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _pR.Search(Filter.ProductName, Paging);
        }
    }
}
