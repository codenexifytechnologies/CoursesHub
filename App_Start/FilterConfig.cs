using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogHandleErrorAttribute { View = "_Error" });
        }
    }

    // Logs controller/view exceptions, then renders Views/Shared/_Error.cshtml.
    // The default HandleErrorAttribute looks for a view named "Error", which doesn't exist here.
    public class LogHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                CoursesHub.Models.Common.errorlog(Convert.ToString(filterContext.Exception.Message) + " Inner Msg:" + Convert.ToString(filterContext.Exception.StackTrace), filterContext.HttpContext.Request.RawUrl, "HandleError");
            }
            base.OnException(filterContext);
        }
    }
}