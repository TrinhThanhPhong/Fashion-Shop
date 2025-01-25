using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FashionShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new { controller = "contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "CheckOut",
                url: "payment",
                defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "ShoppingCart",
                url: "cart",
                defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );
            routes.MapRoute(
                name: "CategoryProducts",
                url: "product-categories/{alias}-{id}",
                defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "DetailProduct",
                url: "detail-product/{alias}-p{id}",
                defaults: new { controller = "Products", action = "Detail", alias = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );
            routes.MapRoute(
                name: "Products",
                url: "products",
                defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );
            
            routes.MapRoute(
                name: "NewsList",
                url: "news",
                defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "DetailNew",
                url: "{alias}-n{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );
        }
    }
}
