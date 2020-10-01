using System;
using Empresa.Conta.Data;

namespace Empresa.Conta.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductD pD = new ProductD();

            var lista = pD.GetList();

            Console.WriteLine("=================================================================================");
            Console.WriteLine("******************** Prueba de Aplicación de Consola ****************************");
            Console.WriteLine("=================================================================================");
            Console.WriteLine("Listado de Productos");
            foreach (var p in lista)
            {
                Console.WriteLine($"Id = {p.Id}, Nombre: {p.Nombre}. Precio {p.Precio} ");
            }
            Console.WriteLine("Fin de Listado");
            Console.ReadLine();
        }
    }
}
