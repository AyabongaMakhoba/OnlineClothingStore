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
    public class Client
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Client ID")]
        public string Client_Id { get; set; }
        [Display(Name = "Wishes")]
        public int Wish_count { get; set; }
        [Display(Name = "Display name")]
        public string Display_Name { get; set; }
        

        public List<Order>Orders{ get; set; }
        public List<Items>Items{ get; set; }
    }
}