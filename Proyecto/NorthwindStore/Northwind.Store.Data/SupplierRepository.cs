using System;
using System.Collections.Generic;
using Northwind.Store.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Northwind.Store.Data
{
    public class SupplierRepository : BaseRepository<Supplier, int>
    {
        public SupplierRepository(NWContext context) : base(context) { }

        public override async Task<Supplier> Get(int key)
        {
            var result = await base.Get(key);

            return result;
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from Supplier where supplierid = {key}");
        }

        public async Task<IEnumerable<Supplier>> Search(string filter, PageFilter pf)
        {
            var result = new List<Supplier>();

            pf.Count = await _db.Suppliers.Where(c => string.IsNullOrEmpty(filter) || 
                                               c.CompanyName.Contains(filter)).CountAsync();

            result = await _db.Suppliers.AsNoTracking().
                Where(c => string.IsNullOrEmpty(filter) || c.CompanyName.Contains(filter)).OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        public Boolean SupplierExists(int id)
        {
            return _db.Suppliers.Any(e => e.SupplierId == id);
        }
    }
}
