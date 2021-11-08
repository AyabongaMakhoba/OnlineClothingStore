using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineWebApp.Models;
using OnlineWebApp.Models.AppModels;
using OnlineWebApp.Models.ViewModels;

namespace OnlineWebApp.Controllers.StoreControllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var cart = Business_Logics.GetCart(this.HttpContext); 
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        } 
            public ActionResult AddToCart(int id)
            {
                var addedItem = db.Items.Single(item => item.Item_Id == id);
                var cart = Business_Logics.GetCart(this.HttpContext); 
                cart.AddToCart(addedItem);
                return RedirectToAction("Shop", "OnlineStores");
            } 

   
            public ActionResult RemoveFromCart(int id)
            {             
                var cart = Business_Logics.GetCart(this.HttpContext);
            string ItemName = db.Carts.Single(item => item.RecordId == id).Items.Item_Name;
            int itemCount = cart.RemoveFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(ItemName) + " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return RedirectToAction("Index");
            }
        [ChildActionOnly]
         public ActionResult CartSummary()
         {
                var cart = Business_Logics.GetCart(this.HttpContext);

                ViewData["CartCount"] = cart.GetCount();

                return PartialView("CartSummary");
         }
        [ChildActionOnly]
        public ActionResult OrderdList()
        {
            var cart = Business_Logics.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {


                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return PartialView(viewModel);
        }
        [ChildActionOnly]
        public ActionResult HoverCart()
        {
            var cart = Business_Logics.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return PartialView(viewModel);
        }

        [ChildActionOnly]
        public ActionResult TotalAmount()
        {
            var cart = Business_Logics.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return PartialView(viewModel);
        }
        [ChildActionOnly]
        public ActionResult OrderedItems()
        {
            var cart = Business_Logics.GetCart(this.HttpContext);

            ViewBag.Items = cart.GetCount();
            
            return PartialView();
           
        }
    }
}