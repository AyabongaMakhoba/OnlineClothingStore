using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace OnlineWebApp.Models.AppModels
{
    public class DriverInfo
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DriverID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Telephone")]
        [DataType(DataType.PhoneNumber)]
        public string TelNumber { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        //public string GetDriverID()
        //{
        //    var id = (from i in db.UserInRole
        //              where i.RoleId == "3"
        //              select i.UserId).FirstOrDefault();
        //    return id;
        //}

    }
}