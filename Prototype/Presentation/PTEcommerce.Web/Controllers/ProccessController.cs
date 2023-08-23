using Framework.Configuration;
using Framework.EF;
using marketplace;
using PTEcommerce.Business;
using PTEcommerce.Web.Extensions;
using PTEcommerce.Web.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using RestSharp;
using Framework.Helper.Logging;

namespace PTEcommerce.Web.Controllers
{
    public class ProccessController : BaseController
    {
        private readonly IPlayGames playGames;
        private readonly ISettings settings;
        private readonly ISessionGames sessionGames;
        private readonly IAccountCustomer accountCustomer;
        private readonly IHistoryTransfer historyTransfer;
        private readonly string url = Config.GetConfigByKey("url");
        public ProccessController()
        {
            playGames = SingletonIpl.GetInstance<IplPlayGames>();
            sessionGames = SingletonIpl.GetInstance<IplSessionGames>();
            settings = SingletonIpl.GetInstance<IplSettings>();
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
        }
        // GET: Proccess
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Withdrawal()
        {
            return View();
        }
        public ActionResult HistoryWithdrawal()
        {
            return View();
        }
        public JsonResult GetSession()
        {
            var sesionGamesData = sessionGames.GetLastSession();
            if (sesionGamesData != null)
            {
                return Json(new
                {
                    status = true,
                    countdown = (sesionGamesData.CreatedDate.AddSeconds(90) - DateTime.Now).TotalSeconds,
                    session = sesionGamesData.Id,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = true,
                countdown = 0,
                session = 0
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYetSession()
        {
            var sesionGamesData = sessionGames.GetYetSession();
            if (sesionGamesData != null)
            {
                return Json(new
                {
                    status = true,
                    sesion = sesionGamesData.Id,
                    value1 = Business.Helper.ConvertValue(sesionGamesData.Value.ToString()).valuestring,
                    value2 = Business.Helper.ConvertValue(sesionGamesData.Value2.ToString()).valuestring,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = true,
                sesion = 0,
                value1 = "...",
                value2 = "...",
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetHistory(int page)
        {
            int offset = (page - 1) * 10;
            int limit = 10;
            var data = playGames.GetListPlayGameByMe(memberSession.AccID, offset, limit);
            return View(data);
        }
        [Throttle]
        public JsonResult PlayGames(PlayGamePost data)
        {
            var priceMin = string.IsNullOrEmpty(settings.GetValueByKey("pricemin").Value) ? 0 : decimal.Parse(settings.GetValueByKey("pricemin").Value);
            if (string.IsNullOrEmpty(data.value))
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng chọn dự đoán"
                }, JsonRequestBehavior.AllowGet);
            }
            var checkData = Helper.ConvertValue(data.value);
            if(checkData == null || checkData.value.Count == 0)
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng chọn dự đoán"
                }, JsonRequestBehavior.AllowGet);
            }
            if(data.sessionId <= 0)
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng chọn phiên dự đoán"
                }, JsonRequestBehavior.AllowGet);
            }
            if(priceMin > 0 && (data.price * checkData.value.Count) < priceMin)
            {
                return Json(new
                {
                    status = false,
                    msg = "Vui lòng dự đoán số tiền lớn hơn hoặc bằng " + PTEcommerce.Business.Helper.MoneyFormat(priceMin)
                }, JsonRequestBehavior.AllowGet);
            }
            var sessionData = sessionGames.GetById(data.sessionId);
            if(sessionData == null)
            {
                return Json(new
                {
                    status = false,
                    msg = "Phiên dự đoán không tồn tại"
                }, JsonRequestBehavior.AllowGet);
            }
            if (sessionData.CreatedDate.AddSeconds(90) <= DateTime.Now)
            {
                return Json(new
                {
                    status = false,
                    msg = "Phiên dự đoán đã kết thúc"
                }, JsonRequestBehavior.AllowGet);
            }
            if (sessionData.CreatedDate.AddSeconds(85) <= DateTime.Now)
            {
                return Json(new
                {
                    status = false,
                    msg = "Phiên ngừng nhận dự đoán"
                }, JsonRequestBehavior.AllowGet);
            }
            var dataAccount = accountCustomer.GetById(memberSession.AccID);
            if(dataAccount == null)
            {
                return Json(new
                {
                    status = false,
                    msg = "Tài khoản đã bị khoá"
                }, JsonRequestBehavior.AllowGet);
            }
            var amountBefore = dataAccount.AmountAvaiable;
            if(amountBefore < (data.price * checkData.value.Count))
            {
                return Json(new
                {
                    status = false,
                    msg = "Số tiền không đủ để dự đoán"
                }, JsonRequestBehavior.AllowGet);
            }
            dataAccount.AmountAvaiable = amountBefore - (data.price * checkData.value.Count);
            var resultAmount = accountCustomer.Update(dataAccount);
            if (resultAmount)
            {
                var client = new RestClient(url + "/AdministratorManager/PushSignalR/UpdatePlayGame");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if(response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Logging.PushString("Call signalR success");
                }
                else
                {
                    Logging.PushString("Call signalR error");
                }
                playGames.Insert(new PlayGames
                {
                    IdAccount = dataAccount.Id,
                    CreatedDate = DateTime.Now,
                    Amount = data.price,
                    AmountReceive = 0,
                    CompletedDate = DateTime.Now,
                    Result = string.Empty,
                    ResultString = string.Empty,
                    SessionId = data.sessionId,
                    Status = 1,
                    Value = string.Join(",", checkData.value),
                    ValueString = checkData.valuestring
                });
                historyTransfer.Insert(new HistoryTransfer
                {
                    IdAccount = dataAccount.Id,
                    AmountBefore = amountBefore,
                    AmountModified = (data.price * checkData.value.Count),
                    AmountAfter = amountBefore - (data.price * checkData.value.Count),
                    CreatedDate = DateTime.Now,
                    Note = "Dự đoán phiên " + data.sessionId + " kết quả " + Helper.ConvertValue(data.value).valuestring,
                    Type = 2
                });
                return Json(new
                {
                    status = true,
                    msg = "Dự đoán thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                msg = "Dự đoán không thành công"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}