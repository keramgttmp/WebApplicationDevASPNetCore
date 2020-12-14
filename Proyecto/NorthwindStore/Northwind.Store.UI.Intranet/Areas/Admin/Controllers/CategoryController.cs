﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Notification;
using Northwind.Store.UI.Intranet.Areas.Admin.ViewModels;

namespace Northwind.Store.UI.Intranet.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly Notifications ns = new Notifications();

        private readonly NWContext _context;
        private readonly IRepository<Category, int> _cIR;
        private readonly CategoryRepository _cR;

        public CategoryController(NWContext context, IRepository<Category, int> cIR, CategoryRepository cR)
        {
            _context = context;
            _cIR = cIR;
            _cR = cR;
        }

        //public IActionResult Index0()
        //{
        //    return View(_context.Categories.ToList());
        //}

        // GET: Admin/Category
        //[Authorize]
        //public IActionResult Index(ViewModels.CategoryIndexViewModel vm)
        public async Task<IActionResult> Index(ViewModels.CategoryIndexViewModel vm)
        {
            await vm.HandleRequest(_cR);

            return View(vm);

            //vm.Items = await _cR.GetList();
            // return View(await _cR.GetList(pf));
            //return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/Category/Details/5
        //[AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _context.Categories
            //    .FirstOrDefaultAsync(m => m.CategoryId == id);
            var category = await _cR.Get(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description")] Category category, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picture.CopyTo(ms);
                        category.Picture = ms.ToArray();
                    }
                }
                //_context.Add(category);
                //await _context.SaveChangesAsync();
                category.State = Model.ModelState.Added;
                await _cR.Save(category, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(category);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _context.Categories.FindAsync(id);
            var category = await _cR.Get(id.Value);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,ModifiedProperties,RowVersion")] Category category, IFormFile picture)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(category);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!CategoryExists(category.CategoryId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

                if (picture != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picture.CopyTo(ms);
                        category.Picture = ms.ToArray();
                    }

                    category.ModifiedProperties.Add("Picture");
                }

                category.State = Model.ModelState.Modified;
                await _cR.Save(category, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(category);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var category = await _context.Categories.FindAsync(id);
            //_context.Categories.Remove(category);
            //await _context.SaveChangesAsync();

            await _cR.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        public async Task<FileStreamResult> ReadImage(int id)
        {
            FileStreamResult result = null;

            var file = await ((CategoryRepository)_cR).GetFileStream(id);
            if (file != null)
            {
                result = File(file, "image/jpg");
            }

            return result;
        }
    }
}
