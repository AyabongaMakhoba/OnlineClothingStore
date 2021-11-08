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
    public class DriverInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DriverInfoes
        public ActionResult Index()
        {
            return View(db.DriverInfos.ToList());
        }

        // GET: DriverInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverInfo driverInfo = db.DriverInfos.Find(id);
            if (driverInfo == null)
            {
                return HttpNotFound();
            }
            return View(driverInfo);
        }

        // GET: DriverInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverID,FirstName,LastName,TelNumber")] DriverInfo driverInfo)
        {
            if (ModelState.IsValid)
            {
                driverInfo.DriverID = User.Identity.GetUserId(); 
                db.DriverInfos.Add(driverInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driverInfo);
        }

        // GET: DriverInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverInfo driverInfo = db.DriverInfos.Find(id);
            if (driverInfo == null)
            {
                return HttpNotFound();
            }
            return View(driverInfo);
        }

        // POST: DriverInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverID,FirstName,LastName,TelNumber")] DriverInfo driverInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driverInfo);
        }

        // GET: DriverInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverInfo driverInfo = db.DriverInfos.Find(id);
            if (driverInfo == null)
            {
                return HttpNotFound();
            }
            return View(driverInfo);
        }

        // POST: DriverInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DriverInfo driverInfo = db.DriverInfos.Find(id);
            db.DriverInfos.Remove(driverInfo);
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
