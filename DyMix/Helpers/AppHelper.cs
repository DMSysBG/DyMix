using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class AppHelper
    {
        public static string ToDisplayDate(this HtmlHelper htmlHelper, DateTime? value)
        {
            return (value == null) ? "" : ((DateTime)value).ToString("dd.MM.yyyy");
        }    

        public static string ToDisplayDecimal(this HtmlHelper htmlHelper, decimal? value)
        {
            return (value == null) ? "" : ((decimal)value).ToString("#0.00");
        }
    }
}