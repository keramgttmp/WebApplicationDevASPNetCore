using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class RegionIndexViewModel
    {
        RegionRepository _rR;

        public string Command { get; set; } = "list";
        public Region Filter { get; set; } = new Region();
        public IEnumerable<Region> Items { get; set; } = new List<Region>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "RegionDescription" };

        public async Task HandleRequest(RegionRepository rR)
        {
            _rR = rR;
            
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
            Items = await _rR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _rR.Search(Filter.RegionDescription, Paging);
        }
    }
}
