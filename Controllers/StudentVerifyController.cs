using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class StudentVerifyController : BaseController
    {
        // GET: StudentVerify
        public ActionResult Verify(string link, string emailid)
        {
            string msg = string.Empty;
            string strName =string.Empty;
            RegistrationOperation objRegistrationOperation = new RegistrationOperation();
            msg = objRegistrationOperation.VerifyStudent(link, emailid);
            if (msg.Contains(","))
            {
                var parts = msg.Split(',');
                if (parts.Length > 1)
                {
                    msg = parts[0];
                    strName = parts[1];
                }
            }
            ViewBag.Msg = msg.Split(',')[0];
            if (msg.Split(',')[0].Contains("Verification Successfull"))
            {
                string body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Student_Registration.html"));
                body = body.Replace("{{student_name}}", strName);
                body = body.Replace("{{verification_link}}", Convert.ToString(ConfigurationManager.AppSettings["LoginUrl"].ToString()));
                Common.SendEmail(emailid, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"].ToString()), ConfigurationManager.AppSettings["Student_Registration"].ToString(), body);
            }
            return View();
        }
    }
}