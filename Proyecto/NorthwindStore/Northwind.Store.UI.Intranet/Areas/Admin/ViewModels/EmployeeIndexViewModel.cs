using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class EmployeeIndexViewModel
    {
        //IRepository<Category, int> _cR;
        EmployeeRepository _eR;

        public string Command { get; set; } = "list";
        public Employee Filter { get; set; } = new Employee();
        public IEnumerable<Employee> Items { get; set; } = new List<Employee>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "FirstName" };

        public async Task HandleRequest(EmployeeRepository eR)
        {
            _eR = eR;
            
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
            Items = await _eR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _eR.Search(string.Concat(Filter.FirstName, Filter.LastName), Paging);
        }
    }
}
