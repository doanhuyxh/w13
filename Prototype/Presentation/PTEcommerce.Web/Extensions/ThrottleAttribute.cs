using PTEcommerce.Web.Models;
using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace PTEcommerce.Web.Extensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var memberSession = SessionCustomer.GetUser();
            int second = 5;
            if (memberSession != null)
            {

                var key = "Access_" + memberSession.AccID.ToString();
                var allowExecute = false;

                if (HttpRuntime.Cache[key] == null)
                {
                    HttpRuntime.Cache.Add(key,
                        true, // is this the smallest data we can have?
                        null, // no dependencies
                        DateTime.Now.AddSeconds(second), // absolute expiration
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.Low,
                        null); // no callback

                    allowExecute = true;
                }

                if (!allowExecute)
                {
                    string message = string.Format("Không spam yêu cầu, vui lòng thử lại sau {0} giây", second);

                    c.Result = new JsonResult
                    {
                        Data = new
                        {
                            status = false,
                            message = message
                        },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        ContentType = "application/json",
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                c.Result = new JsonResult
                {
                    Data = new
                    {
                        status = false,
                        message = "Hết thời gian đăng nhập, vui lòng thử lại"
                    },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}