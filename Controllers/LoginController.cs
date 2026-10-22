using CoursesHub.Areas.Panel.PanelSessionVariable;
using CoursesHub.Models;
using CoursesHub.Operation;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CoursesHub.Controllers
{
    public class LoginController : Controller
    {
        LoginOperation objLoginOperation = new LoginOperation();
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"].ToString();

        #region Login
        [Route("Login")]
        public ActionResult Login(string type)
        {
            // /Login?type=Student | /Login?type=TrainingProvider ; plain /Login keeps the old default (Student)
            Login objLogin = new Login { LoginType = LoginTypes.Normalize(type) ?? LoginTypes.Student };
            return View(objLogin);
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(Login objLogin)
        {
            try
            {
                // Never trust the posted hidden field: only Student / University are allowed.
                // Drop the posted value so the re-rendered hidden field uses the normalized one.
                ModelState.Remove("LoginType");
                string loginType = LoginTypes.Normalize(objLogin.LoginType);
                if (loginType == null)
                {
                    objLogin.LoginType = LoginTypes.Student;
                    TempData["ErrMessage"] = "Please choose Student Login or Training Provider Login.";
                    return View(objLogin);
                }
                objLogin.LoginType = loginType;

                if (ModelState.IsValid)
                {
                    Registration objRegistration = objLoginOperation.Login(objLogin);
                    objRegistration.LoginType = objLogin.LoginType;

                    // The account returned must match the requested login type.
                    bool roleMatches = loginType == LoginTypes.Student
                        ? objRegistration.objUniversityRegistration == null
                        : objRegistration.objStudentRegistration == null;
                    if (!roleMatches)
                    {
                        TempData["ErrMessage"] = "Enter Valid Credentials.";
                        return View(objLogin);
                    }

                    if (objRegistration.objStudentRegistration != null || objRegistration.objUniversityRegistration != null)
                    {
                        if (objRegistration.objStudentRegistration != null)
                        {
                            if (string.IsNullOrEmpty(objRegistration.objStudentRegistration.SqlMsg))
                            {
                                Session["Registration"] = objRegistration;
                                return RedirectToAction("Index", "Index");
                            }
                            else
                            {
                                TempData["ErrMessage"] = objRegistration.objStudentRegistration.SqlMsg;
                            }
                        }
                        else if (objRegistration.objUniversityRegistration != null)
                        {
                            if (string.IsNullOrEmpty(objRegistration.objUniversityRegistration.SqlMsg))
                            {
                                Session["Registration"] = objRegistration;
                                return RedirectToAction("UniversityDashboard", "Panel/University");
                            }
                            else
                            {
                                TempData["ErrMessage"] = objRegistration.objUniversityRegistration.SqlMsg;
                            }
                        }
                        else
                        {
                            TempData["ErrMessage"] = "Enter Valid Credentials.";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "User not available in system, Please contact admin and create your account.";
                    }
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Post Login", "LoginController");
            }
            return View(objLogin);
        }
        #endregion

        #region User Logout
        [Route("LogOff")]
        public ActionResult LogOff()
        {
            if (TempData["Message"] != null)
            {
                TempData["Message"] = TempData["Message"];
            }

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Index");
        }
        #endregion


        #region Lost Password
        [Route("LostPassword")]
        public ActionResult LostPassword(Login objLogin)
        {
            return View(objLogin);
        }

        [Route("LostPassword")]
        [HttpPost]
        public ActionResult LostPasswordPost(Login objLogin)
        {
            try
            {
                TempData["Message"] = objLoginOperation.LostPassword(objLogin);
                if (Convert.ToString(TempData["Message"]).Contains("Success"))
                {
                    return RedirectToAction("Login", "Login", new { area = "", type = LoginTypes.ToUrlType(objLogin.LoginType) });
                }
                else
                {
                    return View(objLogin);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Post Login", "LoginController");
            }
            return View(objLogin);
        }
        #endregion
    }
}