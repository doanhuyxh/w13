using Framework.Configuration;
using Framework.EF;
using marketplace;
using Newtonsoft.Json;
using PTEcommerce.Business;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Controllers
{
    public class LoginController : Controller
    {
        private IAccountCustomer account;
        private ISettings settings;
        private readonly string templateMailPath = Config.GetConfigByKey("templateMail");
        public LoginController()
        {
            account = SingletonIpl.GetInstance<IplAccountCustomer>();
            settings = SingletonIpl.GetInstance<IplSettings>();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Recovery()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(AccountCustomerRegister data)
        {
            if (string.IsNullOrEmpty(data.Username))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập tài khoản"
                }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(data.Password))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập mật khẩu"
                }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(data.RePassword))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng xác nhận mật khẩu"
                }, JsonRequestBehavior.AllowGet);
            }
            if (data.Username.Trim().Length < 5 || !new Regex("^[a-zA-Z0-9'!#$%&'*+/=?^_`{|}~.-]*$").IsMatch(data.Username.Trim()))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng tài khoản gồm chữ cái hoặc số 6 ký tự trở lên"
                }, JsonRequestBehavior.AllowGet);
            }
            if (account.ViewDetailByUsername(data.Username.Trim()) != null)
            {
                return Json(new
                {
                    status = false,
                    msg = "Tài khoản đăng nhập đã tồn tại trên hệ thống"
                }, JsonRequestBehavior.AllowGet);
            }
            if (!data.Password.Trim().Equals(data.RePassword.Trim()))
            {
                return Json(new
                {
                    status = false,
                    msg = "Xác nhận mật khẩu không đúng"
                }, JsonRequestBehavior.AllowGet);
            }
            var flagInsert = account.Insert(new AccountCustomer
            {
                Username = data.Username.Trim(),
                Email = string.Empty,
                Password = Helper.sha256_hash(data.Password.Trim() + ConstKey.keySHA),
                AmountAvaiable = 0,
                IsActive = true,
                CreatedDate = DateTime.Now,
                Token = string.Empty
            });
            if (flagInsert != null)
                return Json(new
                {
                    status = true,
                    msg = "Đăng ký thành công, vui lòng đăng nhập để sử dụng dịch vụ"
                }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                status = false,
                msg = "Đăng ký thất bại, vui lòng liên hệ quản trị viên"
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập tài khoản hoặc mật khẩu"
                }, JsonRequestBehavior.AllowGet);
            }
            var accountData = account.ViewDetailByUserNamePassword(username, Helper.sha256_hash(password + ConstKey.keySHA));
            if (accountData == null)
            {
                return Json(new
                {
                    status = false,
                    msg = "Tài khoản hoặc mật khẩu không đúng"
                }, JsonRequestBehavior.AllowGet);
            }
            if(accountData.IsActive == false)
            {
                return Json(new
                {
                    status = false,
                    msg = "Tài khoản bị khóa"
                }, JsonRequestBehavior.AllowGet);
            }
            string token = Helper.GetToken(accountData.Username.Trim());
            account.UpdateToken(accountData.Id, token);
            var sess = new MemberSession
            {
                AccID = accountData.Id,
                Username = accountData.Username,
                Email = string.Empty,
                Token = token
            };
            SessionCustomer.sessionName = "customer";
            SessionCustomer.SetUser(sess);
            return Json(new
            {
                status = true,
                msg = "Đăng nhập thành công"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            SessionCustomer.ClearSession();
            return Redirect("/");
        }
    }
}