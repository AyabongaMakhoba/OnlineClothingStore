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
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Items).Include(o => o.Order);
            return View(orderDetails.ToList());
        }

        //public ActionResult SalesDates()
        //{
        //    var dates = 
        //}
        //[Authorize(Roles ="Admin")]
        public ActionResult OrderDetails()
		{
            var orderDetails = db.OrderDetails.Include(o => o.Items).Include(o => o.Order).Where(O=> O.HasDriver == false && O.HasStaff == false && O.Order.Collected==false);
            return View(orderDetails.ToList());
        }

        public ActionResult ViewPDF(int id)
        {
            var report = new Rotativa.ActionAsPdf("OrderState", new { id = id }) { FileName = "Invoice.pdf" };
            return report;

            //return new ViewAsPdf("Invoice", new { id = id });
        }

        // GET: OrderLists/Details/5
        public ActionResult OrderState(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderList = db.OrderDetails.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            Order order = (from v in db.Orders
                           where v.Order_Id == orderList.Order_Id
                           select v).FirstOrDefault();

            order.statues = "Is Packed";
            return View(orderList);
        }
        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name");
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetail_Id,Quantity,UnitPrice,Item_Id,Order_Id")] OrderDetails orderDetails, Items items)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", orderDetails.Item_Id);
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username", orderDetails.Order_Id);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", orderDetails.Item_Id);
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username", orderDetails.Order_Id);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetail_Id,Quantity,UnitPrice,Item_Id,Order_Id")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", orderDetails.Item_Id);
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username", orderDetails.Order_Id);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetails);
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
