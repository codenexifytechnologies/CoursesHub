using CoursesHub.App_Start;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoursesHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();
            if (exception != null)
            {
                Common.errorlog(Convert.ToString(exception.Message) + " Inner Msg:" + Convert.ToString(exception.StackTrace), Request.RawUrl, "Application_Error");
            }

            // The error page is attribute-routed at /Error. If the error page itself fails,
            // don't redirect back to it, otherwise the browser loops on "too many redirects".
            if (Request.Path.Equals("/Error", StringComparison.OrdinalIgnoreCase))
            {
                Response.StatusCode = 500;
                Response.ContentType = "text/plain";
                Response.Write("Sorry, an unexpected error occurred. Please try again later.");
                return;
            }
            Response.Redirect("~/Error");
        }
    }
}
