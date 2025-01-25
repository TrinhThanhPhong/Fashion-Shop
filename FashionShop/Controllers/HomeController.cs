using FashionShop.Models;
using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial_Subcribe()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Subcribe(Subcribe req)
        {
            if(ModelState.IsValid)
            {
                db.Subcribe.Add(new Subcribe { Email = req.Email, CreatedDate = DateTime.Now });
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subcribe", req);
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Refresh()
        {
            var item = new StatisticalModel();
            ViewBag.Visitors_online = HttpContext.Application["Visitors_online"];
            var hn = HttpContext.Application["Today"];
            item.Today = HttpContext.Application["Today"].ToString();
            item.Yesterday = HttpContext.Application["Yesterday"].ToString();
            item.ThisWeek = HttpContext.Application["ThisWeek"].ToString();
            item.LastWeek = HttpContext.Application["LastWeek"].ToString();
            item.ThisMonth = HttpContext.Application["ThisMonth"].ToString();
            item.LastMonth = HttpContext.Application["LastMonth"].ToString();
            item.Total = HttpContext.Application["Total"].ToString();

            return PartialView(item);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}