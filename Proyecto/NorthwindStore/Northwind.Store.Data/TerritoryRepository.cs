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
    public class TerritoryRepository : BaseRepository<Territory, int>
    {
        public TerritoryRepository(NWContext context) : base(context) { }

        public async Task<Territory> Get(string key)
        {
            //var result = await base.Get(key);
            var result = await _db.Territories.Include(t => t.Region).SingleAsync(i => i.TerritoryId == key);

            return result;
        }

        public async Task<int> Delete(string key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from Territory where territoryid = {key}");
        }

        public async Task<IEnumerable<Territory>> Search(string filter, PageFilter pf)
        {
            var result = new List<Territory>();

            pf.Count = await _db.Territories.Where(c => string.IsNullOrEmpty(filter) || 
                                               c.TerritoryDescription.Contains(filter)).CountAsync();

            result = await _db.Territories.AsNoTracking().
                Where(c => string.IsNullOrEmpty(filter) || c.TerritoryDescription.Contains(filter)).OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        public Boolean TerritoryExists(string id)
        {
            return _db.Territories.Any(e => e.TerritoryId == id);
        }
    }
}
