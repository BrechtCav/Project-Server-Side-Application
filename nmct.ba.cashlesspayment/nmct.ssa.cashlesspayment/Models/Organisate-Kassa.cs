using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Models
{
    public class Organisate_Kassa
    {
        [Required(ErrorMessage = "Een organisatie is nodig")]
        [DisplayName("Organisatie")]
        [AllowHtml]
        public Organisatie OrganisationID { get; set; }
        [Required(ErrorMessage = "Een kassa is nodig")]
        [DisplayName("Kassa")]
        [AllowHtml]
        public Kassa RegisterID { get; set; }
        [Required(ErrorMessage = "Een begindatum is nodig")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Begindatum")]
        [AllowHtml]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "Een einddatum is nodig")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Einddatum")]
        [AllowHtml]
        public DateTime UntilDate { get; set; }
    }
}