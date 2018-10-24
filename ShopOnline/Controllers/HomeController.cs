using Model.Dao;
using ShopOnline.Common;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().GetAll();
            var product = new ProductDao();
            ViewBag.NewProduct = product.GetNewProduct(4);
            ViewBag.FeatureProduct = product.GetFeatureProduct(4);
            //var userSession = Session[CommonConstant.USER_SESSION];
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600)]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}