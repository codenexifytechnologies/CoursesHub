using CoursesHub.Areas.Admin.Filter;
using CoursesHub.Areas.Admin.Models;
using CoursesHub.Areas.Panel.Models;
using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Admin.Controllers
{
    [CheckLogin]
    public class TestimonialController : Controller
    {
        // GET: Admin/Testimonial
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
        RegistrationOperation objRegistrationOperation = new RegistrationOperation();

        public ActionResult ViewTestimonial()
        {
            Testimonial objTestimonial = new Testimonial();
            List<Testimonial> objListTestimonial = new List<Testimonial>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListTestimonial = objCourseDetailsOperation.GetTestimonials(0, 0, 0, "Admin");
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewTestimonial", "TestimonialController");
            }
            return View(objListTestimonial);
        }

        public ActionResult ViewTestimonialDetails(int Id)
        {
            Testimonial objTestimonial = new Testimonial();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objTestimonial = objCourseDetailsOperation.GetTestimonials(Id, 0, 0, "").FirstOrDefault();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewTestimonialDetails Get", "TestimonialController");
            }
            return View(objTestimonial);
        }

        [HttpPost]
        public ActionResult ViewTestimonialDetails(Testimonial objTestimonial)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditTestimonialDetailsMsg"] = objCourseDetailsOperation.StausChangeTestimonial(objTestimonial);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewTestimonialDetails Post", "TestimonialController");
            }
            return RedirectToAction("ViewTestimonialDetails", new { Id = objTestimonial.Id });
        }

        public ActionResult DeleteTestimonial(int Id)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditTestimonialDetailsMsg"] = objCourseDetailsOperation.DeleteTestimonial(Id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteTestimonial", "TestimonialController");
            }
            return RedirectToAction("ViewTestimonial");
        }
    }
}