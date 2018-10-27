using Model.Dao;
using Model.EF;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            var currentPage = page ?? 1;
            var contentDao = new ContentDao();
            var model = contentDao.GetAllContent(searchString, currentPage, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        #region Create Content With New CategoryID
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = (UserLogin)Session[CommonConstant.USER_SESSION];
                var currentCulture = Session[CommonConstant.CurrentCulture];
                model.CreateBy = currentUser.Username;
                model.Language = currentCulture.ToString();
                new ContentDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }
        #endregion

        #region Edit Content By CategoryID
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var content = new ContentDao().GetByID(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = (UserLogin)Session[CommonConstant.USER_SESSION];
                var currentCulture = Session[CommonConstant.CurrentCulture];
                model.ModifiedBy = currentUser.Username;
                model.Language = currentCulture.ToString();
                new ContentDao().Edit(model);
                return RedirectToAction("Index");
            }
            SetViewBag(model.CategoryID);
            return View();
        }
        #endregion

        public void SetViewBag(long? selectedID = null)
        {
            var categoryDao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(categoryDao.GetAllCategory(), "ID", "Name", selectedID);
        }
    }
}