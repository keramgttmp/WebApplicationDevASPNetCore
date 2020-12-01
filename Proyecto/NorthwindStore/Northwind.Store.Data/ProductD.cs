using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Store.Data
{
    public class ProductD : ICreateReadUpdateDelete<Model.Product>
    {
        readonly protected NWContext _db = null;

        public ProductD(NWContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Método para crear un producto
        /// </summary>
        /// <param name="p">Instancia de Product</param>
        public void Create(Product p)
        {
            _db.Products.Add(p);
            _db.SaveChanges();
        }

        public void Delete(Product p)
        {
            _db.Remove(p);
            _db.SaveChanges();
        }

        public List<Product> Read()
        {
            return _db.Products.ToList();
        }

        public Product Read(int key)
        {
            return _db.Products.Find(key);
        }

        public List<Product> Read(string filter)
        {
            return _db.Products.Where(c => c.ProductName.Contains(filter)).ToList();
        }

        public void Update(Product p)
        {
            _db.Update(p);
            _db.SaveChanges();
        }

        public List<Product> Search(string filter, int page)
        {
            throw new NotImplementedException();
        }
    }
}
