using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace OnlineWebApp.Models.AppModels
{
    public class Want
    {
        [Key]
     public int WantId { get; set; }

        public bool HasDriver { get; set; }
        public bool HasStaff { get; set; }


        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }
        [Required(ErrorMessage = "Driver should be selected")]
        [Display(Name = "Driver")]
        public string DriverID { get; set; }
        public virtual DriverInfo DriverInfos { get; set; }
        //[Required(ErrorMessage = "Staff should be selected")]
        //[Display(Name = "Staff")]
        public int SoloID { get; set; }
        public virtual Solo Solo { get; set; }
      
    }
}