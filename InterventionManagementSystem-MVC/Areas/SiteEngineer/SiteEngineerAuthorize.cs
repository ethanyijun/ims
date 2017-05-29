using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer
{
    public class SiteEngineerAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var area = routeData.DataTokens["area"];
            var user = httpContext.User;
            if (area != null && area.ToString() == "SiteEngineer")
            {
                if (!user.Identity.IsAuthenticated && !user.IsInRole("SiteEngineer"))
                    return true;
            }

            return false;


        }

    }
}