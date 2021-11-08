using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineWebApp.Models;
using OnlineWebApp.Models.AppModels;
using ZXing;

namespace OnlineWebApp.Controllers
{
    public class QRCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QRCodes
        public ActionResult QRDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QRCode qRCodeModel = db.QRCodes.Find(id);
            if (qRCodeModel == null)
            {
                return HttpNotFound();
            }
            
            return View(qRCodeModel);
        }
    }
}
