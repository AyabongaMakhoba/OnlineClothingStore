using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebApp.Models.AppModels
{
    public class AssignDriver
    {
        [Key]
        [DisplayName("DeliveryID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliveryID { get; set; }
        [DisplayName("Driver")]
        [Required]
        public string DriverID { get; set; }
        public virtual DriverInfo DriverInfo { get; set; }
        [DisplayName("Order")]
        public int Order_Id { get; set; }
        public string Name { get; set; }


        ApplicationDbContext db = new ApplicationDbContext();

        public string GetName()
        {
            var name = (from c in db.DriverInfos
                        where c.DriverID == DriverID
                        select c.FirstName).FirstOrDefault();

            var surname = (from c in db.DriverInfos
                           where c.DriverID == DriverID
                           select c.LastName).FirstOrDefault();
            return name + " " + surname;
        }

        public string GetID()
        {
            var id = (from i in db.DriverInfos
                      where i.DriverID == DriverID
                      select i.DriverID).FirstOrDefault();
            return id;
        }

        public void GetConfirm()
        { 

            Order order = (from c in db.Orders
                                where c.Order_Id == Order_Id
                                select c).FirstOrDefault();
            order.Driver = GetName();
            db.SaveChanges();

        }

        public void MakeDriverID()
        {

            Order order = (from c in db.Orders
                           where c.Order_Id == Order_Id
                           select c).FirstOrDefault();
            order.DID = GetID();
            db.SaveChanges();
        }

        public string GetEmail()
        {

            var Custid = (from c in db.Orders
                          where c.Order_Id == Order_Id
                          select c.Client_Id).FirstOrDefault();
            var email = (from c in db.Clients
                         where c.Client_Id == Custid
                         select c.Display_Name).FirstOrDefault();

            return email;
        }
    }
}