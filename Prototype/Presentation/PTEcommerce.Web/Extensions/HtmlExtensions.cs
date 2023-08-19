using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Translate(this HtmlHelper htmlHelper, string key)
        {
            var viewPath = ((System.Web.Mvc.RazorView)htmlHelper.ViewContext.View).ViewPath;
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            var httpContext = htmlHelper.ViewContext.HttpContext;
            var val = (string)httpContext.GetGlobalResourceObject("Language", key, culture);

            return MvcHtmlString.Create(val);
        }
        public static MvcHtmlString DateFormat(this HtmlHelper helper, DateTime? date = null, string format = "dd-MM-yyyy", bool showTime = false)
        {
            if (date == null)
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(date.Value.ToString(format + (showTime ? "dd-MM-yyyy" : string.Empty)));
        }
    }
}