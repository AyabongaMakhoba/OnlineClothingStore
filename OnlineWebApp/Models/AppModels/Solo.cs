using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OnlineWebApp.Models.AppModels
{
    public class Solo
    {
        [Key]
        public int SoloID { get; set; }
		[Display(Name = "First Name")]
		public string StaffName { get; set; }
		[Display(Name = "Second Name")]

		public string StaffLastName { get; set; }
		[Display(Name = "Home Address")]

		public string StaffAddress { get; set; }
		[Display(Name = "Phone Number")]

		public string StaffPhone { get; set; }
		
	}
}