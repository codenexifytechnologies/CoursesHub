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
    public class UniversityController : Controller
    {
        UniversityOperation objUniversityOperation = new UniversityOperation();
        LoginOperation objLoginOperation = new LoginOperation();
        RegistrationOperation objRegistrationOperation = new RegistrationOperation();

        public ActionResult ViewUniversity()
        {
            Registration objRegistration = new Registration();
            objRegistration.objUniversityRegistration = new UniversityRegistration();
            List<UniversityRegistration> objListUniversityRegistration = new List<UniversityRegistration>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListUniversityRegistration = objUniversityOperation.GetAllUniversity(objRegistration.objUniversityRegistration);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewUniversity", "UniversityController");
            }
            return View(objListUniversityRegistration);
        }

        public ActionResult ViewUniversityDetails(int Id)
        {
            Registration objRegistration = new Registration();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objRegistration = objLoginOperation.GetProfile("University", Id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewUniversityDetails Get", "UniversityController");
            }
            return View(objRegistration.objUniversityRegistration);
        }

        [HttpPost]
        public ActionResult ViewUniversityDetails(UniversityRegistration objUniversityRegistration, string action)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    if (action == "btnVerified")
                    {
                        objUniversityRegistration.IsActive = true;
                        TempData["EditUniversityDetailsMsg"] = objRegistrationOperation.ChangeStatusUniversity(objUniversityRegistration, 0);
                    }
                    else if (action == "btnActive")
                    {
                        objUniversityRegistration.isUniversityActive = true;
                        TempData["EditUniversityDetailsMsg"] = objRegistrationOperation.ChangeStatusUniversity(objUniversityRegistration, 0);

                    }
                    else if (action == "btnInactive")
                    {
                        objUniversityRegistration.isUniversityActive = false;
                        TempData["EditUniversityDetailsMsg"] = objRegistrationOperation.ChangeStatusUniversity(objUniversityRegistration, 0);

                    }
                    else if (action == "btnArchive")
                    {
                        TempData["EditUniversityDetailsMsg"] = objRegistrationOperation.ChangeStatusUniversity(objUniversityRegistration, 1);
                        return RedirectToAction("ViewUniversity");
                    }
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewUniversityDetails Post", "UniversityController");
            }
            return RedirectToAction("ViewUniversityDetails", new { Id = objUniversityRegistration.Id });
        }

        public ActionResult DeleteUniversity(int Id)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditUniversityDetailsMsg"] = objRegistrationOperation.DeleteUniversity(Id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteUniversity", "UniversityController");
            }
            return RedirectToAction("ViewUniversity");
        }

        [HttpPost]
        public ActionResult RejectUniversity(int UniversityId, string reason)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    UniversityRegistration objUniversityRegistration = new UniversityRegistration();
                    objUniversityRegistration.Id = UniversityId;
                    objUniversityRegistration.RejectionReason = reason;
                    objUniversityRegistration.IsReject = true;
                    objUniversityRegistration.IsActive = false;
                    TempData["EditUniversityDetailsMsg"] = objRegistrationOperation.ChangeStatusUniversity(objUniversityRegistration, 0);
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

        public ActionResult ViewArchiveUniversity()
        {
            List<UniversityRegistration> objListUniversityRegistration = new List<UniversityRegistration>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListUniversityRegistration = objUniversityOperation.GetArchiveUniversity(null);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewUniversity", "UniversityController");
            }
            return View(objListUniversityRegistration);
        }


        public ActionResult ArchiveUniversityDetails(int UniversityID)
        {
            UniversityRegistration objListUniversityRegistration = new UniversityRegistration();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListUniversityRegistration = objUniversityOperation.GetArchiveUniversity(UniversityID).FirstOrDefault();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewUniversity", "UniversityController");
            }
            return View(objListUniversityRegistration);
        }

        [HttpPost]
        public ActionResult ArchiveUniversityDetails(UniversityRegistration objUniversityRegistration)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditUniversityDetailsMsg"] = objUniversityOperation.RetriveArchiveUniversity(objUniversityRegistration.Id).FirstOrDefault();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ArchiveUniversityDetails", "UniversityController");
            }
            return View();
        }
    }
}