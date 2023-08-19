using Framework.Configuration; 
using Framework.EF;
using marketplace;
using Newtonsoft.Json;
using PTEcommerce.Business;
using PTEcommerce.Web.Extensions;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Controllers
{
    public class AccountCustomerController : BaseController
    {
        private readonly IAccountCustomer accountCustomer;
        private readonly IHistoryTransfer historyTransfer;
        private readonly IPlayGames playGames;
        private readonly IBanks banks;
        private readonly string templateMailPath = Config.GetConfigByKey("templateMail");
        public AccountCustomerController()
        {
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
            playGames = SingletonIpl.GetInstance<IplPlayGames>();
            banks = SingletonIpl.GetInstance<IplBanks>();
        }
        // GET: AccountCustomer
        public ActionResult Index()
        {
            ViewBag.ListBank = banks.GetAll();
            ViewBag.Account = accountCustomer.GetById(memberSession.AccID);
            return View();
        }
        public ActionResult HistoryActivity()
        {
            return View();
        }
        public JsonResult UpdateAccount(AccountUpdate data)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập thông tin"
                }, JsonRequestBehavior.AllowGet);
            }
            if(data.BankId <= 0)
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập ngân hàng"
                }, JsonRequestBehavior.AllowGet);
            }
            if(data.PhoneNumber.Length != 10)
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng nhập số điện thoại hợp lệ"
                }, JsonRequestBehavior.AllowGet);
            }
            var dataAccount = accountCustomer.GetById(memberSession.AccID);
            if(dataAccount == null)
            {
                return Json(new
                {
                    status = false,
                    msg = "Tài khoản không tồn tại"
                }, JsonRequestBehavior.AllowGet);
            }
            dataAccount.BankId = data.BankId;
            dataAccount.BankAccount = data.BankAccount;
            dataAccount.BankNumber = data.BankNumber;
            dataAccount.FullName = data.FullName;
            dataAccount.PhoneNumber = data.PhoneNumber;
            dataAccount.Gender = data.Gender;
            var result = accountCustomer.Update(dataAccount);
            if (result)
            {
                return Json(new
                {
                    status = true,
                    msg = "Cập nhật thông tin thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                msg = "Cập nhật thông tin không thành công"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HistoryOrder()
        {
            ViewBag.hs = playGames.GetListPlayGameByMe(memberSession.AccID, 10, 10);
            return View();
        }
        public ActionResult GetHistoryOrder(int offset)
        {
            var data = playGames.GetListPlayGameByMe(memberSession.AccID, offset, 10);
            return View(data);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetInformation()
        {
            if (memberSession == null)
            {
                SessionCustomer.ClearSession();
                return Json(new
                {
                    code = 404,
                    AmountAvaiable = 0
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = accountCustomer.GetById(memberSession.AccID);
                if (memberSession.Token != data.Token)
                {
                    SessionCustomer.ClearSession();
                    return Json(new
                    {
                        code = 401,
                        AmountAvaiable = 0
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (memberSession.AccID != data.Id)
                    {
                        SessionCustomer.ClearSession();
                        return Json(new
                        {
                            code = 401,
                            AmountAvaiable = 0
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            code = 200,
                            AmountAvaiable = data.AmountAvaiable,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
        [HttpGet]
        public JsonResult GetHistoryTransfer()
        {
            int totalRow = 0;
            var pagging = GetPagingMessage(Request.QueryString);
            var data = historyTransfer.ListAllPagingByCustomer(memberSession.AccID, pagging.PageIndex, pagging.PageSize, ref totalRow);
            if (data != null && data.Count > 0)
            {
                return Json(new
                {
                    status = true,
                    rows = data.Select(x => new
                    {
                        Id = x.Id,
                        Type = x.Type,
                        AmountBefore = x.AmountBefore,
                        AmountModified = x.AmountModified,
                        AmountAfter = x.AmountAfter,
                        CreatedDate = x.CreatedDate,
                        Note = x.Note
                    }),
                    total = totalRow
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Throttle]
        public JsonResult ChangePassword(string passwordNew,string confirmPassword)
        {
            if (string.IsNullOrEmpty(passwordNew))
            {
                return Json(new
                {
                    status = false,
                    message = "Vui lòng nhập mật khẩu mới"
                }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(confirmPassword))
            {
                return Json(new
                {
                    status = false,
                    message = "Vui lòng nhập xác nhận mật khẩu"
                }, JsonRequestBehavior.AllowGet);
            }
            if (!passwordNew.ToLower().Equals(confirmPassword.ToLower().Trim()))
            {
                return Json(new
                {
                    status = false,
                    message = "Xác nhận mật khẩu không đúng"
                }, JsonRequestBehavior.AllowGet);
            }
            var dataAccount = accountCustomer.GetById(memberSession.AccID);
            if (dataAccount == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Cập nhật mật khẩu không thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            var passwordNewHash = Business.Helper.sha256_hash(passwordNew + ConstKey.keySHA);
            dataAccount.Password = passwordNewHash;
            accountCustomer.Update(dataAccount);
            SessionCustomer.ClearSession();
            return Json(new
            {
                status = true,
                message = "Đổi mật khẩu thành công, chuyển hướng đến đăng nhập"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            SessionAdmin.ClearSession();
            return Redirect("/");
        }
    }
}