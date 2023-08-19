using Framework.Configuration;
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

namespace PTEcommerce.Web.Controllers
{
    public class BaseController : Controller
    {
        public MemberSession memberSession;
        public BaseController()
        {
            memberSession = SessionCustomer.GetUser();
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
            ISettings settings = SingletonIpl.GetInstance<IplSettings>();
            IAccountCustomer account = SingletonIpl.GetInstance<IplAccountCustomer>();
            var settingMaintain = settings.GetValueByKey("maintain");
            if (settingMaintain != null && settingMaintain.Value.ToLower() == "on")
            {
                filterContext.Result = new RedirectResult("/bao-tri");
            }
            else if (memberSession == null)
            {
                filterContext.Result = new RedirectResult("/dang-nhap");
            }
            else if(memberSession != null)
            {
                var dataAccount = account.GetById(memberSession.AccID);
                if (memberSession.Token != dataAccount.Token)
                {
                    filterContext.Result = new RedirectResult("/dang-xuat");
                }
                else if (memberSession.AccID != dataAccount.Id)
                {
                    filterContext.Result = new RedirectResult("/dang-xuat");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}