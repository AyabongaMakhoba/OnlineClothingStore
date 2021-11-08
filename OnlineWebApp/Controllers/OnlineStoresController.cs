using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineWebApp.Models;
using OnlineWebApp.Model.ViewModels;
using OnlineWebApp.Models.AppModels;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Net;

namespace OnlineWebApp.Controllers
{
    public class OnlineStoresController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Shop(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "Item Name" : "";
            ViewBag.Date = sortOrder == "Item Date" ? "Item Date" : "";
            ViewBag.Cost = sortOrder == "Item Cost" ? "Item Cost" : "";

            if (searchString != null) { page = 1; }
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;
 
            var items = from itm in db.Items select itm;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(itm => itm.Item_Name.ToUpper().Contains(searchString.ToUpper().Trim())
                 || itm.Tag.Tag_Name.ToUpper().Contains(searchString.ToUpper().Trim()) || itm.Brand.Tag_Name.ToUpper().Contains(searchString.ToUpper().Trim())
                 || itm.Categories.Category_Type.ToUpper().Contains(searchString.ToUpper().Trim()));
            }
            switch (sortOrder)
            {
                case "Item Name":
                    items = items.OrderByDescending(itm => itm.Item_Name);
                    break;

                case "Item Date":
                    items = items.OrderByDescending(itm => itm.DateCreated);
                    break;

                case "Item Cost":
                    items = items.OrderByDescending(itm => itm.ItemCost);
                    break;

                default:
                    items = items.OrderBy(itm => itm.DateCreated);
                    break;
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Sale()
        {
            var items = db.Discounts.Include("Items");
            return View(items.ToList());
        }

        public ActionResult Browse(string category)
        {
           
            var genreModel = db.Categories.Include("Items").Single(g => g.Category_Type == category);

            return View(genreModel);
        }

        public ActionResult BrowseTag(int id)
        {

            var tagModel = db.Tags.Include("Items").Single(g => g.Tag_Id == id);
        
            return View(tagModel);
        }

        public ActionResult BrowseBrands(int id)
        {

            var tagModel = db.Brands.Include("Items").Single(g => g.Brand_Id == id);

            return View(tagModel);
        }
        public ActionResult BrowseWish()
        {
            var uid = User.Identity.GetUserId();
            var WishModel = db.WishLists.Where(i => i.Client_Id == uid).ToList();

            return View(WishModel);
        }
       

        public ActionResult Details(int? id)
        {

            var item = db.Items.Find(id);
            return View(item);
        }
        public ActionResult AddToList(int id, string crumbPage)
        {
            var uid = User.Identity.GetUserId();
            var itemId = (from i in db.Items
                          where i.Item_Id == id
                          select i.Item_Id).Single();

            var ItemsList = (from c in db.WishLists
                             where c.Item_Id == itemId && c.Client_Id == uid
                             select c).SingleOrDefault();
            if (ItemsList == null)
            {
                ItemsList = new WishList
                {

                    Item_Id = itemId,
                    Client_Id = uid
                };
                db.WishLists.Add(ItemsList);

                Client ClientItemCount = (from w in db.Clients
                                          where w.Client_Id == uid
                                          select w).Single();
                ClientItemCount.Wish_count += 1;
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction(crumbPage, "OnlineStores");
            }
            db.SaveChanges();
            return RedirectToAction(crumbPage, "OnlineStores");
        }
        [ChildActionOnly]
        public ActionResult TotalItems()
        {
            var uid = User.Identity.GetUserId();


            var items = db.WishLists.Where(s => s.Client_Id == uid).Count();
            ViewBag.Count = items;
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult AllDepartments()
        {
            var genres = db.Categories.ToList();

            return PartialView(genres);
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = db.Categories.ToList();

            return PartialView(genres);
        }

        [ChildActionOnly]
        public ActionResult Tags()
        {
            var genres = db.Tags.ToList();

            return PartialView(genres);
        }

        [ChildActionOnly]
        public ActionResult Brands()
        {
            var genres = db.Brands.ToList();

            return PartialView(genres);
        }
     
        [ChildActionOnly]
        public ActionResult SearchPanel()
        {
            var genres = db.Items.ToList();

            return PartialView(genres);
        }
    }
}
