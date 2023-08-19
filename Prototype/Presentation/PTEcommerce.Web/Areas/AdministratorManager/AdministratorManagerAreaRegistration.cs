using System.Web.Mvc;

namespace PTEcommerce.Web.Areas.AdministratorManager
{
    public class AdministratorManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdministratorManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdministratorManager_default",
                "AdministratorManager/{controller}/{action}/{id}",
                new { action = "Index", controller = "Login" , id = UrlParameter.Optional }
            );
        }
    }
}