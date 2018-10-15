using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "products/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "CategoryField", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Product Details",
                url: "details/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "ProductField", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Add To Cart",
                url: "add-to-cart",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
        }
    }
}
