using FashionShop.Models;
using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial_Setting()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            // Title
            SystemSetting set = null;
            var checkTitle = db.SystemSetting.FirstOrDefault( x => x.SettingKey.Contains("SettingTitle"));
            if(checkTitle == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                db.SystemSetting.Add(set);
            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;
            }

            //Logo
            var checkLogo = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                db.SystemSetting.Add(set);
            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;
            }

            // Email
            var Email = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (Email == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                db.SystemSetting.Add(set);
            }
            else
            {
                Email.SettingValue = req.SettingEmail;
                db.Entry(Email).State = System.Data.Entity.EntityState.Modified;
            }


            // Hotline
            var Hotline = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (Hotline == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotline;
                db.SystemSetting.Add(set);
            }
            else
            {
                Hotline.SettingValue = req.SettingHotline;
                db.Entry(Hotline).State = System.Data.Entity.EntityState.Modified;
            }


            // TitleSEO
            var TitleSEO = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (TitleSEO == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = req.SettingTitleSeo;
                db.SystemSetting.Add(set);
            }
            else
            {
                TitleSEO.SettingValue = req.SettingTitleSeo;
                db.Entry(TitleSEO).State = System.Data.Entity.EntityState.Modified;
            }


            // DesSEO
            var DesSEO = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (DesSEO == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = req.SettingDesSeo;
                db.SystemSetting.Add(set);
            }
            else
            {
                DesSEO.SettingValue = req.SettingDesSeo;
                db.Entry(DesSEO).State = System.Data.Entity.EntityState.Modified;
            }


            // KeySEO
            var KeySEO = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (KeySEO == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = req.SettingKeySeo;
                db.SystemSetting.Add(set);
            }
            else
            {
                KeySEO.SettingValue = req.SettingKeySeo;
                db.Entry(KeySEO).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return View("Partial_Setting");
         }
    }
}