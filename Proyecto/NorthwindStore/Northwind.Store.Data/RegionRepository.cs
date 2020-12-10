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
    public class RegionRepository : BaseRepository<Region, int>
    {
        public RegionRepository(NWContext context) : base(context) { }

        public override async Task<Region> Get(int key)
        {
            var result = await base.Get(key);

            return result;
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from region where regionid = {key}");
        }

        public async Task<IEnumerable<Region>> Search(string filter, PageFilter pf)
        {
            var result = new List<Region>();

            pf.Count = await _db.Region.Where(c => string.IsNullOrEmpty(filter) || 
                                               c.RegionDescription.Contains(filter)).CountAsync();

            result = await _db.Region.AsNoTracking().
                Where(c => string.IsNullOrEmpty(filter) || c.RegionDescription.Contains(filter)).OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        public Boolean RegionExists(int id)
        {
            return _db.Region.Any(e => e.RegionId == id);
        }
    }
}
