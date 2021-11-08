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

namespace OnlineWebApp.Controllers
{
    public class SoliController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Soli
        public ActionResult Index()
        {
            return View(db.Solos.ToList());
        }

        // GET: Soli/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solo solo = db.Solos.Find(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        // GET: Soli/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Soli/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoloID,StaffName,StaffLastName,StaffAddress,StaffPhone")] Solo solo)
        {
            if (ModelState.IsValid)
            {
               
                db.Solos.Add(solo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(solo);
        }

        // GET: Soli/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solo solo = db.Solos.Find(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        // POST: Soli/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoloID,StaffName,StaffLastName,StaffAddress,StaffPhone")] Solo solo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solo);
        }

        // GET: Soli/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solo solo = db.Solos.Find(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        // POST: Soli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solo solo = db.Solos.Find(id);
            db.Solos.Remove(solo);
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
