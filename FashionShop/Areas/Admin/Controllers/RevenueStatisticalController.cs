using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    public class RevenueStatisticalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/RevenueStatistical
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStatistical (string fromDate, string toDate)
        {
            var query = from o in db.Order
                        join od in db.OrderDetail
                        on o.Id equals od.OrderId
                        join p in db.Product
                        on od.ProductId equals p.Id
                        select new { 
                            CreatedDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice
                        };
            try
            {
                if (!string.IsNullOrEmpty(fromDate))
                {
                    DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                    query = query.Where(x => x.CreatedDate >= startDate);
                }

                if (!string.IsNullOrEmpty(toDate))
                {
                    DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    query = query.Where(x => x.CreatedDate < endDate);
                }
            }
            catch (FormatException)
            {
                return Json(new { Data = "Invalid date format" }, JsonRequestBehavior.AllowGet);
            }
            

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price),
            }).Select(x => new { 
                Date = x.Date,
                Revenue = x.TotalSell,
                Profit = x.TotalSell -x.TotalBuy
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}