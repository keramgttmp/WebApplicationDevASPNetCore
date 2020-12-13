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
    public class TerritoryController : Controller
    {
        private readonly Notifications ns = new Notifications();

        private readonly NWContext _context;
        private readonly IRepository<Territory, int> _tIR;
        private readonly TerritoryRepository _tR;

        public TerritoryController(NWContext context, IRepository<Territory, int> tIR, TerritoryRepository tR)
        {
            _context = context;
            _tIR = tIR;
            _tR = tR;
        }

        // GET: Admin/Territory
        public async Task<IActionResult> Index( ViewModels.TerritoryIndexViewModel vm)
        {
            //var nWContext = _context.Territories.Include(t => t.Region);
            //return View(await nWContext.ToListAsync());

            await vm.HandleRequest(_tR);

            return View(vm);
        }

        // GET: Admin/Territory/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var territory = await _context.Territories
            //    .Include(t => t.Region)
            //    .FirstOrDefaultAsync(m => m.TerritoryId == id);
            var territory= await _tR.Get(id);

            if (territory == null)
            {
                return NotFound();
            }

            return View(territory);
        }

        // GET: Admin/Territory/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionDescription");
            return View();
        }

        // POST: Admin/Territory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerritoryId,TerritoryDescription,RegionId")] Territory territory)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(territory);
                //await _context.SaveChangesAsync();
                territory.State = Model.ModelState.Added;
                await _tR.Save(territory, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(territory);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionDescription", territory.RegionId);
            return View(territory);
        }

        // GET: Admin/Territory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var territory = await _context.Territories.FindAsync(id);
            var territory = await _tR.Get(id);
            if (territory == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionDescription", territory.RegionId);
            return View(territory);
        }

        // POST: Admin/Territory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TerritoryId,TerritoryDescription,RegionId")] Territory territory)
        {
            if (id != territory.TerritoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(territory);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!TerritoryExists(territory.TerritoryId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                territory.State = Model.ModelState.Modified;
                await _tR.Save(territory, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(territory);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionDescription", territory.RegionId);
            return View(territory);
        }

        // GET: Admin/Territory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _context.Territories
                .Include(t => t.Region)
                .FirstOrDefaultAsync(m => m.TerritoryId == id);
            if (territory == null)
            {
                return NotFound();
            }

            return View(territory);
        }

        // POST: Admin/Territory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var territory = await _context.Territories.FindAsync(id);
            //_context.Territories.Remove(territory);
            //await _context.SaveChangesAsync();
            await _tR.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TerritoryExists(string id)
        {
            return _tR.TerritoryExists(id);
        }
    }
}
