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
    public class OrderRepository : BaseRepository<Order, int>
    {
        public OrderRepository(NWContext context) : base(context) { }

        public override async Task<Order> Get(int key)
        {
            //var result = await base.Get(key);
            var result = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == key);

            return result;
        }

        public override async Task<IEnumerable<Order>> GetList(PageFilter pf = null)
        {

            var result = new List<Order>();

            if (pf == null)
            {
                result = await _db.Set<Order>().AsNoTracking().ToListAsync();
            }
            else
            {
                pf.Count = await _db.Orders.Include(o => o.Customer)
                 .Include(o => o.Employee)
                 .Include(o => o.ShipViaNavigation).CountAsync();

                result = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .AsNoTracking().
                    OrderBy(pf.Sorting).
                    Skip((pf.Page - 1) * pf.PageSize).
                    Take(pf.PageSize).ToListAsync();
            }

            return result;
        }

        public async Task<Order> GetWithDetails(int key)
        {
            //var result = await base.Get(key);
            var result = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(m => m.OrderId == key);
            
            return result;
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from Order where orderid = {key}");
        }

        public async Task<IEnumerable<Order>> Search(string filter, PageFilter pf)
        {
            var result = new List<Order>();

            pf.Count = await _db.Orders.Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .Where(c => string.IsNullOrEmpty(filter) || 
                            c.Customer.ContactName.Contains(filter)).CountAsync();

            result = await _db.Orders.AsNoTracking().
                Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .Where(c => string.IsNullOrEmpty(filter) || c.Customer.ContactName.Contains(filter)).OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        public Boolean OrderExists(int id)
        {
            return _db.Orders.Any(e => e.OrderId == id);
        }

        public decimal OrderSubTotal(int id)
        {
            var SubTotal = _db.OrderSubtotals.FirstOrDefaultAsync(m => m.OrderId == id);
            return (decimal)SubTotal.Result.Subtotal;

        }
    }
}
