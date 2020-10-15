using System;
using System.Linq;

namespace WA40
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db= new NWContext())
            {
                Console.WriteLine("Ingresa una palabra para filtrar los productos a mostrar:");

                var vFiltro = Console.ReadLine();

                var vQuery = db.Products.Where(p => p.ProductName.Contains(vFiltro));
                
                foreach (var p in vQuery)
                {
                    Console.WriteLine($"{p.ProductName}");
                }

            }

            Console.WriteLine("Listo......");
            Console.ReadKey();
        }
    }
}
