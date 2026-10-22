using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoursesHub.Filter
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Registration"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "Login" }
                    });
            }
        }
    }
}