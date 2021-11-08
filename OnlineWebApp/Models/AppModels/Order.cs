using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Web.Mvc;
using OnlineWebApp.Models.AppModels;

namespace OnlineWebApp.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order ID")]
        public int Order_Id { get; set; }
        [ScaffoldColumn(false)]
        public string Username { get; set; }
        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Cell Number:")]
        public string Phone { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        [Display(Name = "Order Date:")]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "Collection Date:")]
        public System.DateTime CollectionDate { get; set; }
        [Display(Name = "Received:")]
        public bool Collected { get; set; }
        [Display(Name = "Packed:")]
        public bool Packed { get; set; }

        public string statues { get; set; }
        public string ReferenceNumber { get; set; }
        public bool HasDriver { get; set; }
        public bool HasStaff { get; set; }
        public string Driver { get; set; }
        [ScaffoldColumn(false)]
        public string DID { get; set; }
        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Option { get; set; }

       
        //public int AddressID { get; set; }
        //public virtual AddressBook AddressBook { get; set; }

        //public int Item_Id { get; set; }
        //public virtual Items Items { get; set; }

        public string Client_Id { get; set; }
        public virtual Client Client { get; set; }

        public string DriverID { get; set; }
        public virtual DriverInfo DriverInfo { get; set; }
     







        public List<OrderDetails> OrderDetails { get; set; }


        public List<Return> Returns { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public void GetConfirm()
        {
            Order fumigation = (from c in db.Orders
                                select c).Single();
            fumigation.Packed = true;
            db.SaveChanges();

        }

        public string GetAddress()
        {
            return Option;
        }

        //public string GetDriverID()
        //{

        //};
    }
}
