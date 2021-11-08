using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebApp.Models.AppModels
{
    public class QRCode
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int QRId { get; set; }
        [Display(Name = "QRCode Text")]
        public string QRCodeText { get; set; }
        [Display(Name = "QRCode Image")]
        public string QRCodeImagePath { get; set; }
        public int Order_Id { get; set; }
        public virtual Order Orders { get; set; }
    }
}