using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DyMix.Models;
using DyMix.Contexts;

namespace DyMix.Controllers
{
    public class ContractorController : Controller
    {
        //
        // GET: /Contractor/

        public ActionResult Index()
        {
            List<ContractorModel> model = null;
            using (ContractorContext context = new ContractorContext())
            {
                model = context.GetContractors();
            }
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
            ContractorModel model = new ContractorModel();
            return View("EditCompany", model);
        }

        //
        // POST: /Contractor/NewCompany
        [HttpPost]
        public ActionResult NewCompany(ContractorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCompany", model);
            }
            else
            {
                using (ContractorContext context = new ContractorContext())
                {
                    model.ContractorTypeId = 2; // Юридическо лице
                    context.Add(model);
                }
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Contractor/Edit/Id
        public ActionResult Edit(int id = 0)
        {
            ContractorModel model = null;
            using (ContractorContext context = new ContractorContext())
            {
                model = context.GetContractor(id);
            }

            if (model == null)
            { return RedirectToAction("Index"); }
            // Физическо лице
            else if (model.ContractorTypeId == 1)
            { return View("EditPerson", model); }
            // // Юридическо лице
            else if (model.ContractorTypeId == 2)
            { return View("EditCompany", model); }
            else
            { return RedirectToAction("Index"); }
        }

        //
        // POST: /Contractor/Edit
        [HttpPost]
        public ActionResult Edit(ContractorModel model)
        {
            if (!ModelState.IsValid)
            {
                // Физическо лице
                if (model.ContractorTypeId == 1)
                { return View("EditPerson", model); }
                // // Юридическо лице
                else if (model.ContractorTypeId == 2)
                { return View("EditCompany", model); }
                else
                { return RedirectToAction("Index"); }
            }
            else
            {
                using (ContractorContext context = new ContractorContext())
                {
                    context.Edit(model);
                }
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Contractor/JList
        public JsonResult JList(string term = "")
        {
            List<ListItemModel> model = null;
            using (ContractorContext context = new ContractorContext())
            {
                model = context.GetList(term);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
