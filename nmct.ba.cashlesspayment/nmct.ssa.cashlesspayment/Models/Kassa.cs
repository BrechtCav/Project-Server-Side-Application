using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Models
{
    public class Kassa
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Een Kassanaam is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Kassa Naam")]
        [AllowHtml]
        public string RegisterName { get; set; }
        [Required(ErrorMessage = "Een toestel is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Toestel")]
        [AllowHtml]
        public string Device { get; set; }
        [Required(ErrorMessage = "Een aankoopdatum is nodig")]
        [DisplayName("Aankoopdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [AllowHtml]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Een vervaldatum is nodig")]
        [DisplayName("Vervaldatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [AllowHtml]
        public DateTime ExpiresDate { get; set; }
    }
}