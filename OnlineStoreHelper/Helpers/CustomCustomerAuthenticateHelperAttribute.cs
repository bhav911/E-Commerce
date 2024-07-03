using OnlineStore.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreHelper.Helpers
{
    public class CustomCustomerAuthenticateHelperAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HttpContext.Current.Session["UserRole"].Equals("Customer");
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    {"controller",UserSession.UserRole},
                    {"action","Unauthorize"},
                    {"role",UserSession.UserRole}
                }
            );
        }
    }
}
