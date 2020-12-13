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
    public class SupplierController : Controller
    {
        private readonly Notifications ns = new Notifications();

        private readonly NWContext _context;
        private readonly IRepository<Supplier, int> _sIR;
        private readonly SupplierRepository _sR;

        public SupplierController(NWContext context, IRepository<Supplier, int> sIR, SupplierRepository sR)
        {
            _context = context;
            _sIR = sIR;
            _sR = sR;
        }

        // GET: Admin/Supplier
        public async Task<IActionResult> Index( ViewModels.SupplierIndexViewModel vm)
        {
            //return View(await _context.Suppliers.ToListAsync());
            await vm.HandleRequest(_sR);

            return View(vm);
        }

        // GET: Admin/Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var supplier = await _context.Suppliers
            //    .FirstOrDefaultAsync(m => m.SupplierId == id);
            var supplier = await _sR.Get(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Admin/Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(supplier);
                //await _context.SaveChangesAsync();
                supplier.State = Model.ModelState.Added;
                await _sR.Save(supplier, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(supplier);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var supplier = await _context.Suppliers.FindAsync(id);
            var supplier = await _sR.Get(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(supplier);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!SupplierExists(supplier.SupplierId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

                supplier.State = Model.ModelState.Modified;
                await _sR.Save(supplier, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(supplier);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var supplier = await _context.Suppliers.FindAsync(id);
            //_context.Suppliers.Remove(supplier);
            //await _context.SaveChangesAsync();
            await _sR.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _sR.SupplierExists(id);
        }
    }
}
