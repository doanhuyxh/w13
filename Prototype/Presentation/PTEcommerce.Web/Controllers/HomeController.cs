using Framework.EF;
using marketplace;
using Newtonsoft.Json;
using PTEcommerce.Web.Extensions;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPlayGames playGames;
        public HomeController()
        {
            playGames = SingletonIpl.GetInstance<IplPlayGames>();
        }
        // GET: Home
        public ActionResult Index()
        {
            var data = playGames.GetTop10();
            return View(data);
        }
        public ActionResult OrderCurrent()
        {
            var data = playGames.GetTop10();
            return View(data);
        }
        public ActionResult HistoryWin(int idAccount)
        {
            if(idAccount > 0)
            {
                idAccount = memberSession.AccID;
            }
            var data = playGames.GetListHistoryPlayGame(idAccount, 0, 10);
            return View(data);
        }
        public ActionResult Maintain()
        {
            return View();
        }
    }
}