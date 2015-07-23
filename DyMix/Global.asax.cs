using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DyMix
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>  
        /// Application AcquireRequestState  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string cultureCode = "bg-BG";
            string lang = Request.QueryString["lang"];
            if (String.IsNullOrWhiteSpace(lang))
            {
                HttpCookie cookie = Request.Cookies["UserLanguage"];
                if (cookie != null
                  && cookie.Value != null
                  && cookie.Value.Trim() != string.Empty)
                { cultureCode = cookie.Value; }
            }
            else
            { cultureCode = lang; }
            //Default Language/Culture for all number, Date format  
            System.Threading.Thread.CurrentThread.CurrentCulture =
            System.Globalization.CultureInfo.CreateSpecificCulture("en");

            //Ui Culture for Localized text in the UI  
            System.Threading.Thread.CurrentThread.CurrentUICulture =
            new System.Globalization.CultureInfo(cultureCode);
        }
    }
}