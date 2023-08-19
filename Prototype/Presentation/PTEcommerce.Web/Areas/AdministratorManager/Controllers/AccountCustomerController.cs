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
    public class AccountCustomerController : BaseController
    {
        private readonly IAccountCustomer accountCustomer;
        private readonly IAccountAdmin accountAdmin;
        private readonly IHistoryTransfer historyTransfer;
        public AccountCustomerController()
        {
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
            accountAdmin = SingletonIpl.GetInstance<IplAccountAdmin>();
        }
        public ActionResult Index()
        {
            ViewBag.ListAccount = accountCustomer.GetAll();
            return View();
        }
        public JsonResult ListAllPaging(string searchString)
        {
            int totalRow = 0;
            var p = GetPagingMessage(Request.QueryString);
            var data = accountCustomer.ListAllPaging(searchString, 0, p.PageIndex, p.PageSize);
            return Json(new
            {
                status = true,
                rows = data,
                total = totalRow
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetAmount(int idAccount, decimal amount, int type, string note)
        {
            if (idAccount <= 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Vui lòng nhập dữ liệu hợp lệ"
                }, JsonRequestBehavior.AllowGet);
            }
            var dataAccount = accountCustomer.GetById(idAccount);
            if (dataAccount == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Tài khoản không hợp lệ"
                }, JsonRequestBehavior.AllowGet);
            }
            decimal priceBefore = dataAccount.AmountAvaiable;
            var modelInsertHistory = new HistoryTransfer
            {
                IdAccount = dataAccount.Id,
                CreatedDate = DateTime.Now,
                Type = (int)EnumSystem.EnumTypeHistoryTransfer.addamount,
                AmountBefore = priceBefore,
                AmountModified = amount
            };
            if(type == 1)
            {
                dataAccount.AmountAvaiable = priceBefore + amount;
                modelInsertHistory.AmountAfter = priceBefore + amount;
            }
            else
            {
                dataAccount.AmountAvaiable = priceBefore - amount;
                modelInsertHistory.AmountAfter = priceBefore - amount;
            }
            modelInsertHistory.Note = "Admin " + (type == 1 ? "cộng" : "trừ") + Business.Helper.MoneyFormat(amount) + " với nội dung: " + note;
            historyTransfer.Insert(modelInsertHistory);
            accountCustomer.Update(dataAccount);
            return Json(new
            {
                status = true,
                message = (type == 1 ? "Cộng" : "Trừ") + " tiền thành công"
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePassword(int idAccount, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Json(new
                {
                    status = false,
                    message = "Vui lòng nhập mật khẩu mới"
                }, JsonRequestBehavior.AllowGet);
            }
            var dataAccount = accountCustomer.GetById(idAccount);
            if(dataAccount == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Tài khoản không tồn tại"
                }, JsonRequestBehavior.AllowGet);
            }
            dataAccount.Password = Helper.sha256_hash(password.Trim() + ConstKey.keySHA);
            var result = accountCustomer.Update(dataAccount);
            if (result)
            {
                return Json(new
                {
                    status = true,
                    message = "Đổi mật khẩu thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                message = "Đổi mật khẩu không thành công"
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewDetail(int id)
        {
            var data = accountCustomer.GetById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePasswordAdmin(string password, string repassword)
        {
            if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repassword))
            {
                return Json(new
                {
                    status = false,
                    message = "Vui lòng nhập đầy đủ thông tin"
                }, JsonRequestBehavior.AllowGet);
            }
            if (!password.Equals(repassword))
            {
                return Json(new
                {
                    status = false,
                    message = "Xác nhận mật khẩu không đúng"
                }, JsonRequestBehavior.AllowGet);
            }
            var passwordHash = Helper.sha256_hash(password.Trim() + ConstKey.keySHA);
            var mems = SessionAdmin.GetUser();
            var dataAccount = accountAdmin.GetById(mems.Id);
            if(dataAccount == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Tài khoản không tồn tại"
                }, JsonRequestBehavior.AllowGet);
            }
            dataAccount.Password = passwordHash;
            var result = accountAdmin.Update(dataAccount);
            if (result)
            {
                SessionAdmin.ClearSession();
                return Json(new
                {
                    status = true,
                    message = "Đổi mật khẩu thành công, vui lòng đăng nhập lại"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                message = "Đổi mật khẩu không thành công"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}