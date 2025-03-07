using FashionShop.Models;
using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]

    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.ProductCategory;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = FashionShop.Models.common.Filter.FilterChar(model.Title);
                db.ProductCategory.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.ProductCategory.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            var item = db.ProductCategory.Find(model.Id);
            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.Title = model.Title;
                item.ModifiedDate = DateTime.Now;
                item.Alias = FashionShop.Models.common.Filter.FilterChar(model.Title);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductCategory.Find(id);
            if (item != null)
            {
                db.ProductCategory.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}