using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Northwind.Store.UI.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            //vpy por el punto 9
            using (var db = new NWContext())
            {

                var vQuery = from c in db.Customers
                             orderby c.CompanyName
                             select new { c.CompanyName, c.ContactName, c.Country};

                foreach (var p in vQuery)
                {
                    Console.WriteLine($"{p.CompanyName.PadRight(50,' ')}{p.ContactName.PadRight(30, ' ')}{p.Country}");
                }

            }
            Console.WriteLine("Hello World!");
        }
    }
}
