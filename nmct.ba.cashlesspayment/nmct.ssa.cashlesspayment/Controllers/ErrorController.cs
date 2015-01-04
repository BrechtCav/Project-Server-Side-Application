using nmct.ssa.cashlesspayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlesspayment.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            List<Errorlog> errorlist = new List<Errorlog>();
            errorlist = OrganisatieDA.GetLogs();
            ViewBag.List = errorlist;
            return View();
        }
    }
}