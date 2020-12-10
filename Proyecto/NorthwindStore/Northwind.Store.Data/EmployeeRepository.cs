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
    public class EmployeeRepository : BaseRepository<Employee, int>, IFileRepository<int>
    {
        public EmployeeRepository(NWContext context) : base(context) { }

        public override async Task<Employee> Get(int key)
        {
            var result = await _db.Employees.Include(e => e.ReportsToNavigation).SingleAsync(i => i.EmployeeId == key);

            result.PictureBase64 = await GetFileBase64(key);

            return result;
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from employees where employeeid = {key}");
        }

        public async Task<IEnumerable<Employee>> Search(string filter, PageFilter pf)
        {
            var result = new List<Employee>();

            pf.Count = await _db.Employees.Where(c => string.IsNullOrEmpty(filter) || string.Concat(c.LastName, c.FirstName).Contains(filter)).CountAsync();

            result = await _db.Employees.AsNoTracking().
                Where(c => string.IsNullOrEmpty(filter) || string.Concat(c.LastName, c.FirstName).Contains(filter)).OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un MemoryStream.
        /// </summary>
        /// <example>
        /// Para utilizarse en una acción de un Controller de ASP.NET MVC
        /// public FileStreamResult ReadImage(int id)
        /// {
        ///    return File(pB.ReadImageStream(id), "image/jpg");
        /// } 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemoryStream> GetFileStream(int id)
        {
            MemoryStream result = null;

            var image = await _db.Employees.Where(c => c.EmployeeId == id).
                Select(i => i.Photo).AsNoTracking().FirstOrDefaultAsync();

            if (image != null)
            {
                result = new MemoryStream(image);
            }

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un string en Base64.
        /// </summary>
        /// <example>
        /// Para utilizarse directamente en una vista de razor. 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFileBase64(int id)
        {
            string result = "";

            using (var ms = await GetFileStream(id))
            {
                if (ms != null)
                {
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    result = $"data:image/jpg;base64,{base64}";
                }
            }

            return result;
        }

        public Boolean EmployeeExists(int id)
        {
            return _db.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
