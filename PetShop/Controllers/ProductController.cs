using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class ProductController : Controller
    {
        private PetShopDBContext db = new PetShopDBContext();

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var model = db.Products.Find(id);
            return View(model);
        }
    }
}
