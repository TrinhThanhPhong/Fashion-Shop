using FashionShop.Models;
using FashionShop.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]

    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Products
        public ActionResult Index(int? page)
        {
            IEnumerable<Product> items = db.Product.OrderByDescending(x => x.Id);
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategory.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rdDefault)
        {
            if(ModelState.IsValid)
            {
                if(Images != null && Images.Count > 0)
                {
                    for(int i = 0; i < Images.Count; i++)
                    {
                        if(i + 1 == rdDefault[0])
                        {
                            model.ProductImage.Add(new ProductImage 
                            { 
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = FashionShop.Models.common.Filter.FilterChar(model.Title);
                }
                model.Quantity = model.SizeXS + model.SizeS + model.SizeM + model.SizeL + model.SizeXL + model.SizeXXL + model.SizeXXXL;
                db.Product.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategory.ToList(), "Id", "Title");
            return View(model);
        }

        public ActionResult Edit( int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategory.ToList(), "Id", "Title");
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(model.Id);
                if (item != null)
                {
                    item.Title = model.Title;
                    item.ProductType = model.ProductType;
                    item.ProductBrand = model.ProductBrand;
                    item.SeoTitle = string.IsNullOrEmpty(model.SeoTitle) ? model.Title : model.SeoTitle;
                    model.Alias = FashionShop.Models.common.Filter.FilterChar(model.Title);
                    item.Description = model.Description;
                    item.Detail = model.Detail;
                    item.Price = model.Price;
                    item.PriceSale = model.PriceSale;
                    item.OriginalPrice = model.OriginalPrice;
                    item.IsActive = model.IsActive;
                    item.IsHome = model.IsHome;
                    item.IsSale = model.IsSale;
                    item.ProductCategoryId = model.ProductCategoryId;
                    model.ModifiedDate = DateTime.Now;

                    item.SizeXS = model.SizeXS;
                    item.SizeS = model.SizeS;
                    item.SizeM = model.SizeM;
                    item.SizeL = model.SizeL;
                    item.SizeXL = model.SizeXL;
                    item.SizeXXL = model.SizeXXL;
                    item.SizeXXXL = model.SizeXXXL;

                    item.Quantity = item.SizeXS + item.SizeS + item.SizeM + item.SizeL + item.SizeXL + item.SizeXXL + item.SizeXXXL;

                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                db.Product.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }
            return Json(new { success = false });
        }
    }
}