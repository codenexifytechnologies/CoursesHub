using CoursesHub.Areas.Admin.Filter;
using CoursesHub.Areas.Admin.Models;
using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Admin.Controllers
{
    [CheckLogin]
    public class LeadController : Controller
    {
        LoginOperation objLoginOperation = new LoginOperation();
        RegistrationOperation objRegistrationOperation = new RegistrationOperation();
        LeadOperations objLeadOperations = new LeadOperations();
        // GET: Admin/Lead
        public ActionResult ViewLead()
        {
            if (Session["AdminUser"] != null)
            {
                List<Lead> objListLead = new List<Lead>();
                objListLead = objLeadOperations.GetAllLeads();
                return View(objListLead);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }

        public ActionResult ViewLeadDetails(int Student_ID)
        {
            if (Session["AdminUser"] != null)
            {
                Lead objListPremiumLead = new Lead();
                StudentRegistration objStudentRegistration = new StudentRegistration();
                objStudentRegistration = objLoginOperation.GetProfile("Student", Student_ID).objStudentRegistration;

                ViewBag.StudentDetails = objStudentRegistration;
                ViewBag.PremiumLead = objLeadOperations.GetPremiumLeadsById(Student_ID);
                ViewBag.NormalLead = objLeadOperations.GetAllNormalLeadsById(Student_ID);
                return View();
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }

        public ActionResult SendEmail(int LeadId, string LeadType)
        {
            if (Session["AdminUser"] != null)
            {
                Lead objLead = objLeadOperations.GetAllDetails(LeadId, LeadType).FirstOrDefault();
                string subject = Convert.ToString(ConfigurationManager.AppSettings["Student_Inquiry"]);
                subject = subject.Replace("{{Course_Name}}", objLead.Course_Name);
                subject = subject.Replace("{{Student_Name}}", objLead.Student_Name);
                string body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Student_Inquiry.html"));
                body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                body = body.Replace("{{University_Name}}", objLead.University_Name);
                body = body.Replace("{{Student_Name}}", objLead.Student_Name);
                body = body.Replace("{{Student_Email}}", objLead.Student_EmailId);
                body = body.Replace("{{Student_Phone}}", objLead.Student_Mobile);
                body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                body = body.Replace("{{Location}}", objLead.Student_isAustraliaResident ? (objLead.Student_Zipcode + "," + objLead.Student_State) : objLead.Student_Country);
                body = body.Replace("{{Course_Name}}", objLead.Course_Name);

                Common.SendEmail(objLead.University_Email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                bool Result = objLeadOperations.UpdateLeadAfterEmail(LeadId, LeadType);
                if (Result == true)
                {
                    TempData["Message"] = "Email sent successfully !!";
                }
                else
                {
                    TempData["ErrMessage"] = "Error while sending email !!";
                }

                return RedirectToAction("ViewLeadDetails", new { Student_ID = objLead.Student_ID });
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }

        [HttpPost]
        public ActionResult BulkSendEmail(List<int> ids)
        {
            if (Session["AdminUser"] != null)
            {
                if (ids != null && ids.Count > 0)
                {
                    foreach (var id in ids)
                    {
                        Lead objLead = objLeadOperations.GetAllDetails(id, "N").FirstOrDefault();
                        string subject = Convert.ToString(ConfigurationManager.AppSettings["Student_Inquiry"]);
                        subject = subject.Replace("{{Course_Name}}", objLead.Course_Name);
                        subject = subject.Replace("{{Student_Name}}", objLead.Student_Name);
                        string body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Student_Inquiry.html"));
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                        body = body.Replace("{{University_Name}}", objLead.University_Name);
                        body = body.Replace("{{Student_Name}}", objLead.Student_Name);
                        body = body.Replace("{{Student_Email}}", objLead.Student_EmailId);
                        body = body.Replace("{{Student_Phone}}", objLead.Student_Mobile);
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                        body = body.Replace("{{Location}}", objLead.Student_isAustraliaResident ? (objLead.Student_Zipcode + "," + objLead.Student_State) : objLead.Student_Country);
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);

                        Common.SendEmail(objLead.University_Email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                        bool Result = objLeadOperations.UpdateLeadAfterEmail(id, "N");
                    }
                }
                return Json(true);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }
    }
}