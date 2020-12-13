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
    public class ShipperController : Controller
    {
        private readonly Notifications ns = new Notifications();

        private readonly NWContext _context;
        private readonly IRepository<Shipper, int> _sIR;
        private readonly ShipperRepository _sR;



        public ShipperController(NWContext context, IRepository<Shipper, int> sIR, ShipperRepository sR)
        {
            _context = context;
            _sIR = sIR;
            _sR = sR;
        }

        // GET: Admin/Shipper
        public async Task<IActionResult> Index( ViewModels.ShipperIndexViewModel vm)
        {
            await vm.HandleRequest(_sR);
            return View(vm);
            //return View(await _context.Shippers.ToListAsync());
        }

        // GET: Admin/Shipper/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var shipper = await _context.Shippers
            //    .FirstOrDefaultAsync(m => m.ShipperId == id);
            var shipper = await _sR.Get(id.Value);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // GET: Admin/Shipper/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Shipper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipperId,CompanyName,Phone")] Shipper shipper)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(shipper);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                shipper.State = Model.ModelState.Added;
                await _sR.Save(shipper, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(shipper);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Admin/Shipper/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var shipper = await _context.Shippers.FindAsync(id);
            var shipper = await _sR.Get(id.Value);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        // POST: Admin/Shipper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipperId,CompanyName,Phone")] Shipper shipper)
        {
            if (id != shipper.ShipperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(shipper);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ShipperExists(shipper.ShipperId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

                shipper.State = Model.ModelState.Modified;
                await _sR.Save(shipper, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(shipper);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Admin/Shipper/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers
                .FirstOrDefaultAsync(m => m.ShipperId == id);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // POST: Admin/Shipper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var shipper = await _context.Shippers.FindAsync(id);
            //_context.Shippers.Remove(shipper);
            //await _context.SaveChangesAsync();
            await _sR.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ShipperExists(int id)
        {
            return _sR.ShipperExists(id);
        }
    }
}
