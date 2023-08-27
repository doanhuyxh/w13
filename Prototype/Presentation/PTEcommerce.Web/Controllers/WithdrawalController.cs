using DocumentFormat.OpenXml.Math;
using Framework.EF;
using Framework.Helper.Logging;
using marketplace;
using PTEcommerce.Business;
using PTEcommerce.Business.Business;
using PTEcommerce.Business.IBusiness;
using PTEcommerce.Web.Extensions;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Controllers
{
    public class WithdrawalController : BaseController
    {
        private readonly IAccountCustomer accountCustomer;
        private readonly IWithdrawal withdrawal;
        private readonly IBanks banks;
        private readonly IHistoryTransfer historyTransfer;
        public WithdrawalController()
        {
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            withdrawal = SingletonIpl.GetInstance<IplWithdrawal>();
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
            banks = SingletonIpl.GetInstance<IplBanks>();
        }
        // GET: Withdrawal
        public ActionResult Index()
        {
            ViewBag.Account = accountCustomer.GetById(memberSession.AccID);
            ViewBag.ListBank = banks.GetAll();
            return View();
        }
        public ActionResult History()
        {
            var data = withdrawal.ListAllPagingByCustomer(memberSession.AccID, 0, 10);
            return View(data);
        }
        public ActionResult HistoryTransaction(int offset, int limit = 10)
        {
            var data = withdrawal.ListAllPagingByCustomer(memberSession.AccID, offset, limit);
            return View(data);
        }
        [Throttle]
        public JsonResult CreateRequestWithdrawal(WithdrawalModel model)
        {
            try
            {
                if (memberSession == null)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Phiên đăng nhập hết hạn, vui lòng đăng nhập lại",
                        code = 401
                    }, JsonRequestBehavior.AllowGet);
                }
                var dataAccount = accountCustomer.GetById(memberSession.AccID);
                if (dataAccount == null)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Tài khoản không tồn tại",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if(string.IsNullOrEmpty(dataAccount.BankAccount) || string.IsNullOrEmpty(dataAccount.BankNumber) || dataAccount.BankId <= 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vui lòng cập nhật thông tin cá nhân trước khi rút tiền",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!dataAccount.IsActive)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Tài khoản bị khóa vui lòng liên hệ quản trị viên",
                        code = 400 
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Amount < 50000)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Số tiền rút không hợp lệ, tối thiểu 50.000đ",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.BankId <= 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vui lòng chọn ngân hàng/ví",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.BankAccount))
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vui lòng nhập chủ tài khoản",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.BankNumber))
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vui lòng nhập số tài khoản",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                var dataBank = banks.GetById(model.BankId);
                if (dataBank == null)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Ngân hàng không khả dụng",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.Amount <= 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vui lòng nhập số tiền rút",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                if (dataAccount.AmountAvaiable < model.Amount)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Số dư không đủ để rút",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                var checkProcess = withdrawal.CheckQuantityWithdrawal(memberSession.AccID);
                if(checkProcess != null)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vẫn còn đơn hàng đang xử lý không thể rút tiền",
                        code = 400
                    }, JsonRequestBehavior.AllowGet);
                }
                var flagInsert = withdrawal.Insert(new Withdrawal
                {
                    IdAccount = dataAccount.Id,
                    BankId = dataBank.Id,
                    BankAccount = model.BankAccount,
                    BankNumber = model.BankNumber,
                    CreatedDate = DateTime.Now,
                    Amount = model.Amount,
                    Note = "Đang xử lý",
                    Status = 1
                });
                if (flagInsert != null)
                {
                    var prices = dataAccount.AmountAvaiable;
                    dataAccount.AmountAvaiable = prices - model.Amount;
                    accountCustomer.Update(dataAccount);
                    historyTransfer.Insert(new HistoryTransfer
                    {
                        IdAccount = dataAccount.Id,
                        AmountBefore = prices,
                        AmountModified = model.Amount,
                        AmountAfter = prices - model.Amount,
                        CreatedDate = DateTime.Now,
                        Note = "Rút tiền về " + dataBank.BankName,
                        Type = 3
                    });
                    return Json(new
                    {
                        status = true,
                        message = "Rút tiền thành công, vui lòng đợi duyệt",
                        code = 200
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    status = false,
                    message = "Có lỗi rút tiền, vui lòng liên hệ quản trị viên",
                    code = 400
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return Json(new
                {
                    status = false,
                    message = "Có lỗi rút tiền, vui lòng liên hệ quản trị viên",
                    code = 400
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}