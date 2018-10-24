using BotDetect.Web.Mvc;
using Facebook;
using Model.Dao;
using Model.EF;
using ShopOnline.Common;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

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
                    newUser.Password = Encryptor.MD5Hash(model.Password);
                    newUser.Email = model.Email;
                    newUser.CreateDate = DateTime.Now;
                    newUser.Phone = model.Phone;
                    if (!string.IsNullOrEmpty(model.Province))
                    {
                        newUser.Province = model.Province;
                        if (!string.IsNullOrEmpty(model.District))
                        {
                            newUser.District = model.District;
                            if (!string.IsNullOrEmpty(model.Precinct))
                            {
                                newUser.Precinct = model.Precinct;
                            }
                        }
                    }

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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetUserByUsername(model.Username);
                    var userSession = new UserLogin();

                    userSession.Username = user.Username;
                    userSession.UserID = user.ID;

                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Username is not existed");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Your Account is blocked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Password is invalid");
                }
                else
                {
                    ModelState.AddModelError("", "Login failed");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return Redirect("/");
        }

        public ActionResult LoginWithFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.Username = email;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreateDate = DateTime.Now;
                var resultInsert = new UserDao().InsertUserFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.Username = user.Username;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"/Assets/client/data/Provinces_Data.xml"));
            var xElement = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var listProvince = new List<Province>();
            foreach (var item in xElement)
            {
                var province = new Province();
                province.ID = item.Attribute("id").Value;
                province.Name = item.Attribute("value").Value;
                listProvince.Add(province);
            }
            return Json(new
            {
                data = listProvince,
                status = true,
            });
        }

        public JsonResult LoadDistrict(string provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"/Assets/client/data/Provinces_Data.xml"));
            var xElement = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province"
                            && x.Attribute("value").Value == provinceID);
            var xChildElement = xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district");
            var listDistrict = new List<District>();
            foreach (var item in xChildElement)
            {
                var district = new District();
                district.ID = item.Attribute("id").Value;
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = provinceID;
                listDistrict.Add(district);
            }
            return Json(new
            {
                data = listDistrict,
                status = true,
            });
        }

        public JsonResult LoadPrecinct(string provinceID, string districtID)
        {
            var xmlDoc = XElement.Load(Server.MapPath(@"/Assets/client/data/Provinces_Data.xml"));
            var xElement = xmlDoc.Elements("Item").Where(x => x.Attribute("type").Value == "province"
                            && x.Attribute("value").Value == provinceID);
            var xDistricts = xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district" && x.Attribute("value").Value == districtID);
            var xPrecinct = xDistricts.Elements("Item").Where(x => x.Attribute("type").Value == "precinct");

            var listPrecinct = new List<Precinct>();
            foreach (var item in xPrecinct)
            {
                var precinct = new Precinct();
                precinct.ProvinceID = provinceID;
                precinct.DistrictID = districtID;
                precinct.ID = item.Attribute("id").Value;
                precinct.Name = item.Attribute("value").Value;
                listPrecinct.Add(precinct);
            }

            return Json(new
            {
                data = listPrecinct,
                status = true,
            });
        }
    }
}