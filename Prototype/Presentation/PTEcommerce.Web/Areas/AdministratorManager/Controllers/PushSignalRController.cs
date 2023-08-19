using Microsoft.AspNet.SignalR;
using PTEcommerce.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class PushSignalRController : Controller
    {
        private readonly IHubContext chatHub;
        public PushSignalRController()
        {
            chatHub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        }
        public JsonResult CreateSession(int sessionId, int result1, int result2)
        {
            chatHub.Clients.All.broadcastMessage(sessionId, result1, result2);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePlayGame()
        {
            chatHub.Clients.All.updatePlaygame();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}