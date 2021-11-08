using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models.AppModels
{
	public class OrderList
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderList_ID { get; set; }
		public bool HasDriver { get; set; }
		public bool HasStaff { get; set; }


		public int OrderDetail_Id { get; set; }
		public virtual OrderDetails OrderDetails { get; set; }
		[Required(ErrorMessage = "Driver should be selected")]
		[Display(Name = "Driver")]
		public string DriverID { get; set; }
		public virtual DriverInfo DriverInfos { get; set; }
		[Required(ErrorMessage = "Staff should be selected")]
		[Display(Name = "Staff")]
	

		ApplicationDbContext db = new ApplicationDbContext();

		public void getId()

		{
			var od = (from c in db.OrderLists
					  where c.OrderDetail_Id == OrderDetail_Id
					  select c).SingleOrDefault();
		}




	}
}