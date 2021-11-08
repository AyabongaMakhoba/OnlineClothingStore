using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineWebApp.Models;
using Microsoft.AspNet.Identity;
using OnlineWebApp.Models.AppModels;


namespace OnlineWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var orders1 = db.Orders.Where(c => c.Collected == false).Include(o => o.Client);
                return View(orders1.ToList());
            }
            var id = User.Identity.GetUserId();
            var orders = db.Orders.Include(c => c.Client).Where(x => x.Client_Id == id).Where(c => c.Collected == false);
            return View(orders.ToList());
        }

        //public ActionResult Deliver([Bind(Include ="Collected")] Order order)
        //{
        //   // Order order = new Order();
        //    {
        //        order.Collected = true;
        //        db.Orders.Add(order);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //}

        //public ActionResult ViewOrders(int? DetailID)
        //{
        //    var viewModel = new DailySales();
        //    viewModel.orders = db.Orders
        //        .Include(i => i.OrderDate)
        //        .Include(i => i.OrderDetails.Select(c => c.Quantity))
        //        .OrderBy(i => i.OrderDate);

        //    if (DetailID != null)
        //    {
        //        ViewBag.OrderID = DetailID.Value;
        //        viewModel.orderDetails = viewModel.orders.Where(i => i.Order_Id == DetailID.Value).Single();
        //    }
        //}
        public ActionResult OrderDe()
        {
            var orderDetails = db.Orders.Where(O => O.HasDriver == false && O.HasStaff == false && O.Collected == false);
            return View(orderDetails.ToList());
        }

        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
       
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);

        }

       
       
        public ActionResult Checking()
        {
            var id = User.Identity.GetUserId();
           
            var orders = db.Orders.Include(c => c.Client).Where(o => o.Client_Id == id).Where(o=>o.statues!="Complete");
            return View(orders.ToList());

        }
        //public ActionResult OrderDe()
        //{
        //    var orderDetails = db.Orders.Where(O => O.HasDriver == false && O.HasStaff == false && O.Collected == false);
        //    Order order = (from v in db.Orders
        //                          where v.Order_Id ==Order_Id
        //                          select v).FirstOrDefault();
        //    return View(orderDetails.ToList());
        //}
        public ActionResult SearchRef(string searchString)

        {
            var items = from s in db.Orders
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ReferenceNumber.Contains(searchString));
                return RedirectToAction("OrderDetails",new { id =items.Select(e=>e.Order_Id)});
            }

            return View();
        }


        public ActionResult DailySales()
        {
            var dates = db.Orders.Where(c => c.Collected == true).Select(d => d.OrderDate).Distinct().ToList();
            return View(dates.ToList());
        }

        public ActionResult Orders()
        {
            if (User.IsInRole("Admin"))
            {
                var orders1 = db.Orders.Where(c => c.Collected == true);
                return View(orders1.ToList());
            }
            var id = User.Identity.GetUserId();
            var orders = db.Orders.Include(c => c.Client).Where(x => x.Client_Id == id).Where(c => c.Collected == true);
            return View(orders.ToList());
        }

        public ActionResult Delivered()
        {
            if (User.IsInRole("Admin"))
            {
                var orders1 = db.Orders.Where(c => c.Collected == true);
                return View(orders1.ToList());
            }
            else if (User.IsInRole("Drivers"))
            {
                var id = User.Identity.GetUserId();
                var orders = db.Orders.Include(c => c.Client).Where(x => x.Client_Id == id).Where(c => c.Collected == true);
                return View(orders.ToList());
            }
            var id2 = User.Identity.GetUserId();
            var orders2 = db.Orders.Include(c => c.Client).Where(x => x.Client_Id == id2).Where(c => c.Collected == true);
            return View(orders2.ToList());
        }

        public ActionResult Deliveries()
        {
            
                var orders = db.Orders.Where(c => c.Option != "Collection");
                return View(orders.ToList());
           
        }
        public ActionResult Complete(int id)
        {
            Order complete = (from i in db.Orders
                            where i.Order_Id == id
                            select i).FirstOrDefault();
            complete.statues = "Complete";
            EComplete obj = new EComplete();
            obj.SendConfirmation(complete.Email);
            db.SaveChanges();
            return RedirectToAction("DriversView", "Wants");


        }
                // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.Client_Id = new SelectList(db.Clients, "Client_Id", "Display_Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,Username,FirstName,LastName,Phone,Total,OrderDate,CollectionDate,Collected,Packed,Email,Client_Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client_Id = new SelectList(db.Clients, "Client_Id", "Display_Name", order.Client_Id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_Id = new SelectList(db.Clients, "Client_Id", "Display_Name", order.Client_Id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Id,Username,FirstName,LastName,Phone,Total,OrderDate,CollectionDate,Collected,Packed,Email,Client_Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_Id = new SelectList(db.Clients, "Client_Id", "Display_Name", order.Client_Id);
            return View(order);
        }

        public ActionResult Collect(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Collect([Bind(Include = "Order_Id,FirstName,LastName,Phone,OrderDate,CollectionDate,Collected,Packed,Email")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CollectionDate = DateTime.Now;

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
