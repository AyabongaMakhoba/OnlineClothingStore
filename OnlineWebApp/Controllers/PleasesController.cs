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
    public class PleasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pleases
        public ActionResult Index()
        {
            var pleases = db.Pleases.Include(p => p.DriverInfos).Include(p => p.OrderDetails).Include(p => p.Solo);
            return View(pleases.ToList());
        }
        public ActionResult DriverView()
        {
            var uid = User.Identity.GetUserId();
            var orderLists = db.Pleases.Include(o => o.DriverInfos).Include(o => o.OrderDetails).Include(o => o.Solo);/*.Where(o => o.DriverID == uid || o.StaffID == uid);*/
            return View(orderLists.ToList());
        }

        public ActionResult StaffView()
        {
            var uid = User.Identity.GetUserId();
            var orderLists = db.Pleases.Include(o => o.DriverInfos).Include(o => o.OrderDetails).Include(o => o.Solo);/*.Where(o => o.DriverID == uid || o.StaffID == uid);*/
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
            Please orderList = db.Pleases.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            OrderDetails order = (from v in db.OrderDetails
                           where v.Order_Id == orderList.OrderDetails.Order_Id
                           select v).FirstOrDefault();

            order.Order.statues = "Is Packed";
            return View(orderList);
        }

        public ActionResult Test()
        {
           
            return View();
        }


        // GET: Pleases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Please please = db.Pleases.Find(id);
            if (please == null)
            {
                return HttpNotFound();
            }
            OrderDetails order = (from v in db.OrderDetails
                           where v.Order_Id == please.OrderDetails.Order_Id
                           select v).FirstOrDefault();

            order.Order.statues = "Delivery";
            return View(please);
        }

        // GET: Pleases/Create
        public ActionResult Create(int id)
        {
            var identity = new Please
            {
                OrderDetail_Id = id
            };
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName");
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id");
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName");
            return View(identity);
        }

        // POST: Pleases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PleaID,HasDriver,HasStaff,OrderDetail_Id,DriverID,SoloID")] Please please)
        {
           
            if (ModelState.IsValid)
            {
                db.Pleases.Add(please);
                db.SaveChanges();
                var validate = (from v in db.Pleases
                                where v.PleaID == please.PleaID
                                select v.OrderDetail_Id).FirstOrDefault();
                OrderDetails order = (from v in db.OrderDetails
                                      where v.Order_Id == please.OrderDetails.Order_Id
                                      select v).FirstOrDefault();

                order.Order.statues = "Packaging";
                UpdataOrderDetails(validate);
                return RedirectToAction("OrderDetails", "OrderDetails");
            }

            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", please.DriverID);
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id", please.OrderDetail_Id);
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName", please.SoloID);
            return View(please);
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

        // GET: Pleases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Please please = db.Pleases.Find(id);
            if (please == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", please.DriverID);
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id", please.OrderDetail_Id);
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName", please.SoloID);
            return View(please);
        }

        // POST: Pleases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PleaID,HasDriver,HasStaff,Order_Id,DriverID,SoloID")] Please please)
        {
            if (ModelState.IsValid)
            {
                db.Entry(please).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", please.DriverID);
            ViewBag.OrderDetail_Id = new SelectList(db.OrderDetails, "OrderDetail_Id", "OrderDetail_Id", please.OrderDetail_Id);
            ViewBag.SoloID = new SelectList(db.Solos, "SoloID", "StaffName", please.SoloID);
            return View(please);
        }

        // GET: Pleases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Please please = db.Pleases.Find(id);
            if (please == null)
            {
                return HttpNotFound();
            }
            return View(please);
        }

        // POST: Pleases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Please please = db.Pleases.Find(id);
            db.Pleases.Remove(please);
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
