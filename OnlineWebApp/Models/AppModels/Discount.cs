using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Web.Mvc;

namespace OnlineWebApp.Models.AppModels
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Discount ID")]
        public int Discount_Id { get; set; }
        [Required]
        [Display(Name = "Discount Price"), DataType(DataType.Currency)]
        public decimal Discount_Price { get; set; }

        public int Item_Id { get; set; }
        public virtual Items Items { get; set; }
    }
}