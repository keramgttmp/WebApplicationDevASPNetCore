using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class SupplierIndexViewModel
    {
        SupplierRepository _sR;

        public string Command { get; set; } = "list";
        public Supplier Filter { get; set; } = new Supplier();
        public IEnumerable<Supplier> Items { get; set; } = new List<Supplier>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "CompanyName" };

        public async Task HandleRequest(SupplierRepository sR)
        {
            _sR = sR;
            
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
            Items = await _sR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _sR.Search(Filter.CompanyName, Paging);
        }
    }
}
