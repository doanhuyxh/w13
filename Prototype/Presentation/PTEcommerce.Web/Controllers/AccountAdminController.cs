
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Controllers
{
    public class AccountAdminController : BaseController
    {
        // GET: AccountAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            SessionAdmin.ClearSession();
            return Redirect("/");
        }
    }
}