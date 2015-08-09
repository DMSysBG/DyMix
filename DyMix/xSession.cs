using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSys.Systems;

namespace DyMix
{
    public static class xSession
    {
        private static Int32 GetSessionInt32(string name)
        { 
            object value = HttpContext.Current.Session[name];
            return TryParse.ToInt32(value, 0);
        }
        private static void SetSession(string name, Int32 value)
        {
            HttpContext.Current.Session[name] = value;
        }

        public static int AccountId
        {
            get
            { return GetSessionInt32("ACCOUNT_ID"); }
            set
            { SetSession("ACCOUNT_ID", value); }
        }

        public static bool IsLogin
        {
            get
            { return (xSession.AccountId > 0); }
        }

        public static string SessionId
        {
            get
            { return HttpContext.Current.Session.SessionID; }
        }

        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }
        
    }
}