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
    public class CoursesController : Controller
    {
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();

        public ActionResult ViewCourses(CourseDetails objCourseDetails)
        {
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "Admin");
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewCourses", "CourseController");
            }

            return View(objListCourseDetails);
        }

        public ActionResult EditCourseDetails(int Id)
        {
            CourseDetails objCourseDetails = new CourseDetails();
            objCourseDetails.Id = Id;
            CourseDetails objNewCourseDetails = new CourseDetails();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objNewCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "Admin").FirstOrDefault();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditCourse get", "CourseController");
            }
            return View(objNewCourseDetails);
        }

        [HttpPost]
        public ActionResult ChangeStatusCourse(CourseDetails objCourseDetails)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
                    TempData["EditCourseDetailsMsg"] = objCourseDetailsOperation.StausChangeCourse(objCourseDetails);
                    return RedirectToAction("EditCourseDetails", new { Id = objCourseDetails.Id });
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }

            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ChangeStatusCourse post", "CourseController");
            }
            TempData["EditCourseDetailsMsg"] = "Something Went Wrong";
            return RedirectToAction("EditCourseDetails", new { Id = objCourseDetails.Id });
        }

        public ActionResult DeleteCourseDetails(int Id)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditCourseDetailsMsg"] = objCourseDetailsOperation.DeleteCourse(Id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteCourse", "CourseController");
            }
            return RedirectToAction("ViewCourses");
        }

        [HttpPost]
        public ActionResult RejectCourse(int id, string reason)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["RejectCourseDetailsMsg"] = objCourseDetailsOperation.RejectCourse(id, reason);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteCourse", "CourseController");
            }

            return Json(new { status = "success" });
        }


    }
}