using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class ShipperIndexViewModel
    {
        ShipperRepository _sR;

        public string Command { get; set; } = "list";
        public Shipper Filter { get; set; } = new Shipper();
        public IEnumerable<Shipper> Items { get; set; } = new List<Shipper>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "CompanyName" };

        public async Task HandleRequest(ShipperRepository sR)
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
