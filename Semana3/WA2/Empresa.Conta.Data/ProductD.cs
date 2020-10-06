using System;
using System.Collections.Generic;
using Empresa.Conta.Model;

namespace Empresa.Conta.Data
{
    public class ProductD
    {
        /// <summary>
        /// Se obtiene una lista de productos
        /// </summary>
        /// <returns></returns>
        public List<Product> GetList()
        {
            return new List<Product>() {
                new Product(){ 
                Id =1,
                Nombre= "Leche en Caja", Precio =100},
                new Product(){
                Id =2,
                Nombre= "Galletas dulce", Precio =500},
                new Product(){
                Id =3,
                Nombre= "Chocolates", Precio =70}
            };
        }
    }
}
