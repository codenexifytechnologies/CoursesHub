using CoursesHub.Areas.Panel.Models;
using CoursesHub.Controllers;
using CoursesHub.Filter;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Student.Controllers
{
    public class AppliedCoursesController : BaseController
    {
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
        // GET: Student/AppliedCourses
        [SessionCheck]
        public ActionResult AppliedCourses()
        {
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            try
            {
                if (Session["Registration"] != null)
                {

                    Registration objRegistration = (Registration)Session["Registration"];
                    objListCourseDetails = objCourseDetailsOperation.GetAppliedCourseDetails(objRegistration.objStudentRegistration.Id).ToList();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewFeedback get", "FeedbackOperation");
            }

            return View(objListCourseDetails);
        }
    }
}