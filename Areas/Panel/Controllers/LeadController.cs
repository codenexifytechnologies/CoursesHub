using CoursesHub.Areas.Admin.Models;
using CoursesHub.Areas.Panel.Models;
using CoursesHub.Controllers;
using CoursesHub.Filter;
using CoursesHub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Panel.Controllers
{
    public class LeadController : BaseController
    {
        LeadOperations objLeadOperations = new LeadOperations();
        // GET: Panel/Lead
        [SessionCheck]
        public ActionResult ViewLead()
        {
            List<Lead> objListLead = new List<Lead>();
            if (Session["Registration"] != null)
            {
                if (((Registration)Session["Registration"]).objUniversityRegistration.Id != null)
                {
                    objListLead = objLeadOperations.GetLeadForUniversity(Convert.ToInt32(((Registration)Session["Registration"]).objUniversityRegistration.Id));
                }
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
            ViewBag.Leads = objListLead;
            return View(objListLead);
        }
    }
}