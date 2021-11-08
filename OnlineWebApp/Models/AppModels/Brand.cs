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
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Brand ID")]
        public int Brand_Id { get; set; }
        [Required]
        [Display(Name = "Brand Name")]
        public string Tag_Name { get; set; }
    

        public virtual List<Items> Items { get; set; }
    }
}