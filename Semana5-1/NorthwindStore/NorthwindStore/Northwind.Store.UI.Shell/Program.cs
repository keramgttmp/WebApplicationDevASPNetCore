using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Linq;

namespace Northwind.Store.UI.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            //vpy por el punto 9
            using (var db = new NWContext())
            {

                var vQuery = db.Customers;

                foreach (var p in vQuery)
                {
                    Console.WriteLine($"{p.CompanyName}");
                }

            }
            Console.WriteLine("Hello World!");
        }
    }
}
