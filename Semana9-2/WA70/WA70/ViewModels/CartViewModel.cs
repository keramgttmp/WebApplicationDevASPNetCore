using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA70.ViewModels
{
    public class CartViewModel
    {
        public List<Product> Items { get; set; } = new List<Product>();
        public int Count=> Items.Count;
        public decimal Total => Items.Sum(p => p.UnitPrice ?? 0);

        //public CartViewModel()
        //{
        //    Items = new List<Product>();
        //}
    }
}
