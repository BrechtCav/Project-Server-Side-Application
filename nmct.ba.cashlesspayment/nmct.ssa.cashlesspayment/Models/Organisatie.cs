using nmct.ba.demo.encryptie;
using nmct.ssa.cashlesspayment.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Models
{
    public class Organisatie
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Een login is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Login")]
        [AllowHtml]
        public string Login { get; set; }
        [Required(ErrorMessage = "Een paswoord is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [DisplayName("Paswoord")]
        [AllowHtml]
        public string Password { get; set; }
        [Required(ErrorMessage = "Een database naam is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Database Naam")]
        [AllowHtml]
        public string DbName { get; set; }
        [Required(ErrorMessage = "Een database login is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Database Login")]
        [AllowHtml]
        public string DbLogin { get; set; }
        [Required(ErrorMessage = "Een database paswoord is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [DisplayName("Database Paswoord")]
        [AllowHtml]
        public string DbPassword { get; set; }
        [Required(ErrorMessage = "Een organisatie naam is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Organisatie Naam")]
        [AllowHtml]
        public string OrganisationName { get; set; }
        [Required(ErrorMessage = "Een adres is nodig")]
        [DisplayName("Adres")]
        [StringLength(150, MinimumLength = 3)]
        [AllowHtml]
        public string Address { get; set; }
        [Required(ErrorMessage = "Een email is nodig")]
        [EmailAddress]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("E-Mail-adres")]
        [AllowHtml]
        public string Email { get; set; }
        [Required(ErrorMessage = "Een telefoonnr is nodig")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Telefoonnr.")]
        [AllowHtml]
        public string Phone { get; set; }
    }
}