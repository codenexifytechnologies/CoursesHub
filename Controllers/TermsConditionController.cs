using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class TermsConditionController : BaseController
    {
        // GET: TermsCondition
        [Route("TermsCondition")]
        public ActionResult TermsCondition()
        {
            return View();
        }
    }
}