using nmct.ba.demo.encryptie;
using nmct.ssa.cashlesspayment.Models;
using nmct.ssa.cashlesspayment.PresentationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Controllers
{
    [Authorize]
    public class OrganisatieController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewOrganisation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewOrganisation(PMOrganisatie neworg)
        {
            Organisatie NieuweOrganisatie = new Organisatie();
            NieuweOrganisatie.Login =  neworg.Login;
            NieuweOrganisatie.Password = neworg.Password;
            NieuweOrganisatie.DbName = neworg.DbName;
            NieuweOrganisatie.DbLogin = neworg.DbLogin;
            NieuweOrganisatie.DbPassword = neworg.DbPassword;
            NieuweOrganisatie.OrganisationName = neworg.OrganisationName;
            NieuweOrganisatie.Address = neworg.Address;
            NieuweOrganisatie.Email = neworg.Email;
            NieuweOrganisatie.Phone = neworg.Phone;
            Organisatie.InsertOrganisation(NieuweOrganisatie);

            return RedirectToAction("Index");
        }
    }
}

