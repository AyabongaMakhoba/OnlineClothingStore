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
    public class WantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wants
        public ActionResult Index()
        {
            var wants = db.Wants.Include(w => w.DriverInfos).Include(w => w.Order).Include(w => w.Solo);
            return View(wants.ToList());
        }
        public ActionResult Checking(int? id)
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
        public ActionResult DriverView()
        {
            var uid = User.Identity.GetUserId();
            var orderLists = db.Wants.Include(o => o.DriverInfos).Include(o => o.Order).Include(o => o.Solo);/*.Where(o => o.DriverID == uid || o.StaffID == uid);*/
            return View(orderLists.ToList());
        }

        public ActionResult StaffView()
        {
            var uid = User.Identity.GetUserId();
            var orderLists = db.Wants.Include(o => o.DriverInfos).Include(o => o.Order).Include(o => o.Solo);/*.Where(o => o.DriverID == uid || o.StaffID == uid);*/
            return View(orderLists.ToList());
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
            Want orderList = db.Wants.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            Order order = (from v in db.Orders
                                  where v.Order_Id == orderList.Order_Id
                                  select v).FirstOrDefault();

            order.statues = "Is Packed";
            db.SaveChanges();
            return View(orderList);
        }

        // GET: Wants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Want want = db.Wants.Find(id);
            if (want == null)
            {
                return HttpNotFound();
            }
            Order order = (from v in db.Orders
                                  where v.Order_Id == want.Order_Id
                                  select v).FirstOrDefault();

            order.statues = "Delivery";
            db.SaveChanges();
            return View(want);
        }

        // GET: Wants/Create
        public ActionResult Create(int id)
        {
            var identity = new Want
            {
                Order_Id = id
            };
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName");
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username");
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName");
            return View(identity);
        }

        // POST: Wants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WantId,HasDriver,HasStaff,Order_Id,DriverID,SoloID")] Want want)
        {
            if (ModelState.IsValid)
            {
               
                Order order = (from v in db.Orders
                               where v.Order_Id == want.Order_Id
                               select v).FirstOrDefault();

                order.statues = "Packaging";
               
                db.Wants.Add(want);
                db.SaveChanges();
                var validate = (from v in db.Wants
                                where v.WantId == want.WantId
                                select v.Order_Id).FirstOrDefault();
                UpdataOrderDetails(validate);
                return RedirectToAction("OrderDe","Orders");
            }

            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", want.DriverID);
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username", want.Order_Id);
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName", want.SoloID);
            return View(want);
        }

        public void UpdataOrderDetails(int validate)
        {

            Order orderDetails = (from v in db.Orders
                                  where v.Order_Id == validate
                                  select v).FirstOrDefault();
            orderDetails.HasDriver = true;
            orderDetails.HasStaff = true;
            db.SaveChanges();
        }
        // GET: Wants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Want want = db.Wants.Find(id);
            if (want == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", want.DriverID);
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username", want.Order_Id);
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName", want.SoloID);
            return View(want);
        }

        // POST: Wants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WantId,HasDriver,HasStaff,Order_Id,DriverID,SoloID")] Want want)
        {
            if (ModelState.IsValid)
            {
                db.Entry(want).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", want.DriverID);
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Username", want.Order_Id);
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName", want.SoloID);
            return View(want);
        }

        // GET: Wants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Want want = db.Wants.Find(id);
            if (want == null)
            {
                return HttpNotFound();
            }
            return View(want);
        }

        // POST: Wants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Want want = db.Wants.Find(id);
            db.Wants.Remove(want);
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
