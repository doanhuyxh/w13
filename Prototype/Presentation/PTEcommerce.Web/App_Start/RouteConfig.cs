using System.Web.Mvc;
using System.Web.Routing;

namespace PTEcommerce.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Login", action = "Register" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Login", action = "Index" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "Login", action = "Logout" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Recovery",
                url: "quen-mat-khau",
                defaults: new { controller = "Login", action = "Recovery" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "RecoveryPassword",
                url: "lay-lai-mat-khau",
                defaults: new { controller = "Login", action = "RecoveryPassword" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Information",
                url: "thong-tin-tai-khoan",
                defaults: new { controller = "AccountCustomer", action = "Index" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "ChangePassword",
                url: "doi-mat-khau",
                defaults: new { controller = "AccountCustomer", action = "ChangePassword" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "HistoryActivity",
                url: "lich-su-giao-dich",
                defaults: new { controller = "AccountCustomer", action = "HistoryActivity" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "GetHistoryTransfer",
                url: "lay-lich-su-giao-dich",
                defaults: new { controller = "AccountCustomer", action = "HistoryTransaction" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "PlayGame",
                url: "nhiem-vu",
                defaults: new { controller = "Proccess", action = "Index" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Phien cu",
                url: "phien-cu",
                defaults: new { controller = "Proccess", action = "GetYetSession" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Danh Sach Phien Cu",
                url: "danh-sach-phien-cu",
                defaults: new { controller = "Proccess", action = "GetYet5Session" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Phien moi",
                url: "phien-moi",
                defaults: new { controller = "Proccess", action = "GetSession" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Du doan",
                url: "du-doan",
                defaults: new { controller = "Proccess", action = "PlayGames" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
               name: "Lich su du doan",
               url: "lich-su-du-doan",
               defaults: new { controller = "Proccess", action = "GetHistory" },
               namespaces: new string[] { "PTEcommerce.Web.Controllers" }
           );
            routes.MapRoute(
                name: "CreateRequestWithdrawal",
                url: "yeu-cau-rut-tien",
                defaults: new { controller = "Withdrawal", action = "CreateRequestWithdrawal" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Withdrawal",
                url: "rut-tien",
                defaults: new { controller = "Withdrawal", action = "Index" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "HistoryWithdrawal",
                url: "lich-su-rut-tien",
                defaults: new { controller = "Withdrawal", action = "History" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "HistoryRequestWithdrawal",
                url: "lay-lich-su-rut-tien",
                defaults: new { controller = "Withdrawal", action = "HistoryTransaction" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "GetListWithdrawal",
                url: "lich-su-rut-tien",
                defaults: new { controller = "Withdrawal", action = "ListAllPaging" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "GetInformations",
                url: "thong-tin-ca-nhan",
                defaults: new { controller = "AccountCustomer", action = "Index" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "HistoryOrder",
                url: "don-hang-cua-toi",
                defaults: new { controller = "AccountCustomer", action = "HistoryOrder" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "UpdateAccount",
                url: "cap-nhat-thong-tin",
                defaults: new { controller = "AccountCustomer", action = "UpdateAccount" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "HistoryOrders",
                url: "lay-don-hang-cua-toi",
                defaults: new { controller = "AccountCustomer", action = "GetHistoryOrder" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "GetInformationsss",
                url: "lay-thong-tin-user",
                defaults: new { controller = "AccountCustomer", action = "GetInformation" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "HistoryWin",
                url: "lich-su-chien-thang",
                defaults: new { controller = "Home", action = "HistoryWin" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Gettop10",
                url: "don-hang-gan-day",
                defaults: new { controller = "Home", action = "OrderCurrent" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Maintain",
                url: "bao-tri",
                defaults: new { controller = "Home", action = "Maintain" },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "PTEcommerce.Web.Controllers" }
            );
        }
    }
}
