using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DyMix.Contexts;
using DyMix.Models;

namespace DyMix.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                AccountModel account = null;
                try
                {
                    using (AccountContext context = new AccountContext())
                    {
                        account = context.Login(model.UserName, model.Password);
                    }
                }
                catch
                { }
                if (account == null)
                {
                    ModelState.AddModelError("", Resources.Account.Message_WrongLogin);
                    return View(model);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    xSession.AccountId = account.ActionId;
                    xConfig.SetCookieSettings(account.LanguageCode);

                    if (Url.IsLocalUrl(returnUrl))
                    { return Redirect(returnUrl); }
                    else
                    { return Redirect("~/"); }
                }
            }
            return View();
        }

        //
        // GET: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            // Затрива сесията
            Session.RemoveAll();
            // Затрива Cookie на IIS
            FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    /*
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                     */
                }
                catch /* (MembershipCreateUserException e) */
                {
                   // ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
