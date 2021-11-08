using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OnlineWebApp.Models.AppModels
{
	public class Please
	{
		[Key]

		public int PleaID { get; set; }

		public bool HasDriver { get; set; }
		public bool HasStaff { get; set; }


		public int OrderDetail_Id { get; set; }
		public virtual OrderDetails OrderDetails { get; set; }
		[Required(ErrorMessage = "Driver should be selected")]
		[Display(Name = "Driver")]
		public string DriverID { get; set; }
		public virtual DriverInfo DriverInfos { get; set; }
		//[Required(ErrorMessage = "Staff should be selected")]
		//[Display(Name = "Staff")]
		public int SoloID { get; set; }
		public virtual Solo Solo { get; set; }
		ApplicationDbContext db = new ApplicationDbContext();
	}
}