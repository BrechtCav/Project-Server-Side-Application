using nmct.ba.demo.encryptie;
using nmct.ssa.cashlesspayment.Models;
using nmct.ssa.cashlesspayment.PresentationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Controllers
{
    [Authorize]
    public class OrganisatieController : Controller
    {
        public ActionResult Index()
        {
            List<Organisatie> Organisaties = new List<Organisatie>();
            Organisaties = OrganisatieDA.GetOrganisations().OrderBy(org => org.OrganisationName).ToList();
            return View(Organisaties);
        }
        public ActionResult Details(int? Id)
        {
             if(!Id.HasValue)
             {
                 return RedirectToAction("Index");
             }
            Organisatie org = new Organisatie();
            org = OrganisatieDA.GetOrganisationByid(Id.Value);
            ViewBag.orgreg = KassaDA.GetRegistersbyOrg();
            List<Errorlog> errorlist = new List<Errorlog>();
            errorlist = OrganisatieDA.GetLogsByOrg(Id.Value);
            ViewBag.Logs = errorlist;
            if(org != null)
            {
                return View(org);
            }
            else
            {
                return RedirectToAction("ErrorNoOrganisation");
            }
        }
        [HttpGet]
        public ActionResult Change(int? Id)
        {
            ViewBag.Error = "";
            if (!Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Organisatie org = new Organisatie();
            org = OrganisatieDA.GetOrganisationByid(Id.Value);
            if (org != null)
            {
                PMChangedOrganisation corg = new PMChangedOrganisation();
                corg.ID = org.ID;
                corg.Address = org.Address;
                corg.Email = org.Email;
                corg.Login = org.Login;
                corg.OrganisationName = org.OrganisationName;
                corg.Password = org.Password;
                corg.Phone = org.Phone;
                return View(corg);
            }
            else
            {
                return RedirectToAction("ErrorNoOrganisation");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(PMChangedOrganisation changedorg)
        {
            if (!ModelState.IsValid)
            {
                return View(changedorg);
            }
            PMChangedOrganisation changed = new PMChangedOrganisation();
            changed.ID = changedorg.ID;
            changed.Login = Regex.Replace(changedorg.Login, "/[^a-zA-Z0-9 ]/", " ");
            changed.Password = Regex.Replace(changedorg.Password, "/[^a-zA-Z0-9 ]/", " ");
            changed.OrganisationName = Regex.Replace(changedorg.OrganisationName, "/[^a-zA-Z0-9 ]/", " ");
            changed.Address = Regex.Replace(changedorg.Address, "/[^a-zA-Z0-9 ]/", " ");
            changed.Email = Regex.Replace(changedorg.Email, "/[[A-Z0-9a-z.!#$%&'*+-/=?^_`{|}~]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}]/", " ");
            changed.Phone = Regex.Replace(changedorg.Phone, "/[^a-zA-Z0-9 ]/", " "); 
            List<Organisatie> Organisaties = new List<Organisatie>();
            Organisaties = OrganisatieDA.GetOrganisations();
            int test = 0;
            foreach (Organisatie org in Organisaties)
            {
                if (org.OrganisationName.Equals(changed.OrganisationName))
                {
                    test = 2;
                }
                if (org.Login.Equals(changed.Login))
                {
                    test = 1;
                }
            }
            if(test == 0)
            {
                OrganisatieDA.ChangeOrganisation(changed);
                return RedirectToAction("Index");
            }
            else if(test == 1)
            {
                ViewBag.Error = "Deze login bestaat al";
                return View(changedorg);
            }
            else if (test == 2)
            {
                ViewBag.Error = "Deze Organisatienaam bestaat al";
                return View(changedorg);
            }
            return RedirectToAction("Index");
            
        }
        public ActionResult ErrorNoOrganisation()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewOrganisation()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrganisation(Organisatie neworg)
        {
            if (!ModelState.IsValid)
            {
                return View(neworg);
            }
            Organisatie NieuweOrganisatie = new Organisatie();
            NieuweOrganisatie.Login = Regex.Replace(neworg.Login, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.Password = Regex.Replace(neworg.Password, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.DbName = Regex.Replace(neworg.DbName, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.DbLogin = Regex.Replace(neworg.DbLogin, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.DbPassword = Regex.Replace(neworg.DbPassword, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.OrganisationName = Regex.Replace(neworg.OrganisationName, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.Address = Regex.Replace(neworg.Address, "/[^a-zA-Z0-9 ]/", "");
            NieuweOrganisatie.Email = Regex.Replace(neworg.Email, "/[[A-Z0-9a-z.!#$%&'*+-/=?^_`{|}~]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}]/", "");
            NieuweOrganisatie.Phone = Regex.Replace(neworg.Phone, "/[^a-zA-Z0-9 ]/", "");
            List<Organisatie> Organisaties = new List<Organisatie>();
            Organisaties = OrganisatieDA.GetOrganisations();
            int test = 0;
            foreach(Organisatie org in Organisaties)
            {
                if (org.OrganisationName.Equals(NieuweOrganisatie.OrganisationName))
                {
                    test = 4;
                }
                if (org.Login.Equals(NieuweOrganisatie.Login))
                {
                    test = 3;
                }
                if (org.DbName.Equals(NieuweOrganisatie.DbName))
                {
                    test = 2;
                }
                if (org.DbLogin.Equals(NieuweOrganisatie.DbLogin))
                {
                    test = 1;
                }
            }
            if(test == 0)
            {
                OrganisatieDA.InsertOrganisation(NieuweOrganisatie);
                return RedirectToAction("Index");
            }
            else if(test == 1)
            {
                ViewBag.Error = "Deze database login bestaat al";
                return View(neworg);
            }
            else if (test == 2)
            {
                ViewBag.Error = "Deze database naam bestaat al";
                return View(neworg);
            }
            else if (test == 3)
            {
                ViewBag.Error = "Deze login bestaat al";
                return View(neworg);
            }
            else if (test == 4)
            {
                ViewBag.Error = "Deze organisatie bestaat al";
                return View(neworg);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult AddKassa(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }
            Organisatie org = new Organisatie();
            org = OrganisatieDA.GetOrganisationByid(Id.Value);
            PMOrgReg orgreg = new PMOrgReg();
            orgreg.org = org;
            orgreg.Registers = new MultiSelectList(KassaDA.GetRegisters(), "ID", "RegisterName", "Device");
            return View(orgreg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddKassa(Organisatie orgID, Kassa registerID, DateTime FromDate, DateTime UntilDate)
        {
            Organisate_Kassa reg = new Organisate_Kassa();
            reg.OrganisationID = OrganisatieDA.GetOrganisationByid(orgID.ID);
            reg.RegisterID = KassaDA.GetKassaByID(registerID.ID);
            reg.FromDate = FromDate;
            reg.UntilDate = UntilDate;
            KassaDA.AddRegisterToTable(reg);
            KassaDA.AddRegisterToDatabase(reg);
            return RedirectToAction("Index");
        }
    }
}

