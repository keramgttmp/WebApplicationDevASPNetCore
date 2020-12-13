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
    public class ShipperRepository : BaseRepository<Shipper, int>
    {
        public ShipperRepository(NWContext context) : base(context) { }

        public override async Task<Shipper> Get(int key)
        {
            var result = await base.Get(key);

            return result;
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from shippers where shipperid = {key}");
        }

        public async Task<IEnumerable<Shipper>> Search(string filter, PageFilter pf)
        {
            var result = new List<Shipper>();

            pf.Count = await _db.Shippers.Where(c => string.IsNullOrEmpty(filter) || 
                                               c.CompanyName.Contains(filter)).CountAsync();

            result = await _db.Shippers.AsNoTracking().
                Where(c => string.IsNullOrEmpty(filter) || c.CompanyName.Contains(filter)).OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        public Boolean ShipperExists(int id)
        {
            return _db.Shippers.Any(e => e.ShipperId == id);
        }
    }
}
