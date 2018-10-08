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
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int? page, int pageSize = 2)
        {
            var currentPage = page ?? 1;
            var userDao = new UserDao();
            var model = userDao.GetAllUsers(searchString, currentPage, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        #region Create Function
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                var encryptedPass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedPass;
                long userID = userDao.Insert(user);
                if (userID > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Insert user succeed");
                }
            }
            return View("Index");
        }
        #endregion

        #region Update User profile Function
        [HttpGet]
        public ActionResult Update(int id)
        {
            var user = new UserDao().GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                var encryptedPass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedPass;
                var updateStatus = userDao.Update(user);
                if (updateStatus)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Update Failed");
                }
            }
            return View("Index");
        }
        #endregion

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            dao.Delete(id);
            return View("Index");
        }
    }
}