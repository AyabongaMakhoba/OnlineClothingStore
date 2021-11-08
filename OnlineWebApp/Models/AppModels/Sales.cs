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
    public class Sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Sale ID")]
        public int Sale_Id { get; set; }
        [Required]
        [Display(Name = "Sale Name")]
        public string Sale_Name { get; set; }

        public virtual List<Items> Items { get; set; }
    }
}