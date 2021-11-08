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
    public class RecievedStocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecievedStocks
        public ActionResult Index()
        {
            var recievedStocks = db.RecievedStocks.Include(r => r.Items).Include(r => r.Supplier);
            return View(recievedStocks.ToList());
        }

        // GET: RecievedStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedStock recievedStock = db.RecievedStocks.Find(id);
            if (recievedStock == null)
            {
                return HttpNotFound();
            }
            return View(recievedStock);
        }

        // GET: RecievedStocks/Create
        public ActionResult Create()
        {
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            return View();
        }

        // POST: RecievedStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,Item_Id,SupplierID,NumberOfStock,NewTotalStock,StockDate")] RecievedStock recievedStock)
        {
            if (ModelState.IsValid)
            {
                Items items = (from i in db.Items
                               where i.Item_Id == recievedStock.Item_Id
                               select i).FirstOrDefault();
                items.QuantityOnHand = (int)recievedStock.calcQuantity();
                recievedStock.NewTotalStock = recievedStock.calcQuantity();
                recievedStock.StockDate = DateTime.Now;
                db.RecievedStocks.Add(recievedStock);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", recievedStock.Item_Id);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", recievedStock.SupplierID);
            return View(recievedStock);
        }

        // GET: RecievedStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedStock recievedStock = db.RecievedStocks.Find(id);
            if (recievedStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", recievedStock.Item_Id);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", recievedStock.SupplierID);
            return View(recievedStock);
        }

        // POST: RecievedStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockID,Item_Id,SupplierID,NumberOfStock")] RecievedStock recievedStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recievedStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Name", recievedStock.Item_Id);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", recievedStock.SupplierID);
            return View(recievedStock);
        }

        // GET: RecievedStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedStock recievedStock = db.RecievedStocks.Find(id);
            if (recievedStock == null)
            {
                return HttpNotFound();
            }
            return View(recievedStock);
        }

        // POST: RecievedStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecievedStock recievedStock = db.RecievedStocks.Find(id);
            db.RecievedStocks.Remove(recievedStock);
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
