
using Framework.EF;
using marketplace;
using PTEcommerce.Business;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountAdmin accountAdmin;
        public LoginController()
        {
            accountAdmin = SingletonIpl.GetInstance<IplAccountAdmin>();
        }
        // GET: AdministratorManager/Login
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập đầy đủ thông tin"
                }, JsonRequestBehavior.AllowGet);
            }
            var accountData = accountAdmin.ViewDetailByUsername(userName);
            if (accountData == null)
            {
                return Json(new
                {
                    status = false,
                    msg = "Tài khoản không tồn tại"
                }, JsonRequestBehavior.AllowGet);
            }
            var passwordHash = Helper.sha256_hash(password.Trim() + ConstKey.keySHA);
            if (!accountData.Password.Equals(passwordHash))
            {
                return Json(new
                {
                    status = false,
                    msg = "Mật khẩu không đúng"
                }, JsonRequestBehavior.AllowGet);
            }
            var sess = new AdminSession
            {
                FullName = accountData.FullName,
                Id = accountData.Id,
                RoleId = accountData.RoleId
            };
            SessionAdmin.sessionName = "manager";
            SessionAdmin.SetUser(sess);
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            SessionAdmin.ClearSession();
            return Redirect("/AdministratorManager/Login");
        }
    }
}