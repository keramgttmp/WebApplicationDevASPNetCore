using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class TerritoryIndexViewModel
    {
        TerritoryRepository _tR;

        public string Command { get; set; } = "list";
        public Territory Filter { get; set; } = new Territory();
        public IEnumerable<Territory> Items { get; set; } = new List<Territory>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "TerritoryDescription" };

        public async Task HandleRequest(TerritoryRepository tR)
        {
            _tR = tR;
            
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
            Items = await _tR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _tR.Search(Filter.TerritoryDescription, Paging);
        }
    }
}
