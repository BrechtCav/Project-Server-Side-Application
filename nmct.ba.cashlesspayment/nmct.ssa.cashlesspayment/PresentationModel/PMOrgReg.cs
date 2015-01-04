using nmct.ssa.cashlesspayment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.PresentationModel
{
    public class PMOrgReg : Organisate_Kassa
    {
        public Organisatie org { get; set; }
        [DisplayName("Kassa")]
        [AllowHtml]
        public MultiSelectList Registers { get; set; }
        [Required(ErrorMessage = "Een begindatum is nodig")]
        [DisplayName("Begindatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [AllowHtml]

        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "Een einddatum is nodig")]
        [DisplayName("Einddatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [AllowHtml]
        public DateTime UntilDate { get; set; }
    }
}