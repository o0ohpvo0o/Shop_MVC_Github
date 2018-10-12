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
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int? page, int pageSize = 10)
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
                    SetAlert("Create User Succeeded", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Create User failed", "warning");
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
                    SetAlert("Update User Succeeded", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Update User Failed", "error");
                    ModelState.AddModelError("", "Update Failed");
                }
            }
            return View("Index");
        }
        #endregion

        #region Delete user function
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            dao.Delete(id);
            SetAlert("Delete User Succeeded", "success");
            return RedirectToAction("Index","User");
        }
        #endregion

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var userStatus = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = userStatus
            });
        }
    }
}