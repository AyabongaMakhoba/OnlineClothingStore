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
    public class CollectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collects
        public ActionResult Index()
        {
            return View(db.Collects.ToList());
        }

        // GET: Collects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collect collect = db.Collects.Find(id);
            if (collect == null)
            {
                return HttpNotFound();
            }
            return View(collect);
        }

        // GET: Collects/Create
        public ActionResult Create()
        {
            ViewBag.Order_Id = new SelectList(db.Orders.Where(p => p.Packed == true).Where(p => p.Collected == false).Where(p => p.Option == "Collection"), "Order_Id", "Order_Id");
            return View();
        }

        // POST: Collects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionID,Order_Id")] Collect collect)
        {
            if (ModelState.IsValid)
            {
                string str = Request.Params["btn1"];
                if (str == "Check Order")
                {
                    collect.FName = collect.GetFName();
                    collect.LName = collect.GetLName();
                    collect.PNumber = collect.GetPhoneNumber();
                }
                else if (str == "Yes")
                {
                    collect.GetEmail();
                    collect.GetConfirm();
                    db.Collects.Add(collect);
                    OMail objmail = new OMail();
                    objmail.SendConfirmation(collect.GetEmail()) ;
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            ViewBag.Order_Id = new SelectList(db.Orders.Where(p => p.Packed == true).Where(p => p.Collected == false).Where(p => p.Option == "Collection"), "Order_Id", "Order_Id", collect.Order_Id);
            return View(collect);
        }

        public ActionResult Deliver()
        {
            var id = User.Identity.GetUserId();
            ViewBag.Order_Id = new SelectList(db.Orders.Where(p => p.Packed == true).Where(p => p.Collected == false).Where(p => p.Option != "Collection").Where(p => p.DID == id), "Order_Id", "Order_Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deliver([Bind(Include = "CollectionID,Order_Id")] Collect collect)
        {
            if (ModelState.IsValid)
            {
                string str = Request.Params["btn1"];
                if (str == "Check Order")
                {
                    collect.Address = collect.GetAddress();
                    collect.FName = collect.GetFName();
                    collect.LName = collect.GetLName();
                    collect.PNumber = collect.GetPhoneNumber();
                }
                else if (str == "Yes")
                {
                    collect.GetEmail();
                    collect.GetConfirm();
                    db.Collects.Add(collect);
                    db.SaveChanges();
                    return RedirectToAction("Deliver");
                }
                    
            }
            var id = User.Identity.GetUserId();
            ViewBag.Order_Id = new SelectList(db.Orders.Where(p => p.Packed == true).Where(p => p.Collected == false).Where(p => p.Option != "Collection").Where(p => p.DID == id), "Order_Id", "Order_Id", collect.Order_Id);
            //ViewBag.Order = "Name : " + collect.Order_Id;
            //return PartialView("Create");
            return View(collect);
        }

        // GET: Collects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collect collect = db.Collects.Find(id);
            if (collect == null)
            {
                return HttpNotFound();
            }
            return View(collect);
        }

        // POST: Collects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollectionID,Order_Id")] Collect collect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collect);
        }

        // GET: Collects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collect collect = db.Collects.Find(id);
            if (collect == null)
            {
                return HttpNotFound();
            }
            return View(collect);
        }

        // POST: Collects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collect collect = db.Collects.Find(id);
            db.Collects.Remove(collect);
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
