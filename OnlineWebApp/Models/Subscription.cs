using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebApp.Models
{
	public class Subscription
	{
		[Key]
		public int SubscriptionID { get; set; }
		[Display(Name ="First Name")]
		public string Name { get; set; }
		[Display(Name = "Last Name")]
		public string Surname { get; set; }
		[Display(Name = "Email Address")]
		public string Email { get; set; }
	}
}