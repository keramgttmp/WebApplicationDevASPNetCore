using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Notification;

namespace Northwind.Store.UI.Intranet.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly Notifications ns = new Notifications();

        private readonly NWContext _context;
        private readonly IRepository<Order, int> _oIR;
        private readonly OrderRepository _oR;

        public OrderController(NWContext context, IRepository<Order, int> oIR, OrderRepository oR)
        {
            _context = context;
            _oIR = oIR;
            _oR = oR;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index(ViewModels.OrderIndexViewModel vm)
        {
            //var nWContext = _context.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.ShipViaNavigation);
            //return View(await nWContext.ToListAsync());
            await vm.HandleRequest(_oR);
            return View(vm);

        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _context.Orders
            //    .Include(o => o.Customer)
            //    .Include(o => o.Employee)
            //    .Include(o => o.ShipViaNavigation)
            //    .FirstOrDefaultAsync(m => m.OrderId == id);

            var order = await _oR.GetWithDetails(id.Value);
            ViewData["TotalOrden"]= order.OrderDetails.Sum(p => p.UnitPrice);
            if (order == null)
            {
                return NotFound();
            }

            

            return View(order);
        }

        // GET: Admin/Order/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,EmployeeId,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(order);
                //await _context.SaveChangesAsync();
                order.State = Model.ModelState.Added;
                await _oR.Save(order, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(order);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", order.ShipVia);
            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _context.Orders.FindAsync(id);
            var order = await _oR.Get(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", order.ShipVia);
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,EmployeeId,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(order);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!OrderExists(order.OrderId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                order.State = Model.ModelState.Modified;
                await _oR.Save(order, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(order);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", order.ShipVia);
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var order = await _context.Orders.FindAsync(id);
            //_context.Orders.Remove(order);
            //await _context.SaveChangesAsync();
            await _oR.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _oR.OrderExists(id);
        }
    }
}
