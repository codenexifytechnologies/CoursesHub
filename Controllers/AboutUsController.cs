using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class AboutUsController : BaseController
    {
        // GET: AboutUs
        [Route("AboutUs")]
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}