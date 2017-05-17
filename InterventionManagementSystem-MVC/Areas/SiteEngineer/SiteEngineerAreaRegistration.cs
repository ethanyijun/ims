using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer
{
    public class SiteEngineerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SiteEngineer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SiteEngineer_default",
                "SiteEngineer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}