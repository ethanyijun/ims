using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Areas.Accountant
{
    public class AccountantAuthorizeAttribute :AuthorizeAttribute

    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var area = routeData.DataTokens["area"];
            var user = httpContext.User;
            if (area != null && area.ToString() == "Accountant")
            {
                if (!user.Identity.IsAuthenticated && !user.IsInRole("Accountant"))
                    return true;
            }

            return false;


        }
    }
}