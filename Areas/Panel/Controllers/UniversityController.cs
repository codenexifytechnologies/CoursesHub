using CoursesHub.Areas.Panel.Models;
using CoursesHub.Controllers;
using CoursesHub.Filter;
using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Panel.Controllers
{
    public class UniversityController : BaseController
    {
        LoginOperation objLoginOperation = new LoginOperation();
        UniversityOperations objUniversityOperations = new UniversityOperations();

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
        public ActionResult UniversityDashboard()
        {
            UniversityDetails objUniversityDetails = objUniversityOperations.GetDashBoardCount(Convert.ToInt32(((Registration)Session["Registration"]).objUniversityRegistration.Id));
            return View(objUniversityDetails);
        }

        [SessionCheck]
        public ActionResult ViewProfile(int Id)
        {
            if (Session["Registration"] != null)
            {
                Registration objSessionRegistrationr = (Registration)Session["Registration"];
                Registration objRegistration = objLoginOperation.GetProfile(Convert.ToString(objSessionRegistrationr.LoginType), Id);
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
                Registration objSessionRegistrationr = (Registration)Session["Registration"];
                ChangePassword objChangePassword = new ChangePassword();
                UniversityRegistration objUniversityRegistration = objLoginOperation.GetProfile(Convert.ToString(objSessionRegistrationr.LoginType), Id).objUniversityRegistration;
                objChangePassword.Id = Id;
                objChangePassword.oldPassword = objUniversityRegistration.Password;
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
                        TempData["ChangePasswordSuccessMessage"] = Common.ChangePassword(objChangePassword, "U");
                        ModelState.Clear();
                        return RedirectToAction("UniversityDashboard");
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