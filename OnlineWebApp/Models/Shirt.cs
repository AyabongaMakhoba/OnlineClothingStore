using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebApp.Models
{
    public class Shirt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Shirt ID")]
        public int Shirt_Id { get; set; }
        [Required]
        [Display(Name = "Type of Shirt")]
        public string ShirtName { get; set; }
        public virtual List<Request> Request { get; set; }
    }
}