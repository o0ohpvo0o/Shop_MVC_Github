using Model.Dao;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
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
            var cart = Session[CartSession];

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
                    Session[CartSession] = list;
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
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}