using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace OnlineWebApp.Models
{
	public class RecievedStock
	{
		[Key]
		public int StockID { get; set; }

		[Display(Name = "Item ID")]
		public int Item_Id { get; set; }
		public virtual Items Items { get; set; }
		public int SupplierID { get; set; }
		public virtual Supplier Supplier { get; set; }
		[Display(Name ="Please enter number of new stock recieved")]
		public int NumberOfStock { get; set; }
		
		public double NewTotalStock { get; set; }
		public DateTime StockDate { get; set; }

		ApplicationDbContext db = new ApplicationDbContext();
		public string getSupplier()
		{
			var Name = (from c in db.Suppliers
						where c.SupplierID == SupplierID
						select c.SupplierName).FirstOrDefault();
			return Name;
		}

		public string getName()
		{
			var Name = (from c in db.Items
						where c.Item_Id == Item_Id
						select c.Item_Name).FirstOrDefault();
			return Name;
		}
		public double getQuantity()
		{
			var quantity = (from a in db.Items
							where a.Item_Id == Item_Id
							select a.QuantityOnHand).FirstOrDefault();
			return quantity;
		}
		public double calcQuantity()
		{
			return NumberOfStock + getQuantity();
		}

	}
}