using Framework.EF;
using marketplace;
using PTEcommerce.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class WithdrawalController : BaseController
    {

        private readonly IAccountCustomer accountCustomer;
        private readonly IWithdrawal withdrawal;
        private readonly IHistoryTransfer historyTransfer;
        public WithdrawalController()
        {
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            withdrawal = SingletonIpl.GetInstance<IplWithdrawal>();
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
        }
        public ActionResult Index()
        {
            ViewBag.ListAccount = accountCustomer.GetAll();
            return View();
        }
        public JsonResult ListAllPaging(int idAccount, int status)
        {
            int totalRow = 0;
            decimal totalAmount = 0;
            var p = GetPagingMessage(Request.QueryString);
            var data = withdrawal.ListAllPaging(idAccount, status, p.PageIndex, p.PageSize);
            if (data != null && data.Count > 0)
            {
                totalAmount = data[0].TotalAmount;
                totalRow = data[0].TotalRow;
            }
            return Json(new
            {
                status = true,
                rows = data,
                total = totalRow,
                totalAmount = totalAmount
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitOrder(int id, int status, string note)
        {
            if (id <= 0 || status == 1 || string.IsNullOrEmpty(note))
            {
                return Json(new
                {
                    status = false,
                    message = "Vui lòng nhập đầy đủ thông tin"
                }, JsonRequestBehavior.AllowGet);
            }
            var dataOrder = withdrawal.GetById(id);
            if (dataOrder == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Không tồn tại đơn rút tiền"
                }, JsonRequestBehavior.AllowGet);
            }
            if (dataOrder.Status == 2 || dataOrder.Status == 4)
            {
                return Json(new
                {
                    status = false,
                    message = "Đơn rút tiền đã được xử lý rồi"
                }, JsonRequestBehavior.AllowGet);
            }
            dataOrder.Status = status;
            dataOrder.Note = note;
            var flag = withdrawal.Update(dataOrder);
            if (flag)
            {
                if (status == 4)
                {
                    var dataAccount = accountCustomer.GetById(dataOrder.IdAccount);
                    if (dataAccount != null)
                    {
                        var prices = dataAccount.AmountAvaiable;
                        historyTransfer.Insert(new HistoryTransfer
                        {
                            IdAccount = dataAccount.Id,
                            Type = (int)EnumSystem.EnumTypeHistoryTransfer.withdrawal,
                            AmountBefore = prices,
                            AmountModified = dataOrder.Amount,
                            AmountAfter = prices + dataOrder.Amount,
                            CreatedDate = DateTime.Now,
                            Note = "Rút tiền bị hủy, hoàn lại " + Helper.MoneyFormat(dataOrder.Amount) + " về tài khoản"
                        });
                        dataAccount.AmountAvaiable = prices + dataOrder.Amount;
                        accountCustomer.Update(dataAccount);
                    }
                }
                return Json(new
                {
                    status = true,
                    message = "Duyệt đơn rút tiền thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                message = "Xử lý lỗi vui lòng thử lại"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}