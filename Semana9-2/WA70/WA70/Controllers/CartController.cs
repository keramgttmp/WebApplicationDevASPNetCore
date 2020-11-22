using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Data;
using WA70.Settings;

namespace WA70.Controllers
{
    public class CartController : Controller
    {
        private readonly NWContext _context;
        private readonly SessionSettings _ss;


        public CartController(NWContext context, SessionSettings ss)
        {
            _context = context;
            _ss = ss;
        }
        public IActionResult Index()
        {
            //Leemos lo que venga en sesion.
            ViewBag.welcome = HttpContext.Session.GetString("Welcome");
            return View(_ss.Cart);
        }


        public ActionResult AddToCart(int? id)
        {
            if (id.HasValue)
            {
                //#region Sesion
                var cart = _ss.Cart;
                //Podemo usar Single para buscar el registro o bien Find si estamos usando la llave primaria, 
                //que es el caso que usaremos.
                //cart.Items.Add(_context.Products.Single(p => p.ProductId == id));
                cart.Items.Add(_context.Products.Find(id));
                _ss.Cart = cart;
                //#endregion Sesion
            }
            return RedirectToAction("Index");
        }
    }
}
