using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class MyAccountController : BaseController
    {
        // GET: MyAccount
        public ActionResult MyAccount()
        {
            return View();
        }
    }
}