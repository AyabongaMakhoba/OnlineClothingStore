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
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Wish ID")]
        public int Wish_Id { get; set; }

        public int Item_Id { get; set; }
        public virtual Items Items { get; set; }
        public string Client_Id { get; set; }
        public virtual Client Client{ get; set; }

    }
}