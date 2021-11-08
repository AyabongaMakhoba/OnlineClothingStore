using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebApp.Models.AppModels
{
    public class Bruv
    {
        [Key]
        public int BruID { get; set; }
        public int Number { get; set; }
        public string Comment { get; set; }
        public string what { get; set; }
        public int OrderDetail_Id { get; set; }

    }
}