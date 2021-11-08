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
    [Bind(Exclude = "Item_Id")]
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Item ID")]
        public int Item_Id { get; set; }

        //[Required]
        [Display(Name = "Item View 1")]
        public byte[] Front_View { get; set; }
        [Display(Name = "Item View 2")]
        public byte[] Left_View { get; set; }
        [Display(Name = "Item View 3")]
        public byte[] Rght_View { get; set; }
    
        [Required]
        [Display(Name = "Item Name")]
        public string Item_Name { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Item Cost"), DataType(DataType.Currency)]
        public decimal ItemCost { get; set; }

        [Required]
        [Display(Name = "Quantity on hand")]
        public int QuantityOnHand { get; set; }

        [Display(Name = "Times Purchased")]
        public int TimesBought { get; set; }

        [Display(Name = "Category")]
        public int Category_Id { get; set; }
        public virtual Categories Categories{ get; set; }

        [Display(Name = "Tag ID")]
        public int Tag_Id { get; set; }
        public virtual Tag Tag { get; set; }

        [Display(Name = "Brand ID")]
        public int Brand_Id { get; set; }
        public virtual Brand Brand { get; set; }

        [Display(Name = "Sale ID")]
        public int Sale_Id { get; set; }
        public virtual Sales Sales { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        public virtual List<Discount> Discounts { get; set; }
        public virtual List<WishList> WishLists { get; set; }
        public virtual List<Point> Points { get; set; }
        public virtual List<RecievedStock> RecievedStocks { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public int Quantity()
        {
            var a = (from s in db.RecievedStocks
                     where s.Item_Id == Item_Id
                     select s.NumberOfStock).SingleOrDefault();
            return a;
        }
        public int total()
        {
            return QuantityOnHand + Quantity();
        }

    }
}