using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FashionShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["Today"] = 0;
            Application["Yesterday"] = 0;
            Application["ThisWeek"] = 0;
            Application["LastWeek"] = 0;
            Application["ThisMonth"] = 0;
            Application["LastMonth"] = 0;
            Application["Total"] = 0;
            Application["Visitors_online"] = 0;
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 150;
            Application.Lock();
            Application["Visitors_online"] = Convert.ToInt32(Application["Visitors_online"]);
            Application.UnLock();
            try
            {
                var item = FashionShop.Models.common.StatisticalAccess.Statistical();
                if (item != null)
                {
                    Application["Today"] = long.Parse("0" + item.Today.ToString("#, ###"));
                    Application["Yesterday"] = long.Parse("0" + item.Yesterday.ToString("#, ###"));
                    Application["ThisWeek"] = long.Parse("0" + item.ThisWeek.ToString("#, ###"));
                    Application["LastWeek"] = long.Parse("0" + item.LastWeek.ToString("#, ###"));
                    Application["ThisMonth"] = long.Parse("0" + item.ThisMonth.ToString("#, ###"));
                    Application["LastMonth"] = long.Parse("0" + item.LastMonth.ToString("#, ###"));
                    Application["Total"] = (int.Parse(item.Total.ToString())).ToString("#, ###");
                }
            }
            catch { }
        }

        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Visitors_online"] = Convert.ToInt32(Application["Visitors_online"]);
            Application.UnLock();
        }
    }
}
