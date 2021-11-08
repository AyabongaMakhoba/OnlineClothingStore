using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebApp.Models
{
	public class Newsletter
	{
		public int NewsletterID { get; set; }
		[Display(Name = "Items Picture")]
		public byte[] Picture { get; set; }
		[Required]
		[Display(Name = "Items Name")]
		public string Item_Name { get; set; }
		[Required]
		[Display(Name = "Item Cost"), DataType(DataType.Currency)]
		public double ItemCost { get; set; }
		[Required]
		[Display(Name = "Items Decription")]
		public string Decription { get; set; }
		public int SubscriptionID { get; set; }
		public virtual Subscription Subscription { get; set; }
		ApplicationDbContext db = new ApplicationDbContext();
		public string getEmails()
		{
			var email = (from e in db.Subscriptions
						 where e.SubscriptionID == SubscriptionID
						 select e.Email).SingleOrDefault();
			return email;
		}


	}
}