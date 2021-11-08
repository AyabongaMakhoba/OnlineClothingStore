using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        [Required]

        [Display(Name = "Email Address ")]
        public string email { get; set; }


        [Display(Name = "Colour")]
        public int Colour_Id { get; set; }
        public virtual Colour Colour { get; set; }
        [Display(Name = "Type of the Shirt")]
        public int Shirt_Id { get; set; }
        public virtual Shirt Shirt { get; set; }
        [Display(Name = "Size")]
        public int Size_Id { get; set; }
        public virtual Size Size { get; set; }
        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 30, ErrorMessage = "Can only be between 0 .. 30")]

        public int quantity { get; set; }


        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        public string getColor()
        {
            var color = (from c in db.Colours
                         where c.Colour_Id == Colour_Id
                         select c.Colour_Name).Single();
            return color;
        }
        public string getSize()
        {
            var so = (from s in db.Sizes
                      where s.Size_Id == Size_Id
                      select s.mysize).Single();
            return so;
        }
        public string getShirt()
        {
            var we = (from d in db.Shirts
                      where d.Shirt_Id == Shirt_Id
                      select d.ShirtName).Single();
            return we;
        }
    }
}