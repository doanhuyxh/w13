using Framework.EF;
using marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class HistoryTransferController : BaseController
    {
        private readonly IHistoryTransfer historyTransfer;
        private readonly IAccountCustomer accountCustomer;
        private readonly IPlayGames playGames;
        public HistoryTransferController()
        {
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            playGames = SingletonIpl.GetInstance<IplPlayGames>();
        }
        public ActionResult Index()
        {
            ViewBag.ListAccount = accountCustomer.GetAll();
            return View();
        }
        public ActionResult HistoryPlay() {
            ViewBag.ListAccount = accountCustomer.GetAll();
            return View();
        }
        public JsonResult ListAllPaging(int idAccount, int type)
        {
            int totalRow = 0;
            var p = GetPagingMessage(Request.QueryString);
            var data = historyTransfer.ListAllPaging(idAccount, type, p.PageIndex, p.PageSize);
            if (data != null && data.Count > 0)
            {
                totalRow = data[0].TotalRow;
            }
            return Json(new
            {
                status = true,
                rows = data,
                total = totalRow
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListAllPlayHistoryPaging(int idAccount)
        {
            int totalRow = 0;
            var p = GetPagingMessage(Request.QueryString);
            var data = playGames.GetListHistoryPlayGame(idAccount, p.PageIndex, p.PageSize);
            if (data != null && data.Count > 0)
            {
                totalRow = data[0].TotalRow;
            }
            return Json(new
            {
                status = true,
                rows = data != null && data.Count > 0 ? data.Select(x => new
                {
                    Id = x.Id,
                    Username = x.Username,
                    Value = Business.Helper.ConvertValue(x.Value).valuestring,
                    Result = x.ResultString,
                    CreatedDate = x.CreatedDate,
                    CompletedDate = x.CompletedDate,
                    Amount = x.Amount * Business.Helper.ConvertValue(x.Value).value.Count,
                    AmountReceive = x.AmountReceive,
                    SessionId = x.SessionId
                }).ToList() : new object(),
                total = totalRow
            }, JsonRequestBehavior.AllowGet);
        }
    }
}