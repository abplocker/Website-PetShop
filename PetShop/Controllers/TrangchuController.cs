using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class TrangchuController : Controller
    {
        private PetShopDBContext db = new PetShopDBContext();
        // GET: Trangchu
        public ActionResult Index()
        {
            var model = db.Categorys
                //.Where(c => c.Products.Count >= 4)
                .OrderBy(c => Guid.NewGuid()).ToList();
            return View(model);
        } // Index
    }
}