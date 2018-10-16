using Model.Dao;
using Model.EF;
using ShopOnline.Common;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDao().GetProductById(productId);
            var cart = Session[CommonConstant.CartSession];

            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId)) // Check Cart if exist item already
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity; // Sum duplicate item in cart
                        }
                    }
                }
                else
                {
                    // Create new Cart item if item not existed in Cart
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                    Session[CommonConstant.CartSession] = list;
                }
            }
            else
            {
                // Create new CartItem
                var list = new List<CartItem>();
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                list.Add(item);

                // Set cart into session
                Session[CommonConstant.CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstant.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new
            {
                status = true,
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstant.CartSession] = null;
            return Json(new
            {
                status = true,
            });
        }

        public JsonResult DeleteItems(long id)
        {
            var cartSession = (List<CartItem>)Session[CommonConstant.CartSession];
            cartSession.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstant.CartSession] = cartSession;
            return Json(new
            {
                status = true,
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CommonConstant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string name, string address, string email, string phone)
        {
            var order = new Order();
            order.CreateDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipName = name;
            order.ShipEmail = email;
            order.ShipMobile = phone;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CommonConstant.CartSession];
                var detailDao = new OrderDetailDao();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.OrderID = id;

                    var orderConfirm = detailDao.Insert(orderDetail);
                    if (orderConfirm)
                    {
                        ViewBag.ValidateOrder = true;
                    }
                    else
                    {
                        ViewBag.ValidateOrder = false;
                    }
                }
            }
            catch (Exception)
            {
                // write log
                return Redirect("/payment-failed");
            }
            return Redirect("/payment-success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }
    }
}