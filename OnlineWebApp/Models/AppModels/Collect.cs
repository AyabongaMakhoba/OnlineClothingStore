using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models.AppModels
{
    public class Collect
    {
        [Key]
        [DisplayName("CollectionID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionID { get; set; }
        [DisplayName("Order")]
        public int Order_Id { get; set; }

        [DisplayName("First Name")]
        public string FName { get; set; }
        [DisplayName("Last Name")]
        public string LName { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Phone Number")]
        public string PNumber { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public void GetConfirm()
        {


            Order fumigation = (from c in db.Orders
                                where c.Order_Id == Order_Id
                                select c).Single();
            fumigation.Collected = true;
            db.SaveChanges();

        }

        public string GetPhoneNumber()
        {
            var name = (from c in db.Orders
                        where c.Order_Id == Order_Id
                        select c.Phone).FirstOrDefault();
            return name;
        }

        public string GetFName()
        {
            var name = (from c in db.Orders
                           where c.Order_Id == Order_Id
                           select c.FirstName).FirstOrDefault();
            return name;
        }

        public string GetLName()
        {
            var name = (from c in db.Orders
                        where c.Order_Id == Order_Id
                        select c.LastName).FirstOrDefault();
            return name;
        }

        public string GetAddress()
        {
            var name = (from c in db.Orders
                        where c.Order_Id == Order_Id
                        select c.Option).FirstOrDefault();
            return name;
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
    }
}