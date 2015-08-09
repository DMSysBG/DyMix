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
                HttpCookie cookie = xConfig.GetCookieSettings();
                if (cookie != null)
                {
                    string cookieCultureCode = cookie["CultureCode"];
                    if( !String.IsNullOrWhiteSpace(cookieCultureCode))
                    { cultureCode = cookieCultureCode; }
                }
            }
            else
            {
                xConfig.SetCookieSettings(lang);
                cultureCode = lang;
            }
            xConfig.SetThreadCulture(cultureCode);
        }
    }
}