using DapperExtensions.Mapper;
using Framework.Configuration;
using Framework.Helper.Attributes;
using Framework.Helper.Logging;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PTEcommerce.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(CustomPluralizedMapper<>);
            MvcHandler.DisableMvcResponseHeader = true;

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var useSSL = Convert.ToBoolean(Config.GetConfigByKey("useSSL"));
            if (!Request.IsLocal && !Request.IsSecureConnection && useSSL)
            {
                string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
                Response.Redirect(redirectUrl, false);
            }
        }
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-Frame-Options");
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Logging.PutError("App_Error", exception);
            Response.Clear();
            Server.ClearError();
            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        Response.Redirect("/trang-khong-ton-tai");
                        break;
                    case 500:
                        // server error
                        Response.Redirect("/trang-khong-ton-tai");
                        break;
                    default:
                        Response.Redirect("/");
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class CustomPluralizedMapper<T> : ClassMapper<T> where T : class
            //public class CustomPluralizedMapper<T> : PluralizedAutoClassMapper<T> where T : class
        {
            public override void Table(string tableName)
            {
                string table = EntityType.GetAttributeValue((TableAttribute dna) => dna.Name);
                //Properties.
                base.Table(table);
                base.AutoMap();
            }
        }
    }
}
