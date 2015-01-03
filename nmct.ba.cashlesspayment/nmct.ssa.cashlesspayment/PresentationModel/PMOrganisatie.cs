using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlesspayment.PresentationModel
{
    public class PMOrganisatie
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "DbName")]
        public string DbName { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "DbLogin")]
        public string DbLogin { get; set; }
        
        [Required]
        [DataType(DataType.Password)]        
        [Display(Name = "DbPassword")]
        public string DbPassword { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Organisatie Naam")]
        public string OrganisationName { get; set;}
            
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }
}