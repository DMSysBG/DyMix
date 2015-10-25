using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DyMix.Contexts;
using DyMix.Models;

namespace DyMix.Controllers
{
    public class DiscountController : Controller
    {
        //
        // GET: /Discount/

        public ActionResult Index()
        {
            List<DiscountModel> model = null;
            using (DiscountContext context = new DiscountContext())
            {
                model = context.GetDiscounts();
            }
            return View(model);
        }

        //
        // GET: /Discount/New
        [HttpGet]
        public ActionResult New()
        {
            DiscountModel model = new DiscountModel()
            {
                DiscountId = 0,
                Name = "",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now
            };
            using (DiscountContext context = new DiscountContext())
            {
                ViewBag.DiscountKinds = context.GetDiscountKinds();
            }
            return View("Edit", model);
        }

        //
        // POST: /Discount/New
        [HttpPost]
        public ActionResult New(DiscountModel model)
        {
            if (!ModelState.IsValid)
            {
                using (DiscountContext context = new DiscountContext())
                {
                    ViewBag.DiscountKinds = context.GetDiscountKinds();
                }
                return View("Edit", model);
            }
            else
            {
                using (DiscountContext context = new DiscountContext())
                {
                    context.Add(model);
                }
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Discount/Edit
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            DiscountModel model = null;
            using (DiscountContext context = new DiscountContext())
            {
                model = context.GetDiscount(id);
                ViewBag.DiscountKinds = context.GetDiscountKinds();
            }
            return View(model);
        }

        //
        // POST: /Discount/Edit
        [HttpPost]
        public ActionResult Edit(DiscountModel model)
        {
            if (!ModelState.IsValid)
            {
                using (DiscountContext context = new DiscountContext())
                {
                    ViewBag.DiscountKinds = context.GetDiscountKinds();
                }
                return View("Edit", model);
            }
            else
            {
                using (DiscountContext context = new DiscountContext())
                {
                    context.Edit(model);
                }
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Discount/Groups

        public ActionResult Groups()
        {
            List<DiscountGroupModel> model = null;
            using (DiscountContext context = new DiscountContext())
            {
                model = context.GetDiscountGroups();
            }
            return View(model);
        }

        //
        // GET: /Discount/NewGroup
        [HttpGet]
        public ActionResult NewGroup()
        {
            DiscountGroupModel model = new DiscountGroupModel()
            {
                DiscountGroupId = 0,
                Name = "",
                DiscountList = ","
            };
            return View("EditGroup", model);
        }

        //
        // GET: /Discount/NewGroup
        [HttpPost]
        public ActionResult NewGroup(DiscountGroupModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditGroup", model);
            }
            else
            {
                using (DiscountContext context = new DiscountContext())
                {
                    int groupId = context.Add(model);
                    if (groupId > 0)
                    {
                        context.SetGroupItems(groupId, model.DiscountList.Trim(','));
                    }
                }
                return RedirectToAction("Groups");
            }
        }

        //
        // GET: /Discount/EditGroup
        [HttpGet]
        public ActionResult EditGroup(int id = 0)
        {
            DiscountGroupModel model = null; 
            using (DiscountContext context = new DiscountContext())
            {
                model = context.GetDiscountGroup(id);
            }
            return View("EditGroup", model);
        }

        //
        // POST: /Discount/Edit
        [HttpPost]
        public ActionResult EditGroup(DiscountGroupModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditGroup", model);
            }
            else
            {
                using (DiscountContext context = new DiscountContext())
                {
                    context.Edit(model);
                    context.SetGroupItems(model.DiscountGroupId, model.DiscountList.Trim(','));
                }
                return RedirectToAction("Groups");
            }
        }

        //
        // GET: /Discount/JFilterList
        public JsonResult JFilterList(string term = "")
        {
            List<ListItemModel> discounts = null;
            using (DiscountContext context = new DiscountContext())
            {
                discounts = context.GetFilterList(term);
            }
            return Json(discounts, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Discount/JList
        public JsonResult JList(string discountList = "")
        {
            List<ListItemModel> discounts = null;
            using (DiscountContext context = new DiscountContext())
            {
                discounts = context.GetList(discountList);
            }
            return Json(discounts, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Discount/JDiscountGroup
        [HttpPost]
        public ActionResult JDiscountGroup(int id = 0)
        {
            DiscountGroupModel model = null;
            using (DiscountContext context = new DiscountContext())
            {
                model = context.GetDiscountGroup(id);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Discount/JGroupDiscounts
        [HttpPost]
        public ActionResult JGroupDiscounts(int id = 0)
        {
            List<DiscountModel> model = null;
            using (DiscountContext context = new DiscountContext())
            {
                model = context.GetGroupDiscounts(id);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
