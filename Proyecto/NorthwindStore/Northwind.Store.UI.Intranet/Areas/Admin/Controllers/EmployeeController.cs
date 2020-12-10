using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Notification;

namespace Northwind.Store.UI.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly Notifications ns = new Notifications();
        private readonly NWContext _context;
        private readonly IRepository<Employee, int> _eIR;
        private readonly EmployeeRepository _eR;
        

        public EmployeeController(NWContext context, IRepository<Employee, int> eIR, EmployeeRepository eR)
        {
            _context = context;
            _eIR = eIR;
            _eR = eR;

        }

        // GET: Admin/Employee
        public async Task<IActionResult> Index(ViewModels.EmployeeIndexViewModel vm)
        {
            await vm.HandleRequest(_eR);
            return View(vm);
        }

        // GET: Admin/Employee/Details/5
        
        //Opción 1 con async Task >>    public async Task<IActionResult> Details(int? id)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Antes de incluir el trabajo con el repositorio
            //var employee = await _context.Employees
            //    .Include(e => e.ReportsToNavigation)
            //    .FirstOrDefaultAsync(m => m.EmployeeId == id);

            //opción 1 con el IRepository
            var employee = await _eR.Get(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employee/Create
        public IActionResult Create()
        {
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: Admin/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employee employee, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picture.CopyTo(ms);
                        employee.Photo= ms.ToArray();
                    }
                }

                //_context.Add(employee);
                //await _context.SaveChangesAsync();
                employee.State = Model.ModelState.Added;
                await _eIR.Save(employee, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(employee);
                }


                return RedirectToAction(nameof(Index));
            }

            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", employee.ReportsTo);
            return View(employee);
        }

        // GET: Admin/Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees.FindAsync(id);
            var employee = await _eIR.Get(id.Value);

            if (employee == null)
            {
                return NotFound();
            }
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", employee.ReportsTo);
            return View(employee);
        }

        // POST: Admin/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employee employee, IFormFile picture)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(employee);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EmployeeExists(employee.EmployeeId))
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
                        employee.Photo= ms.ToArray();
                    }

                    employee.ModifiedProperties.Add("Photo");
                }

                employee.State = Model.ModelState.Modified;
                await _eIR.Save(employee, ns);

                if (ns.Any())
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");
                    return View(employee);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", employee.ReportsTo);
            return View(employee);
        }

        // GET: Admin/Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.ReportsToNavigation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var employee = await _context.Employees.FindAsync(id);
            //_context.Employees.Remove(employee);
            //await _context.SaveChangesAsync();
            await _eIR.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _eR.EmployeeExists(id);
        }

        ///Creamos un método para leer la info de  la imagen y mostrarla
        public async Task<FileStreamResult> ReadImage(int id)
        {
            FileStreamResult result = null;

            //var image = await _context.Employees.Where(c => c.EmployeeId == id).Select(i => i.Photo).AsNoTracking().FirstOrDefaultAsync();

            //if (image != null)
            //{
            //    var stream = new MemoryStream(image);

            //    if (stream != null)
            //    {
            //        result = File(stream, "image/bmp");
            //    }
            //}

            var file = await ((EmployeeRepository)_eR).GetFileStream(id);
            if (file != null)
            {
                result = File(file, "image/jpg");
            }

            return result;
        }
    }
}
