using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WA70.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            //Leemos lo que venga en sesion.
            ViewBag.welcome = HttpContext.Session.GetString("welcome");
            return View();
        }


        public ActionResult AddToCart(int? id)
        {
            if (id.HasValue)
            {
                //#region Sesion
                //var cart = HttpContext.Session.GetObject<List<Data.Product>>("cart");
                //if (cart == null)
                //{
                //    cart = new List<Data.Product>();
                //}
                //HttpContext.Session.SetObject("cart", cart);
                //#endregion Sesion
            }
            return RedirectToAction("Index");
        }
    }
}
