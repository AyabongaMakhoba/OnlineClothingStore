using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineWebApp.Models;

namespace OnlineWebApp.Controllers
{
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Colour).Include(r => r.Shirt).Include(r => r.Size);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.Colour_Id = new SelectList(db.Colours, "Colour_Id", "Colour_Name");
            ViewBag.Shirt_Id = new SelectList(db.Shirts, "Shirt_Id", "ShirtName");
            ViewBag.Size_Id = new SelectList(db.Sizes, "Size_Id", "mysize");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,email,Colour_Id,Shirt_Id,Size_Id,quantity,Picture")] Request request, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                request.Picture = ConvertToBytes(filelist);
                db.Requests.Add(request);
                db.SaveChanges();
                Email objmail = new Email();
                objmail.SendConfirmation(request.email, request.getColor(), request.getShirt(), request.getSize(), request.quantity);
                return RedirectToAction("Index");
            }

            ViewBag.Colour_Id = new SelectList(db.Colours, "Colour_Id", "Colour_Name", request.Colour_Id);
            ViewBag.Shirt_Id = new SelectList(db.Shirts, "Shirt_Id", "ShirtName", request.Shirt_Id);
            ViewBag.Size_Id = new SelectList(db.Sizes, "Size_Id", "mysize", request.Size_Id);
            return View(request);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Colour_Id = new SelectList(db.Colours, "Colour_Id", "Colour_Name", request.Colour_Id);
            ViewBag.Shirt_Id = new SelectList(db.Shirts, "Shirt_Id", "ShirtName", request.Shirt_Id);
            ViewBag.Size_Id = new SelectList(db.Sizes, "Size_Id", "mysize", request.Size_Id);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,email,Colour_Id,Shirt_Id,Size_Id,quantity,Picture")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colour_Id = new SelectList(db.Colours, "Colour_Id", "Colour_Name", request.Colour_Id);
            ViewBag.Shirt_Id = new SelectList(db.Shirts, "Shirt_Id", "ShirtName", request.Shirt_Id);
            ViewBag.Size_Id = new SelectList(db.Sizes, "Size_Id", "mysize", request.Size_Id);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
