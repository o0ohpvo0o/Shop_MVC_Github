using Model.Dao;
using Model.EF;
using ShopOnline.Areas.Admin.Common;
using ShopOnline.Areas.Admin.Models;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.Username);
                    var userSession = new UserLogin();

                    userSession.Username = user.Username;
                    userSession.UserID = user.ID;

                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Username is not existed");
                }
                else if( result == -1)
                {
                    ModelState.AddModelError("", "Your Account is blocked");
                }
                else if( result == -2)
                {
                    ModelState.AddModelError("", "Password is invalid");
                }
                else
                {
                    ModelState.AddModelError("", "Login failed");
                }
            }
            return View("Index");
        }
    }
}