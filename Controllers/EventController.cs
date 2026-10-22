using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class EventController : BaseController
    {
        // GET: Event
        public ActionResult Event()
        {
            return View();
        }
        public ActionResult EventDetails()
        {
            return View();
        }
    }
}