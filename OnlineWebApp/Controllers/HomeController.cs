using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineWebApp.Models;


namespace OnlineWebApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult rate()
        {
            return View();
        }
        public ActionResult graph()
        {
            return View();
        }
        public ActionResult thanks()
        {
            return View();
        }
       

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Manage()
        {
            return View();
        }
  
        public ActionResult Index()
        {
            // Get most popular items
            var items = GetTopSellingItems(6);
            return View(items);
        }
        private List<Items> GetTopSellingItems(int count)
        {
            // Group the order details by item and return
            // the item with the highest count 

            return db.Items.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
        }
    }
}