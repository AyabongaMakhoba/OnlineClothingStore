using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;

namespace OnlineWebApp.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Item ID ")]
        public int OrderDetail_Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool HasDriver { get; set; }
        public bool HasStaff { get; set;}
        public int Item_Id { get; set; }
        public virtual Items Items{ get; set; }
        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }


        }
}   