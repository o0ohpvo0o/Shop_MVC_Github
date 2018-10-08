using Model.Dao;
using Model.EF;
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
        public ActionResult Index()
        {
            return View();
        }

        #region Create Content With New CategoryID
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag();
            return View();
        }
        #endregion

        #region Edit Content By CategoryID
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var contentDao = new ContentDao().GetByID(id);
            SetViewBag(contentDao.CategoryID);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {

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