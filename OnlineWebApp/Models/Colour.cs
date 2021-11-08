using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebApp.Models
{
    public class Colour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Colour ID")]
        public int Colour_Id { get; set; }
        [Required]
        [Display(Name = "Colour")]
        public string Colour_Name { get; set; }
       


        public virtual List<Request> Requests { get; set; }
    }
}