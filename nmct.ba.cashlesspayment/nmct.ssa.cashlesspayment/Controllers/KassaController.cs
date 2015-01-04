using nmct.ssa.cashlesspayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Controllers
{
    public class KassaController : Controller
    {
        // GET: Kassa
        public ActionResult Index()
        {
            List<Kassa> reglist = new List<Kassa>();
            reglist = KassaDA.GetRegisters().OrderBy(reg => reg.RegisterName).ToList();
            return View(reglist);
        }
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Kassa reg = new Kassa();
            reg = KassaDA.GetKassaByID(Id.Value);

            if (reg != null)
            {
                return View(reg);
            }
            else
            {
                return RedirectToAction("ErrorNoOrganisation");
            }
        }
        [HttpGet]
        public ActionResult NewKassa()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewKassa(Kassa newkassa)
        {
            if (!ModelState.IsValid)
            {
                return View(newkassa);
            }
            Kassa nieuwekassa = new Kassa();
            nieuwekassa.RegisterName = Regex.Replace(newkassa.RegisterName, "/[^a-zA-Z0-9 ]/", "");
            nieuwekassa.Device = Regex.Replace(newkassa.Device, "/[^a-zA-Z0-9 ]/", "");
            nieuwekassa.PurchaseDate = newkassa.PurchaseDate;
            nieuwekassa.ExpiresDate = newkassa.ExpiresDate; 
            List<Kassa> reglist = new List<Kassa>();
            reglist = KassaDA.GetRegisters();
            int test = 0;
            foreach(Kassa reg in reglist)
            {
                if(reg.RegisterName.Equals(nieuwekassa.RegisterName))
                {
                    test = 1;
                }
            }
            if(test == 0)
            {
                KassaDA.InsertRegister(nieuwekassa);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Deze naam bestaat al";
                return View(newkassa);
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
            Kassa reg = new Kassa();
            reg = KassaDA.GetKassaByID(Id.Value);
            if (reg != null)
            {
                return View(reg);
            }
            else
            {
                return RedirectToAction("ErrorNoOrganisation");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(Kassa changedreg)
        {
            if (!ModelState.IsValid)
            {
                return View(changedreg);
            }
            Kassa reg = new Kassa();
            reg.ID = changedreg.ID;
            reg.RegisterName = Regex.Replace(changedreg.RegisterName, "/[^a-zA-Z0-9 ]/", " ");
            reg.Device = Regex.Replace(changedreg.Device, "/[^a-zA-Z0-9 ]/", " ");
            reg.PurchaseDate = changedreg.PurchaseDate;
            reg.ExpiresDate = changedreg.ExpiresDate;
            List<Kassa> kassalist = new List<Kassa>();
            kassalist = KassaDA.GetRegisters();
            int test = 0;
            foreach (Kassa register in kassalist)
            {
                if(!register.ID.Equals(reg.ID))
                {
                    if (register.RegisterName.Equals(reg.RegisterName))
                    {
                        test = 1;
                    }
                }
            }
            if(test == 0)
            {
                KassaDA.ChangeRegister(reg);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Deze naam bestaat al";
                return View(changedreg);
            }
        }
        
    }
}