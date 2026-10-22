using CoursesHub.Areas.Panel.Models;
using CoursesHub.Areas.Student.Model;
using CoursesHub.Controllers;
using CoursesHub.Filter;
using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Student.Controllers
{
    public class FeedbackController : BaseController
    {
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
        // GET: Student/Feedback
        [SessionCheck]
        public ActionResult ViewFeedback()
        {
            List<Testimonial> objListTestimonial = new List<Testimonial>();
            try
            {
                if (Session["Registration"] != null)
                {
                    Registration objRegistration = (Registration)Session["Registration"];
                    objListTestimonial = objCourseDetailsOperation.GetTestimonials(0, 0, objRegistration.objStudentRegistration.Id, "Student").ToList();
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

            return View(objListTestimonial);
        }

        [SessionCheck]
        public ActionResult EditFeedback(int TestimonialId)
        {
            Testimonial objTestimonial = new Testimonial();
            try
            {
                if (Session["Registration"] != null)
                {
                    objTestimonial = objCourseDetailsOperation.GetTestimonials(TestimonialId, 0, 0, "").FirstOrDefault();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditFeedback get", "FeedbackOperation");
            }

            return View(objTestimonial);
        }

        [HttpPost]
        [SessionCheck]
        public ActionResult EditFeedback(Testimonial objTestimonial)
        {
            try
            {
                if (Session["Registration"] != null)
                {
                    TempData["EditTestimonialDetailsMsg"] = objCourseDetailsOperation.EditTestimonial(objTestimonial);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditFeedback post", "FeedbackOperation");
            }
            return RedirectToAction("ViewFeedback");
        }
    }
}