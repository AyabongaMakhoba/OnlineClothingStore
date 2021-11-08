using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models
{
	public class Supplier
	{
		[Key]
		public int SupplierID { get; set; }
		[Display(Name = "Supplier Name")]
		public string SupplierName { get; set; }
	
	
		
	}
}