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
    public class StoreManegerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: StoreManeger
        public ViewResult Index()
        {
            var items = db.Items.Include(a => a.Categories);
            return View(items.ToList());
        }

        public ViewResult Details(int id)
        {
            Items item = db.Items.Find(id);
            return View(item);
        }

        public ActionResult Create()
        {
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Id");
        
            return View();
        }

        [HttpPost]
        public ActionResult Create(Items item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Id");
            return View(item);
        }

        public ActionResult Edit(int id)
        {
            Items item = db.Items.Find(id);
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Id", item.Category_Id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Items item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_Id = new SelectList(db.Items, "Item_Id", "Item_Id", item.Category_Id);
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            Items item = db.Items.Find(id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = db.Items.Find(id);
            db.Items.Remove(items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}