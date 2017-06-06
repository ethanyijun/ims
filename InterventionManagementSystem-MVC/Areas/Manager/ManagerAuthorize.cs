using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Areas.Manager
{
    public class ManagerAuthorizeAttribute:AuthorizeAttribute
    {
        /// <summary>
        /// only authorize manager to access
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var area = routeData.DataTokens["area"];
            var user = httpContext.User;
            if (area != null && area.ToString() == "Manager")
            {
                if (user.Identity.IsAuthenticated && user.IsInRole("Manager"))
                    return true;
            }

            return false;


        }
    }
}