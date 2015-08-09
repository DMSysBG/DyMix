using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DyMix.Models;

namespace DyMix.Controllers
{
    public class ContractorController : Controller
    {
        //
        // GET: /Contractor/

        public ActionResult Index()
        {
            List<ContractorModel> model = new List<ContractorModel>();
            return View(model);
        }

        //
        // GET: /Contractor/NewPerson
        public ActionResult NewPerson()
        {
            return View("EditPerson");
        }

        //
        // GET: /Contractor/NewCompany
        public ActionResult NewCompany()
        {
            return View("EditCompany");
        }

        //
        // GET: /Contractor/Edit/Id
        public ActionResult Edit(int id = 0)
        {
            return View("EditPerson");
        }
    }
}
