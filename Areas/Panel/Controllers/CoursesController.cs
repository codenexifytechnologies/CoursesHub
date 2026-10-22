using CoursesHub.Areas.Panel.Models;
using CoursesHub.Areas.Panel.PanelSessionVariable;
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
    public class CoursesController : BaseController
    {
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();

        [SessionCheck]
        public ActionResult ViewCourses(CourseDetails objCourseDetails)
        {

            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            if (Session["Registration"] != null)
            {
                try
                {
                    if (Session["Registration"] != null)
                    {
                        if (((Registration)Session["Registration"]).objUniversityRegistration != null)
                        {
                            UniversityRegistration objUniversityRegistration = ((Registration)Session["Registration"]).objUniversityRegistration;
                            objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "", null, null, null, Convert.ToString(objUniversityRegistration.Id));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewCourses get", "CoursesController");
                }
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }

            return View(objListCourseDetails);
        }

        [SessionCheck]
        public ActionResult AddCourse()
        {
            Registration objRegistration = new Registration();
            CourseDetails objCourseDetails = new CourseDetails();
            objCourseDetails.Testimonials = new List<Testimonial>();
            if (Session["Registration"] != null)
            {
                objRegistration = (Registration)Session["Registration"];
                objCourseDetails.RTO_ID = objRegistration.objUniversityRegistration.RTO_ID;
                bind(Convert.ToString(objRegistration.objUniversityRegistration.Id));
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }

            return View(objCourseDetails);
        }

        [HttpPost]
        [SessionCheck]
        public ActionResult AddCourse(CourseDetails objCourseDetails, HttpPostedFileBase CoursePhoto, HttpPostedFileBase CourseGuide)
        {
            Common.SaveRequestResponse("AddCourse", JsonConvert.SerializeObject(objCourseDetails) + "*****CoursePhoto : " + CoursePhoto + "*****CourseGuide : " + CourseGuide, "");
            try
            {
                if (ModelState.IsValid)
                {
                    Registration objRegistration = new Registration();
                    objRegistration.objUniversityRegistration = new UniversityRegistration();
                    if (Session["Registration"] != null)
                    {
                        objRegistration = (Registration)Session["Registration"];
                        bind(Convert.ToString(objRegistration.objUniversityRegistration.Id));
                        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
                        TempData["Message"] = objCourseDetailsOperation.AddCourseDetails(objCourseDetails, CoursePhoto, CourseGuide);
                        if (TempData["Message"].ToString().Contains("Added"))
                            return RedirectToAction("AddCourse");
                    }
                    else
                    {
                        TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                        return RedirectToAction("LogOff", "Login", new { area = "" });
                    }

                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "AddCourse post", "CoursesController");
            }
            return View();
        }
        
        [SessionCheck]
        public ActionResult EditCourse(int Id)
        {
            CourseDetails objCourseDetails = new CourseDetails();
            objCourseDetails.Id = Id;
            CourseDetails objNewCourseDetails = new CourseDetails();
            try
            {
                Registration objRegistration = new Registration();
                if (Session["Registration"] != null)
                {
                    objRegistration = (Registration)Session["Registration"];
                    bind(Convert.ToString(objRegistration.objUniversityRegistration.Id));

                    objNewCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, objRegistration.LoginType).FirstOrDefault();

                    // If the provider lookup did not return the new columns, read them from the same lookup "View Course" uses
                    if (objNewCourseDetails != null && (string.IsNullOrWhiteSpace(objNewCourseDetails.GovernmentFunding) || string.IsNullOrWhiteSpace(objNewCourseDetails.StudentType)))
                    {
                        CourseDetails listed = objCourseDetailsOperation.GetCourseDetails(new CourseDetails(), "", null, null, null, Convert.ToString(objRegistration.objUniversityRegistration.Id))
                            .FirstOrDefault(x => x.Id == objCourseDetails.Id);
                        if (listed != null)
                        {
                            if (string.IsNullOrWhiteSpace(objNewCourseDetails.GovernmentFunding)) objNewCourseDetails.GovernmentFunding = listed.GovernmentFunding;
                            if (string.IsNullOrWhiteSpace(objNewCourseDetails.StudentType)) objNewCourseDetails.StudentType = listed.StudentType;
                        }
                    }
                    objNewCourseDetails.Testimonials = objCourseDetailsOperation.GetTestimonials(0, objCourseDetails.Id, 0, objRegistration.LoginType).ToList();

                    //if (!string.IsNullOrEmpty(Convert.ToString(objNewCourseDetails.CourseCategory)) && ViewBag.CourseCategory != null)
                    //{
                    //    //ViewBag.CourseCategory = new SelectList(
                    //    //ViewBag.CourseCategory,
                    //    //"Value",
                    //    //"Text",
                    //    //objNewCourseDetails.CourseCategory
                    //    //);
                    //    ViewBag.CourseCategory = new SelectList(
                    //    ViewBag.CourseCategory,
                    //    "Value",
                    //    "Text",
                    //    objNewCourseDetails.CourseCategory.ToString()
                    //    );
                    //    ViewBag.CourseCategorySelectedValue = objNewCourseDetails.CourseCategory; 
                    //}

                    //if (!string.IsNullOrEmpty(Convert.ToString(objNewCourseDetails.TrendingCourses)) && ViewBag.TrendingCourses != null)
                    //{
                    //    ViewBag.TrendingCourses = new SelectList(
                    //    ViewBag.TrendingCourses,
                    //    "Value",
                    //    "Text",
                    //    objNewCourseDetails.TrendingCourses
                    //    );
                    //}

                    //if (!string.IsNullOrEmpty(Convert.ToString(objNewCourseDetails.CourseLevel)) && ViewBag.CourseLevel != null)
                    //{
                    //    ViewBag.CourseLevel = new SelectList(
                    //    ViewBag.CourseLevel,
                    //    "Value",
                    //    "Text",
                    //    objNewCourseDetails.CourseLevel
                    //    );
                    //}
                    //ModelState.Clear();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }

            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditCourse get", "CoursesController");
            }
            return View(objNewCourseDetails);
        }

        [HttpPost]
        [SessionCheck]
        public ActionResult EditCourse(CourseDetails objCourseDetails, HttpPostedFileBase CoursePhoto, HttpPostedFileBase CourseGuide)
        {
            try
            {
                if (Session["Registration"] != null)
                {
                    CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
                    string msg = objCourseDetailsOperation.EditCourseDetails(objCourseDetails, CoursePhoto, CourseGuide);
                    if (msg.Contains("Updated"))
                    {
                        TempData["Message"] = "Course Updated";
                        return RedirectToAction("EditCourse", new { Id = objCourseDetails.Id });
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
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditCourse post", "CoursesController");
            }
            return View();
        }

        [HttpPost]
        [SessionCheck]
        public ActionResult DeleteTestimonial(int id)
        {
            try
            {
                if (Session["Registration"] != null)
                {
                    CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
                    Testimonial objTestimonial = new Testimonial();
                    objTestimonial.Id = id;
                    objTestimonial.IsActive = false;
                    string msg = objCourseDetailsOperation.StausChangeTestimonial(objTestimonial);
                    if (msg.Contains("Deleted"))
                        return Json(new { success = true, message = "Record Deleted" });
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteTestimonial post", "CoursesController");
            }
            return Json(new { success = false, message = "Something went wrong" });
        }

        [HttpPost]
        [SessionCheck]
        public ActionResult ChangeStatusCourse(int id)
        {
            try
            {
                if (Session["Registration"] != null)
                {
                    CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
                    string msg = objCourseDetailsOperation.DeleteCourse(id);
                    return Json(new { success = true, message = msg });
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ChangeStatusCourse post", "CoursesController");
            }
            return Json(new { success = false, message = "Something went wrong" });
        }

        public void bind(string Id = null)
        {
            ViewBag.CourseCategory = Common.getcategory(Id);
            ViewBag.TrendingCourses = Common.getTrendingCourses();
            ViewBag.CourseLevel = Common.getCourseLevel();
        }
    }
}