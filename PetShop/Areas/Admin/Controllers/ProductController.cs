using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShop.Library;
using PetShop.Models;
using System.IO;
namespace PetShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private PetShopDBContext db = new PetShopDBContext();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var list = db.Products.Join(
                db.Categorys, 
                p => p.CategoryID,
                c => c.Id,
                (p, c) => new ProductCategory
                {
                    Id = p.Id,
                    CatId = p.CategoryID,
                    Name = p.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Metakey = p.Metakey,
                    Metadess = p.Metadess,
                    Img = p.Img,
                    Number = p.Number,
                    Price = p.Price,
                    Pricesale = p.Pricesale,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                    CatName = c.Name,
                }
                )
                .Where(m => m.Status != 0)
                .OrderByDescending(m => m.Created_At)
                .ToList();
            return View(list);
        } // Index
        public ActionResult Trash()
        {
            var list = db.Products.Where(m => m.Status == 0)
                .OrderBy(m => m.Id)
                .ToList();
            return View("Trash", list);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(product.Name);
                product.Slug = slug;
                product.Created_At = DateTime.Now;
                product.Created_By = int.Parse(Session["UserAdmin"].ToString());
                product.Updated_At = DateTime.Now;
                product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
                // Hình ảnh
                var Img = Request.Files["file_img"]; // Lấy ảnh ở bên create ấy
                string[] FileExtention = { ".jpg", ".png" }; // Loại file ảnh được thêm
                if (Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Upload file Ex: sanpham.jpg
                        string imgName = slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        product.Img = imgName; // lưu vào csdl
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Product"), imgName);
                        Img.SaveAs(PathImg); // lưu file lên server
                    }
                }
                //
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(product.Name);
                product.Slug = slug;
                product.Updated_At = DateTime.Now;
                product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
                // Hình ảnh
                var Img = Request.Files["file_img"]; // Lấy ảnh ở bên create ấy
                string[] FileExtention = { ".jpg", ".png" }; // Loại file ảnh được thêm
                if (Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Upload file Ex: sanpham.jpg
                        string imgName = slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        // Xoá hình
                        string DelPath = Path.Combine(Server.MapPath("~/Public/images/Product"), product.Img);
                        if (System.IO.File.Exists(DelPath))
                        {
                            System.IO.File.Delete(DelPath);
                        }
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Product"), imgName);
                        Img.SaveAs(PathImg); // lưu file lên server
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Chức năng các button


        // Thay đổi trạng thái sản phẩm
        public ActionResult Status(int id)
        {
            Product product = db.Products.Find(id);
            int status = (product.Status == 1) ? 2 : 1;
            product.Status = status;
            product.Updated_By = 1;
            product.Updated_At = DateTime.Now;
            // Câu lệnh cập nhật lại database
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Xoá vào thùng rác, thay đổi status về 0
        public ActionResult DelTrash(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = 0;
            product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
            product.Updated_At = DateTime.Now;
            // Câu lệnh cập nhật lại database
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","Product");
        }

        // Khôi phục về trạng thái 2
        public ActionResult ReTrash(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = 2;
            product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
            product.Updated_At = DateTime.Now;
            // Câu lệnh cập nhật lại database
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            // Quay về trang Trash (thực ra thì nó vân ở trang Trash thôi, dùng contronler Category này đây
            return RedirectToAction("Trash", "Product");
        }
    }
}
