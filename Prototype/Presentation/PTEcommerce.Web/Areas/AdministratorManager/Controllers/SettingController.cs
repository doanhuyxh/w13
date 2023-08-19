using Framework.EF;
using marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager.Controllers
{
    public class SettingController : BaseController
    {
        private readonly ISettings settings;
        public SettingController()
        {
            settings = SingletonIpl.GetInstance<IplSettings>();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListSetting()
        {
            int totalRow = 0;
            var pagging = GetPagingMessage(Request.QueryString);
            var data = settings.ListAllPaging(pagging.PageIndex, pagging.PageSize, ref totalRow);
            return Json(new
            {
                status = true,
                rows = data,
                total = totalRow
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateSetting(int id, string value)
        {
            var flag = settings.UpdateSetting(id, value);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
    }
}