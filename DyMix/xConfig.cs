using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DyMix
{
    public static class xConfig
    {
        public static string ConnectionString
        {
            get
            { return System.Configuration.ConfigurationManager.ConnectionStrings["dymixConnection"].ConnectionString; }
        }
        
        public static void SetCookieSettings(string cultureCode)
        {
            HttpCookie myCookie = new HttpCookie("USettings");
            myCookie["CultureCode"] = cultureCode;
            myCookie.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        public static HttpCookie GetCookieSettings()
        {
            return HttpContext.Current.Request.Cookies["USettings"];
        }

        public static void SetThreadCulture(string cultureCode)
        {
            if (cultureCode != null)
            {
                //Default Language/Culture for all number, Date format  
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.CreateSpecificCulture(cultureCode);

                //Ui Culture for Localized text in the UI  
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo(cultureCode);
            }
        }
    }
}