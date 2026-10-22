using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class ContactUsController : BaseController
    {
        ContactUsOperation objContactUsOperation = new ContactUsOperation();
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [Route("ContactUs")]
        [HttpPost]
        public ActionResult ContactUs(ContactUs objContactUs)
        {
            try
            {
                TempData["Message"] = objContactUsOperation.AddConatctus(objContactUs);
                return RedirectToAction("ContactUs");
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ContactUs", "ContactUsController");
            }
            return View();
        }
    }
}