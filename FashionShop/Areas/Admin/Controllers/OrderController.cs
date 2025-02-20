﻿using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FashionShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]

    public class OrderController : Controller
    {
        // GET: Admin/Order

        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index(int ? page)
        {
            var items = db.Order.OrderByDescending(x => x.CreatedDate).ToList();
            if(page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult View(int id)
        {
            var item = db.Order.Find(id);
            return View(item);
        }

        public ActionResult Partial_Products(int id)
        {
            var items = db.OrderDetail.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int status)
        {
            var item = db.Order.Find(id);
            if(item != null)
            {
                db.Order.Attach(item);
                item.TypePayment = status;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }
    }
}