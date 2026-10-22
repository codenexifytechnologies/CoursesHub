using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class PrivacyPolicyController : BaseController
    {
        // GET: PrivacyPolicy
        [Route("PrivacyPolicy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}