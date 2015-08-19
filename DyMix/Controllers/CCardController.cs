using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DyMix.Models;
using DyMix.Contexts;

namespace DyMix.Controllers
{
    public class CCardController : Controller
    {
        //
        // GET: /CCard/

        public ActionResult Index()
        {
            List<CCardModel> model = null;
            using (CCardContext context = new CCardContext())
            {
                model = context.GetCards();
            }
            return View(model);
        }

        //
        // GET: /CCard/New/Id
        public ActionResult New()
        {
            CCardModel model = new CCardModel();
            return View("Edit", model);
        }

        //
        // POST: /CCard/New/Id
        [HttpPost]
        public ActionResult New(CCardModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            else
            {
                /*
                using (ContractorContext context = new ContractorContext())
                {
                    model.ContractorTypeId = 2; // Юридическо лице
                    context.Add(model);
                }*/
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /CCard/Edit/Id
        public ActionResult Edit(int id = 0)
        {
            CCardModel model = null;
            using (CCardContext context = new CCardContext())
            {
                model = context.GetCard(id);
            }
            return View("Edit", model);
        }

        //
        // POST: /CCard/Edit/Id
        [HttpPost]
        public ActionResult Edit(CCardModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            else
            {
                using (CCardContext context = new CCardContext())
                {
                    context.Edit(model);
                }
                return RedirectToAction("Index");
            }
        }
    }
}
