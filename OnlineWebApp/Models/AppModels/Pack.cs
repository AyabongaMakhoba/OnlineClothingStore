using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models.AppModels
{
    public class Pack
    {
        [Key]
        [DisplayName("PackingID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackingID { get; set; }
        [DisplayName("Order")]
        public int Order_Id { get; set; }
        //[DisplayName("Name")]
        //public string FirstName { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public void GetConfirm()
        {
            Order fumigation = (from c in db.Orders
                                          where c.Order_Id == Order_Id
                                          select c).SingleOrDefault();
            fumigation.Packed = true;
            db.SaveChanges();

        }

        public string GetEmail()
        {

            var Custid = (from c in db.Orders
                          where c.Order_Id == Order_Id
                          select c.Client_Id).Single();

            var email = (from c in db.Clients
                         where c.Client_Id == Custid
                         select c.Display_Name).Single();

            return email;
        }

        //public string GetName()
        //{
        //    var Name = (from c in db.Orders
        //                  where c.Order_Id == Order_Id
        //                  select c.FirstName).Single();

        //    return Name;
        //}
    }
}