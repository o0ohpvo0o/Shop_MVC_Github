using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int? page, int pageSize = 10)
        {
            var currentPage = page ?? 1;
            var contentDao = new ContentDao();
            var model = contentDao.GetAllContent(currentPage, pageSize);
            int totalRecord = 0;

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

        public ActionResult Details(long id)
        {
            var model = new ContentDao().GetByID(id);
            ViewBag.Tags = new ContentDao().GetListTagByContentId(id);
            return View(model);
        }

        public ActionResult Tag(string tagId, int? page, int pageSize = 10)
        {
            var currentPage = page ?? 1;
            var contentDao = new ContentDao();
            var model = contentDao.GetAllContentByTag(tagId, currentPage, pageSize);
            int totalRecord = 0;
            ViewBag.Tag = new ContentDao().GetTag(tagId);

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
    }
}