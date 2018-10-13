using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().GetAll();
            return PartialView(model);
        }

        public ActionResult CategoryField(long id)
        {
            var model = new ProductCategoryDao().GetCategoryById(id);
            return View(model);
        }

        public ActionResult ProductField(long id)
        {
            var model = new ProductDao().GetProductById(id);
            ViewBag.ProductCategory = new ProductCategoryDao().GetCategoryById(model.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().GetRelatedProducts(id);
            return View(model);
        }
    }
}