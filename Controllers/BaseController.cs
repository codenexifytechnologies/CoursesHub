using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.SessionUser = Session["Registration"];
            base.OnActionExecuting(filterContext);
        }
    }
}