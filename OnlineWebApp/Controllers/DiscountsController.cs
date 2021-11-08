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
    public class DiscountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discounts
        public ActionResult Index()
        {
            var discounts = db.Discounts.Include(d => d.Items);
            return View(discounts.ToList());
        }

        // GET: Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: Discounts/Create
        public ActionResult Create(int id)
        {
            Discount obj = new Discount();
            obj.Item_Id = id;
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name");
            return View(obj);
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Discount_Id,Discount_Price,Item_Id")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discounts.Add(discount);
                db.SaveChanges();
                //var counts = db.Discounts.Count();
                //var Item = (from d in db.Discounts
                //               where d.Discount_Id == counts
                //               select d.Item_Id).SingleOrDefault();

                //var cost = (from d in db.Items
                //            where d.Item_Id==Item
                //            select d.ItemCost).SingleOrDefault();

                //if (cost > discount.Discount_Price)
                //    
                //else
                //return RedirectToAction("Create",new {id= Item});
                return RedirectToAction("Index", "Items");
            }

            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", discount.Item_Id);
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", discount.Item_Id);
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Discount_Id,Discount_Price,Item_Id")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", discount.Item_Id);
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
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
