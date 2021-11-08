using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineWebApp.Models.AppModels
{
    public class Point
    {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PointID { get; set; }
		public double cost { get; set; }
		public int Item_Id { get; set; }

		public virtual Items Items { get; set; }
		public int point { get; set; }
		public bool payementstatues { get; set; }

		ApplicationDbContext db = new ApplicationDbContext();
		public decimal getCost()
		{
			var money = (from n in db.Items
						 where n.Item_Id == Item_Id
						 select n.ItemCost).SingleOrDefault();
			return (money);
		}
		public int CalcPoints()
		{
			if (cost <= 120)
			{
				point = +50;
			}
			else if (cost > 120 && cost <= 250)
			{
				point = +100;
			}
			else if (cost > 250 && cost <= 380)
			{
				point = +120;
			}
			else if (cost > 380)
			{
				point = +150;
			}

			payementstatues = true;
			db.SaveChanges();
			return point;
		}
	}
}
