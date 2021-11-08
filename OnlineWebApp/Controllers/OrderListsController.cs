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

namespace OnlineWebApp.Controllers
{
    public class OrderListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderLists
        //public ActionResult Index()
        //{
        //    var orderLists = db.OrderLists.Include(o => o.DriverInfos).Include(o => o.OrderDetails).Include(o => o.Staff);
        //    return View(orderLists.ToList());
        //}

        public ActionResult OrderDetails()
		{
            //var aa =db.OrderLists.Include(o=>o.OrderDetails.Items).Include(o=>o.OrderDetails.Order)
            var orderDetails = db.OrderDetails.Include(o => o.Items).Include(o => o.Order).Where(O => O.HasDriver == false && O.HasStaff == false && O.Order.Collected == false);
            return View(orderDetails.ToList());
        }
        //[Authorize(Roles ="Driver")]
        //public ActionResult DriverView()
        //{
        //    var uid = User.Identity.GetUserId();
        //    var orderLists = db.OrderLists.Include(o => o.DriverInfos).Include(o => o.OrderDetails).Include(o => o.Staff).Where(o => o.DriverID == uid || o.StaffID == uid);
        //    return View(orderLists.ToList());
        //}
        [Authorize(Roles = "Staff")]
        //public ActionResult StaffView()
        //{
        //    var uid = User.Identity.GetUserId();
        //    var orderLists = db.OrderLists.Include(o => o.DriverInfos).Include(o => o.OrderDetails).Include(o => o.Staff).Where(o=>o.DriverID == uid || o.StaffID == uid);
        //    return View(orderLists.ToList());
        //}
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
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // GET: OrderLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // GET: OrderLists/Create
        public ActionResult Create(int id)
        {
            var orderList = new OrderDetails
            {
                OrderDetail_Id = id
            };
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName");
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id");
            //ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffName");
            return View(orderList);
        }

        // POST: OrderLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderList_ID,OrderDetail_Id,DriverID,StaffID")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                var validate = (from v in db.OrderLists
                                where v.OrderList_ID == orderList.OrderList_ID
                                select v.OrderDetail_Id).FirstOrDefault();
                db.OrderLists.Add(orderList);
                db.SaveChanges();
                UpdataOrderDetails(validate);
                return RedirectToAction("OrderDetails", "OrderDetails") ;
            }

            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", orderList.DriverID);
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id", orderList.OrderDetail_Id);
            //ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffName", orderList.StaffID);
            return View(orderList);
        }
        public void UpdataOrderDetails(int validate)
        {

            OrderDetails orderDetails = (from v in db.OrderDetails
                                         where v.OrderDetail_Id == validate
                                         select v).FirstOrDefault();
            orderDetails.HasDriver = true;
            orderDetails.HasStaff = true;
            db.SaveChanges();
        }
        // GET: OrderLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", orderList.DriverID);
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id", orderList.OrderDetail_Id);
            //ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffName", orderList.StaffID);
            return View(orderList);
        }

        // POST: OrderLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderList_ID,OrderDetail_Id,DriverID,StaffID")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", orderList.DriverID);
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id", orderList.OrderDetail_Id);
            //ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffName", orderList.StaffID);
            return View(orderList);
        }

        // GET: OrderLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // POST: OrderLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderList orderList = db.OrderLists.Find(id);
            db.OrderLists.Remove(orderList);
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
