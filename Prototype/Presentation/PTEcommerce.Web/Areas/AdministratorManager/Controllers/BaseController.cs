using Framework.EF;
using marketplace;
using PTEcommerce.Business;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAccountAdmin accountAdmin;
        public BaseController()
        {
            accountAdmin = SingletonIpl.GetInstance<IplAccountAdmin>();
        }
        public static HttpContextBase HttpContextBase { get; set; }
        protected PagingRequest GetPagingMessage(NameValueCollection queries, bool returnOffset = true)
        {
            var limit = Common.StringToInt(queries["limit"]);
            var offset = Common.StringToInt(queries["offset"]);
            return new PagingRequest
            {
                PageIndex = offset,
                PageSize = limit
            };
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SessionAdmin.sessionName = "manager";
            var sess = SessionAdmin.GetUser();
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Login", action = "Index"}));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}