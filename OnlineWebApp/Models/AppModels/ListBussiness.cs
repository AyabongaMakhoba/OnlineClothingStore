using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models.AppModels
{
    public class ListBussiness
    {
        ApplicationDbContext db = new ApplicationDbContext();
       
        public void AddToWish(Items items)
        {
           
            var ItemsList = (from c in db.WishLists
                             where c.Item_Id ==items.Item_Id 
                             select c).SingleOrDefault();

        }
    }
}