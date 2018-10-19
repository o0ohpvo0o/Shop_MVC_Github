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

            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

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
               name: "Update Cart",
               url: "update-cart",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );

            routes.MapRoute(
              name: "Payment",
              url: "payment-details",
              defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
          );

            routes.MapRoute(
              name: "Payment Success",
              url: "payment-success",
              defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
          );

            routes.MapRoute(
              name: "Contact",
              url: "contact",
              defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
          );

            routes.MapRoute(
             name: "Register",
             url: "register",
             defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
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
