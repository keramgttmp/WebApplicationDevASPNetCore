using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using System.Threading.Tasks;

namespace WA4.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        readonly NWContext db;
        public ProductViewComponent(NWContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await db.Products.
                SingleOrDefaultAsync(p => p.ProductId == id).ConfigureAwait(true);

            // La vista por defecto es Default.cshtml
            //return View(data);

            //return View("Product", data); //Estaba así y se ajustó

            //return View(data); se cambia luego a Links
            return View("Links", data);
        }
    }
}

