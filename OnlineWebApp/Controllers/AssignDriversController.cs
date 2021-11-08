using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineWebApp.Models;
using OnlineWebApp.Models.AppModels;
using Microsoft.AspNet.Identity;

namespace OnlineWebApp.Controllers
{
    public class AssignDriversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssignDrivers
        public ActionResult Index()
        {
            return View(db.AssignDrivers.ToList());
        }

        // GET: AssignDrivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignDriver assignDriver = db.AssignDrivers.Find(id);
            if (assignDriver == null)
            {
                return HttpNotFound();
            }
            return View(assignDriver);
        }

        // GET: AssignDrivers/Create
        public ActionResult Create()
        {
            ViewBag.Order_Id = new SelectList(db.Orders.Where(p => p.Packed == true).Where(p => p.Collected == false).Where(p => p.Driver == null).Where(p => p.Option != "Collection"), "Order_Id", "Order_Id");
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName");
            

            return View();
        }

        // POST: AssignDrivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeliveryID,DriverID,Order_Id")] AssignDriver assignDriver)
        {
            if (ModelState.IsValid)
            {
                assignDriver.GetConfirm();
                assignDriver.Name = assignDriver.GetName();
                assignDriver.GetEmail();
                assignDriver.GetID();
                assignDriver.MakeDriverID();
               // assignDriver.DriverID = assignDriver.GetID();
                db.AssignDrivers.Add(assignDriver);
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            ViewBag.Order_Id = new SelectList(db.Orders.Where(p => p.Packed == true).Where(p => p.Collected == false).Where(p => p.Driver == null).Where(p => p.Option != "Collection"), "Order_Id", "Order_Id", assignDriver.Order_Id);
            ViewBag.DriverID = new SelectList(db.DriverInfos, "DriverID", "FirstName", assignDriver.DriverID);
            return View(assignDriver);
        }

        // GET: AssignDrivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignDriver assignDriver = db.AssignDrivers.Find(id);
            if (assignDriver == null)
            {
                return HttpNotFound();
            }
            return View(assignDriver);
        }

        // POST: AssignDrivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryID,DriverID,Order_Id")] AssignDriver assignDriver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignDriver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assignDriver);
        }

        // GET: AssignDrivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignDriver assignDriver = db.AssignDrivers.Find(id);
            if (assignDriver == null)
            {
                return HttpNotFound();
            }
            return View(assignDriver);
        }

        // POST: AssignDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignDriver assignDriver = db.AssignDrivers.Find(id);
            db.AssignDrivers.Remove(assignDriver);
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
