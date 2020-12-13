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
    public class RegionController : Controller
    {
        private readonly Notifications ns = new Notifications();
        private readonly NWContext _context;
        private readonly IRepository<Region, int> _rIR;
        private readonly RegionRepository _rR;

        public RegionController(NWContext context, IRepository<Region, int> rIR, RegionRepository rR)
        {
            _context = context;
            _rIR = rIR;
            _rR = rR;
        }

        // GET: Admin/Region
        public async Task<IActionResult> Index( ViewModels.RegionIndexViewModel vm)
        {
            await vm.HandleRequest(_rR);
            return View(vm);
            //return View(await _context.Region.ToListAsync());
        }

        // GET: Admin/Region/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var region = await _context.Region
            //    .FirstOrDefaultAsync(m => m.RegionId == id);
            var region = await _rR.Get(id.Value);

            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Admin/Region/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Region/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,RegionDescription")] Region region)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(region);
                //await _context.SaveChangesAsync();
                region.State = Model.ModelState.Added;
                await _rR.Save(region, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(region);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Admin/Region/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var region = await _context.Region.FindAsync(id);
            var region = await _rR.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: Admin/Region/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,RegionDescription")] Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(region);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!RegionExists(region.RegionId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                region.State = Model.ModelState.Modified;
                await _rR.Save(region, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(region);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Admin/Region/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Admin/Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var region = await _context.Region.FindAsync(id);
            //_context.Region.Remove(region);
            //await _context.SaveChangesAsync();
            
            await _rR.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool RegionExists(int id)
        {
            return _rR.RegionExists(id);
        }
    }
}
