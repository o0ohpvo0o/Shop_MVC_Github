using BotDetect.Web.Mvc;
using Model.Dao;
using Model.EF;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var userCount = dao.CheckUserExsit(model.Username);
                var emailCount = dao.CheckEmailExsit(model.Email);
                if (userCount)
                {
                    ModelState.AddModelError("", "Username is already exist");
                }
                else if (emailCount)
                {
                    ModelState.AddModelError("", "Email is already exist");
                }
                else
                {
                    var newUser = new User();
                    newUser.Username = model.Username;
                    newUser.Password = model.Password;
                    newUser.Email = model.Email;
                    newUser.CreateDate = DateTime.Now;
                    newUser.Phone = model.Phone;
                    newUser.Address = model.Address;
                    newUser.Status = true;

                    var res = dao.Insert(newUser);
                    if (res >= 0)
                    {
                        ViewBag.Success = "Register Success";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Register failed. Try again");
                    }
                }
            }
            return View(model);
        }
    }
}