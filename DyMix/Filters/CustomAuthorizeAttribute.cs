using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DyMix.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /*
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
        */

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isLogin = false;
            // Ако няма оторизация
            if (!httpContext.User.Identity.IsAuthenticated)
            { isLogin = false; }
            // Ако няма сесия
            else if (!xSession.IsLogin)
            { isLogin = false; }
            // Логнат е
            else
            { isLogin = true; }

            // Ако не е логнат
            if (!isLogin)
            {
                // Затрива сесията
                xSession.RemoveAll();
                // Затрива Cookie на IIS
                System.Web.Security.FormsAuthentication.SignOut();
            }
            return isLogin;
            /*
            // Ако няма сесия, прави опит за автоматична оторизация
            if (xSession.UAccount == null)
            {
                string idAccount = "";
                // Търси ID в клиентската заявка
                object id = httpContext.Request.RequestContext.RouteData.Values["id"];
                if (id != null)
                {
                    idAccount = id.ToString().Trim();
                    // object deviceId = httpContext.Request.RequestContext.RouteData.Values["deviceId"];
                }
                // Търси ID в Cookie на IIS
                else if ((httpContext.User != null)
                  && (httpContext.User.Identity != null)
                  && (httpContext.User.Identity.Name != null)
                  && (httpContext.User.Identity.Name != "")
                  && (httpContext.User.Identity.IsAuthenticated))
                {
                    idAccount = httpContext.User.Identity.Name.Trim();
                }
                else
                {
                    // Проверява Cookie на портала
                    idAccount = BaseFramework.Helpers.AuthCookie.Get();
                    if (idAccount == "")
                    { return false; }
                }                

                Infea.Models.UserAccount uAccount = null;
                using (Infea.Models.AccountModel account = new Infea.Models.AccountModel())
                {
                    uAccount = account.GetUserAccount(idAccount);
                }
                if (uAccount == null)
                { return false; }
                else
                {
                    uAccount.IsLogin = uAccount.IsRegistered;
                    // Записва сесията
                    xSession.UAccount = uAccount;
                    // Записва Cookie на IIS
                    System.Web.Security.FormsAuthentication.SetAuthCookie(uAccount.IdAccount, true);
                    // Записва Cookie на порала
                    BaseFramework.Helpers.AuthCookie.Set(uAccount.IdAccount, 24 * 60);
                    // Резултат от логването
                    isLogin = true;
                }
            }
            else
            {
                isLogin = true; 
            }
            xSession.UAccount.IsLogin = xSession.UAccount.IsRegistered;

            if (isLogin && this.Roles != "")
            {
                string accountRole = xSession.UAccount.Role;
                if (String.IsNullOrWhiteSpace(accountRole))
                { return false; }
                else
                { return (this.Roles.IndexOf(xSession.UAccount.Role) > -1); }
            }
            else
            { return isLogin; }
             * */
        }
    }
}