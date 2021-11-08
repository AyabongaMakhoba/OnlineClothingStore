using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebApp.Models
{
	public class Return
	{
        [Key]
        public int Rid { get; set; }
        [Display(Name = "Please enter your order number")]
        [Range(1, 1000)]
        public int Order_Number { get; set; }

        [Display(Name = "State Reason for return")]
        [Required(ErrorMessage = "Please enter your reason")]
        public string Reason { get; set; }
        [Display(Name = "Number of Days Item recieved")]
        [Range(1, 31)]
        [Required(ErrorMessage = "If you have had an item more than 31 days then you can no longer return the item")]
        public int NumberofDays { get; set; }
        public bool Approved { get; set; }

        public bool Disapproved { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter your Email Address ")]
        public string email { get; set; }

        [Display(Name = "Date Query Created")]
        public DateTime Date { get; set; }

        public string Client_Id { get; set; }
        public virtual Client Client { get; set; }
        //public int Order_Id { get; set; }
        //public virtual Order Order { get; set; }

    }
}