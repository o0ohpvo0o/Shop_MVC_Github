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

        public ActionResult CategoryField(long id, int page = 1, int pageSize = 2)
        {
            var category = new ProductCategoryDao().GetCategoryById(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductDao().GetAllProductByCategoryId(id, ref totalRecord, page, pageSize);

            ViewBag.TotalRecord = totalRecord;
            ViewBag.Page = page;
            ViewBag.MaxPage = 5;
            var totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;

            ViewBag.First = 1;
            ViewBag.Last = totalPage;

            ViewBag.NextPage = page + 1;
            ViewBag.PreviousPage = page - 1;

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