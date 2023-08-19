using Framework.EF;
using Framework.Helper.Session;
using marketplace;
using Microsoft.AspNet.SignalR;
using PTEcommerce.Business;
using PTEcommerce.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IPlayGames playGames;
        private readonly ISessionGames sessionGames;
        public DashboardController()
        {
            playGames = SingletonIpl.GetInstance<IplPlayGames>();
            sessionGames = SingletonIpl.GetInstance<IplSessionGames>();
        }
        public ActionResult Index()
        {
            ViewBag.SessionGame = sessionGames.GetLastSession();
            return View();
        }

        public JsonResult ChangeResultSession(int sessionId, int result1, int result2)
        {
            var sessionData = sessionGames.GetById(sessionId);
            if(sessionData == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Phiên không hợp lệ"
                }, JsonRequestBehavior.AllowGet);
            }
            if(sessionData.CreatedDate.AddSeconds(90) <= DateTime.Now)
            {
                return Json(new
                {
                    status = false,
                    message = "Phiên đã kết thúc"
                }, JsonRequestBehavior.AllowGet);
            }
            sessionData.Value = result1;
            sessionData.Value2 = result2;
            var flag = sessionGames.Update(sessionData);
            if (flag)
            {
                return Json(new
                {
                    status = true,
                    message = "Cập nhật kết quả thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                message = "Cập nhật kết quả lỗi"
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPlayGameBySessionId(int sessionId, int offset, int limit)
        {
            var data = playGames.GetListPlayGameAllBySessionId(sessionId);
            return Json(new
            {
                status = true,
                rows = data.Select(x=> new
                {
                    Id = x.Id,
                    Username = x.Username,
                    CreatedDate = x.CreatedDate,
                    Amount = x.Amount,
                    Value = Helper.ConvertValue(x.Value).valuestring,
                    TotalValue = Helper.ConvertValue(x.Value).value.Count * x.Amount,
                }),
                total = data.Count
            }, JsonRequestBehavior.AllowGet);
        }

    }
}