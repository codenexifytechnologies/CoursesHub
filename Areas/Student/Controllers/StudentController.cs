using CoursesHub.Areas.Panel.Models;
using CoursesHub.Controllers;
using CoursesHub.Filter;
using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Student.Controllers
{
    public class StudentController : BaseController
    {
        LoginOperation objLoginOperation = new LoginOperation();
        // GET: Student/Student
        [SessionCheck]
        public ActionResult Index()
        {
            if (Session["Registration"] != null)
            {
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
            return View();
        }

        [SessionCheck]
        public ActionResult StudentDashboard()
        {
            if (Session["Registration"] != null)
            {
                // Summary numbers for the dashboard cards (same data as the Applied Courses / View Feedback pages)
                try
                {
                    Registration objRegistration = (Registration)Session["Registration"];
                    int studentId = objRegistration.objStudentRegistration.Id;
                    var objCourseDetailsOperation = new CourseDetailsOperation();
                    var applied = objCourseDetailsOperation.GetAppliedCourseDetails(studentId).ToList();
                    var feedback = objCourseDetailsOperation.GetTestimonials(0, 0, studentId, "Student").ToList();
                    ViewBag.AppliedCount = applied.Count;
                    ViewBag.RecentApplied = applied.Take(3).ToList();
                    ViewBag.FeedbackCount = feedback.Count;
                    ViewBag.VerifiedFeedbackCount = feedback.Count(x => x.IsActive);
                }
                catch (Exception ex)
                {
                    Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "StudentDashboard", "StudentController");
                }
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
            return View();
        }

        [SessionCheck]
        public ActionResult ViewProfile(int Id)
        {
            if (Session["Registration"] != null)
            {
                Registration objSessionRegistration = (Registration)Session["Registration"];
                Registration objRegistration = objLoginOperation.GetProfile(Convert.ToString(objSessionRegistration.LoginType), Id);
                return View(objRegistration);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
        }

        [SessionCheck]
        public ActionResult ChangePassword(int Id)
        {
            if (Session["Registration"] != null)
            {
                ChangePassword objChangePassword = new ChangePassword();
                Registration objSessionRegistration = (Registration)Session["Registration"];
                StudentRegistration objStudentRegistration = objLoginOperation.GetProfile(objSessionRegistration.LoginType, Id).objStudentRegistration;
                objChangePassword.Id = Id;
                objChangePassword.oldPassword = objStudentRegistration.Password;
                return View(objChangePassword);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
        }

        [HttpPost]
        [SessionCheck]
        public ActionResult ChangePassword(ChangePassword objChangePassword)
        {
            try
            {
                if (Session["Registration"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        TempData["ChangePasswordSuccessMessage"] = Common.ChangePassword(objChangePassword, "S");
                        ModelState.Clear();
                        return RedirectToAction("StudentDashboard");
                    }
                    else
                    {
                        string errorMessage = string.Join(" | ",
                            ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                        TempData["ChangePasswordSuccessMessage"] = errorMessage;
                    }
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Post ChangePassword", "");
            }
            return RedirectToAction("ChangePassword", new { Id = objChangePassword.Id });
        }
    }
}