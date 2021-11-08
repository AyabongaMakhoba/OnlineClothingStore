using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebApp.Models
{
	public class JobPost
	{ 
		[Key]
		public int PostID { get; set; }
		[Display(Name ="Job Position")]
		public string JobPosition { get; set; }
		[Display(Name = "Qualification Required")]
		public string Qualification { get; set; }
		[Display(Name = "Job Description")]
		public string JobDescription { get; set; }
		[Display(Name = "Experience Required")]
		public int Experience { get; set; }
		[Display(Name = "Age")]
		public int Age { get; set; }
		[Display(Name = "Post Closing Date")]
		public string AppProcess { get; set; }

	}
}