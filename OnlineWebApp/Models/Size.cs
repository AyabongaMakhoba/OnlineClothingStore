using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models
{
    public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Size ID")]
        public int Size_Id { get; set; }
        [Required]
        [Display(Name = "Size")]
        public string mysize { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}